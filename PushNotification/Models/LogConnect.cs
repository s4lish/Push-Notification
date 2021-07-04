using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushNotification.Models
{
    public class LogConnect
    {
        public Int64 Id { get; set; }
        public string userID { get; set; }
        public int Type { get; set; } // 1 connect - 2 disconnect
        public DateTime CreateAt { get; set; }


        public LogConnect()
        {
            CreateAt = DateTime.Now;
        }
    }
}
