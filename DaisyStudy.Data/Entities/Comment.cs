using System;
namespace DaisyStudy.Data.Entities
{
    public class Comment
    {
        public int Notification_ID { set; get; }
        public Guid User_ID { set; get; }
        public String Content { set; get; }
        public DateTime DatetimeCreated { set; get; }
        public int Likes { set; get; }
        public int Dislikes { set; get; }
    }
}

