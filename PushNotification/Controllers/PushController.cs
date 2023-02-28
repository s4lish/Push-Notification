using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PushNotification.Context;
using PushNotification.Hubs;
using PushNotification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PushNotification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PushController : ControllerBase
    {
        private readonly PushDBContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public PushController(PushDBContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [HttpGet("first")]
        [AllowAnonymous]
        public IActionResult GetFirstt(int pageNumber, int pageSize)
        {


            return Ok("start push notification");

        }
        // GET: api/<controller>
        public async Task<IActionResult> Get(int pageNumber, int pageSize)
        {
            List<Notifications> items = await _context.Notifications
                  .OrderByDescending(o => o.Id)
                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                  .ToListAsync();

            int count = _context.Notifications.Count();
            var info = new
            {
                items = items,
                count = count,
                pageNumber = pageNumber,
                pageSize = pageSize
            };

            return Ok(info);

        }


        [HttpPost("Receive")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] ReceiveNotification rs)
        {

            try
            {

                NotificationHub hb = new NotificationHub(_context, _hubContext);

                if (rs.user == -1)
                {
                    await hb.CheckBroadCast(rs);
                }
                else
                {
                    await hb.CheckNotifications(rs);

                }
                return Ok(new { Code = 1, Msg = "ok" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { Code = 9, Msg = ex.ToString() });
            }





        }

        [HttpPost("ReceiveGroup")]
        [Authorize]
        public async Task<IActionResult> Post2([FromBody] List<ReceiveNotification> rs)
        {

            try
            {

                NotificationHub hb = new NotificationHub(_context, _hubContext);

                foreach (ReceiveNotification item in rs)
                {
                    await hb.CheckNotifications(item);
                }

                return Ok(new { Code = 1, Msg = "ok" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { Code = 9, Msg = ex.ToString() });
            }





        }

    }
}
