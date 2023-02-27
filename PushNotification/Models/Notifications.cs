using System;
using System.ComponentModel.DataAnnotations;

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
        public int user { get; set; }
        public string type { get; set; }
        public int icon { get; set; }
        public string attach1 { get; set; }
        public string attach2 { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public string Sound { get; set; }
        public string Content { get; set; }

        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public VosuliInfo Reserve3 { get; set; }
    }

    public class VosuliInfo
    {
        public int ID_ListDetails { get; set; }
        public bool TypeQate { get; set; }
        public string NameFamily { get; set; }
        public string Eshterak { get; set; }
        public string CodeAddress { get; set; }
        public string SerialKontor { get; set; }
        public string Address { get; set; }
        public string Alamak { get; set; }
        public string Mobile { get; set; }
        public string MobileHamahangi { get; set; }
        public bool SpecilOrder1 { get; set; }
        public bool SpecilOrder2 { get; set; }
        public string SpecialOrder_Bywho { get; set; }
        public string SpecialOrder_Semat { get; set; }


    }
}
