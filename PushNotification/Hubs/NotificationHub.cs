﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PushNotification.Context;
using PushNotification.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushNotification.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> Users =
                    new ConcurrentDictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        private readonly PushDBContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationHub(PushDBContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task CheckNotifications(ReceiveNotification rs)
        {
            Notifications st = new Notifications();

            st.Content = rs.Content;
            st.datetime = DateTime.Now;

            st.status = false;
            st.type = int.Parse(rs.type);
            st.keyuser = Guid.NewGuid();
            st.user = rs.user.ToString();
            st.icon = rs.icon;
            st.attach1 = rs.attach1;
            st.attach2 = rs.attach2;
            st.Title = rs.Title;
            st.Color = rs.Color;
            st.Sound = rs.Sound;

            st.Reserve1 = rs.Reserve1;
            st.Reserve2 = rs.Reserve2;
            st.Reserve3 = JsonConvert.SerializeObject(rs.Reserve3);

            if (Users.TryGetValue(rs.user.ToString(), out string userHub))
            {
                st.status = true;
                List<Notifications> lsN = new List<Notifications>();  // برای ایجاد جیسون آرایه ای
                lsN.Add(st);
                //var cid = userHub.ConnectionIds.ToList();
                string cid = userHub;
                var selectedNoti = lsN.Select(s => new { s.keyuser, s.icon, s.type, s.Content, s.attach1, s.attach2, s.Title, s.Color, s.Sound, s.Reserve1, s.Reserve2, s.Reserve3, s.datetime }).ToList();

                string jsonString = JsonConvert.SerializeObject(selectedNoti);

                await _hubContext.Clients.Client(cid).SendAsync("ReceiveNotifications", jsonString);
            }

            await _context.Notifications.AddAsync(st);
            await _context.SaveChangesAsync();
        }

        public async Task CheckBroadCast(ReceiveNotification rs)
        {
            Notifications st = new Notifications();

            st.Content = rs.Content;
            st.datetime = DateTime.Now;

            st.status = false;
            st.type = int.Parse(rs.type);
            st.keyuser = Guid.NewGuid();
            st.user = rs.user.ToString();
            st.icon = rs.icon;
            st.attach1 = rs.attach1;
            st.attach2 = rs.attach2;
            st.Title = rs.Title;
            st.Color = rs.Color;
            st.Sound = rs.Sound;

            st.Reserve1 = rs.Reserve1;
            st.Reserve2 = rs.Reserve2;
            st.Reserve3 = JsonConvert.SerializeObject(rs.Reserve3);

            List<Notifications> lsN = new List<Notifications>();  // برای ایجاد جیسون آرایه ای
            lsN.Add(st);
            //var cid = userHub.ConnectionIds.ToList();
            //var cid = userHub.ConnectionIds.LastOrDefault();
            var selectedNoti = lsN.Select(s => new { s.keyuser, s.icon, s.type, s.Content, s.attach1, s.attach2, s.Title, s.Color, s.Sound, s.Reserve1, s.Reserve2, s.Reserve3, s.datetime }).ToList();

            string jsonString = JsonConvert.SerializeObject(selectedNoti);

            await _hubContext.Clients.All.SendAsync("ReceiveBroadCast", jsonString);
        }

        public override Task OnConnectedAsync()
        {
            Microsoft.AspNetCore.Http.HttpContext req = Context.GetHttpContext();

            string userName = req.Request.Query["userID"];
            string connectionId = Context.ConnectionId;

            lock (connectionId)
            {
                string user = Users.GetOrAdd(userName, connectionId);

                LogConnect lg = new LogConnect();
                lg.userID = userName;
                lg.Type = 1;

                _context.LogConnect.Add(lg);
                _context.SaveChangesAsync();

                Clients.Others.SendAsync(userName);
            }

            if (Users.TryGetValue(userName, out string userHub))
            {
                IQueryable<Notifications> allNoti = _context.Notifications.Where(x => !x.status && x.user == userName);
                if (allNoti.Any())
                {
                    var selectedNoti = allNoti.Select(s => new { s.keyuser, s.icon, s.type, s.Content, s.attach1, s.attach2, s.Title, s.Color, s.Sound, s.Reserve1, s.Reserve2, s.Reserve3, s.datetime }).ToList();
                    string jsonString = JsonConvert.SerializeObject(selectedNoti);
                    //var cid = userHub.ConnectionIds.ToList();
                    string cid = userHub;

                    Clients.Client(cid).SendAsync("ReceiveNotifications", jsonString);

                    foreach (Notifications item in allNoti.ToList())
                    {
                        item.status = true;
                    }
                    _context.SaveChanges();
                }
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Microsoft.AspNetCore.Http.HttpContext req = Context.GetHttpContext();

            string userName = req.Request.Query["userID"];
            string connectionId = Context.ConnectionId;

            string user;
            Users.TryGetValue(userName, out user);

            if (user != null)
            {
                lock (connectionId)
                {
                    string removedUser;
                    Users.TryRemove(userName, out removedUser);

                    LogConnect lg = new LogConnect();
                    lg.userID = userName;
                    lg.Type = 2;

                    _context.LogConnect.Add(lg);
                    _context.SaveChangesAsync();
                    Clients.Others.SendAsync(userName);
                }
            }

            //var us = Users.Keys.ToList();
            // Clients.All.SendAsync("ReceiveListsUsers", us);

            return base.OnDisconnectedAsync(exception);
        }
    }

    public class UserHubModels
    {
        public string UserName { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }
}