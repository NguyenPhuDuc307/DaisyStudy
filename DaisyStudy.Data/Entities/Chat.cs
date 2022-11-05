using System;

namespace DaisyStudy.Data.Entities
{
    public class Chat
    {
        public int ChatID { set; get; }
        public int ClassID { set; get; }
        public Class? Class { set; get; }
        public Guid UserID { set; get; }
        public AppUser? AppUser { set; get; }
        public String? Content { set; get; }
        public DateTime DateTimeCreated { set; get; }
        public int Likes { set; get; }
        public int Dislikes { set; get; }
        public List<ChatImage>? ChatImages { set; get; }
    }
}

