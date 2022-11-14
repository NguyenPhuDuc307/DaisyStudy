namespace DaisyStudy.Data.Entities
{
    public class Message
    {
        public int MessageID { set; get; }
        public Guid FromUserID { set; get; }
        public AppUser? FromUser { set; get; }
        public int ToRoomID { set; get; }
        public RoomChat? ToRoom { set; get; }
        public string? Content { set; get; }
        public DateTime TimeStamp { set; get; }
    }
}

