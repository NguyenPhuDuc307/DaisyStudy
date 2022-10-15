using System;
namespace DaisyStudy.Data.Entities
{
    public class Comment
    {
        public int Comment_ID { set; get; }
        public int Notification_ID { set; get; }
        public Notification Notification { set; get; }
        public Guid User_ID { set; get; }
        public AppUser AppUser { set; get; }
        public String Content { set; get; }
        public DateTime DatetimeCreated { set; get; }
        public int Likes { set; get; }
        public int Dislikes { set; get; }
    }
}

