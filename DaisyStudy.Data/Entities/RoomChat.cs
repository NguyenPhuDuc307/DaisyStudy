namespace DaisyStudy.Data.Entities
{
    public class RoomChat
    {
        public int RoomChatID { set; get; }
        public Guid AdminID { set; get; }
        public AppUser? Admin { set; get; }
        public string? RoomChatName { set; get; }
        public List<Message>? Messages { set; get; }
    }
}

