using System;

namespace DaisyStudy.Data.Entities
{
    public class Chat
    {
        public int Chat_ID { set; get; }
        public int Class_ID { set; get; }
        public Class Class { set; get; }
        public Guid User_ID { set; get; }
        public AppUser AppUser { set; get; }
        public String Content { set; get; }
        public DateTime DatetimeCreated { set; get; }
        public int Likes { set; get; }
        public int Dislikes { set; get; }
    }
}

