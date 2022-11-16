namespace DaisyStudy.Data.Entities
{
    public class Room
    {
        public int Id { set; get; }
        public AppUser? Admin { set; get; }
        public string? Name { set; get; }
        public ICollection<Message>? Messages { set; get; }
    }
}

