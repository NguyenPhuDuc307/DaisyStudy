using System;
namespace DaisyStudy.Data.Entities
{
    public class Notification
    {
        public int NotificationID { set; get; }
        public int ClassID { set; get; }
        public Class Class { set; get; }
        public String Title { set; get; }
        public String Image { set; get; }
        public String Content { set; get; }
        public DateTime DateTimeCreated { set; get; }
        public List<Comment> Comments { set; get; }
        public List<NotificationImage> NotificationImages { set; get; }
    }
}