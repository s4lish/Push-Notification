using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PushNotification.Models
{
    public class Notifications
    {
        [Key]
        public long Id { get; set; }
        public string user { get; set; }
        public Guid keyuser { get; set; }
        public DateTime datetime { get; set; }
        public bool status { get; set; }
        public int type { get; set; }
        public int icon { get; set; }

        public string Title { get; set; }
        public string Color { get; set; }
        public string Sound { get; set; }
        public string Content { get; set; }
        public string attach1 { get; set; }
        public string attach2 { get; set; }

        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }

    }


    public class ReceiveNotification
    {
        public string user { get; set; }
        public int type { get; set; }
        public int icon { get; set; }
        public string attach1 { get; set; }
        public string attach2 { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public string Sound { get; set; }
        public string Content { get; set; }

        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
    }
}
