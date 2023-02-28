using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PushNotification.Context;
using PushNotification.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushNotification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogConnectController : ControllerBase
    {
        private readonly PushDBContext _context;

        public LogConnectController(PushDBContext pushDBContext)
        {
            _context = pushDBContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(int pageNumber, int pageSize)
        {
            List<LogConnect> items = await _context.LogConnect
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
    }
}
