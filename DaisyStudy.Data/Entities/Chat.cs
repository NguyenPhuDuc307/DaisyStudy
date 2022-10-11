using System;
using System.Reflection.Metadata;

namespace DaisyStudy.Data.Entities
{
    public class Chat
    {
        public int Class_ID { set; get; }
        public Guid User_ID { set; get; }
        public String Content { set; get; }
        public DateTime DatetimeCreated { set; get; }
        public int Likes { set; get; }
        public int Dislikes { set; get; }
    }
}

