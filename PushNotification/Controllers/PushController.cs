using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PushNotification.Context;
using PushNotification.Models;
using PushNotification.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
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
        // GET: api/<controller>
        public async Task<IActionResult> Get(int pageNumber,int pageSize)
        {
            var items = await _context.Notifications
                  .OrderByDescending(o => o.Id)
                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                  .ToListAsync();

            var count = _context.Notifications.Count();
            var info = new
            {
                items = items,
                count = count,
                pageNumber = pageNumber,
                pageSize = pageSize
            };

            return Ok(info);

        }

        [HttpGet("v2")]
        public IEnumerable<string> myNewFunc()
        {




            return new string[] { "value100", "value200" };
        }

        [HttpPost("Receive")]
        [Authorize]
        public async Task<IActionResult> Post([FromForm]ReceiveNotification rs)
        {

            try
            {


                var hb = new NotificationHub(_context, _hubContext);

                if(rs.user == "-1")
                {
                    await hb.CheckBroadCast(rs);
                }
                else
                {
                    await hb.CheckNotifications(rs);

                }
                return Ok(new { Code=1,Msg="ok"});

            }
            catch (Exception ex)
            {
                return BadRequest(new {Code=9,Msg=ex.ToString() });
            }





        }


    }
}
