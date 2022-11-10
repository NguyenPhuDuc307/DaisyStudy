using System;
namespace DaisyStudy.Data.Entities
{
    public class Comment
    {
        public int CommentID { set; get; }
        public int NotificationID { set; get; }
        public Notification? Notification { set; get; }
        public Guid UserID { set; get; }
        public AppUser? AppUser { set; get; }
        public String? Content { set; get; }
        public DateTime DateTimeCreated { set; get; }
        public List<CommentImage>? CommentImages { set; get; }
    }
}

