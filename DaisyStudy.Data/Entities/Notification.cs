using System;
namespace DaisyStudy.Data.Entities
{
    public class Notification
    {
        public int Notification_ID { set; get; }
        public int Class_ID { set; get; }
        public Class Class { set; get; }
        public String Title { set; get; }
        public String Image { set; get; }
        public String Content { set; get; }
        public DateTime DatetimeCreated { set; get; }
    }
}