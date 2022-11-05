using Microsoft.AspNetCore.Identity;

namespace DaisyStudy.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime Dob { get; set; }
        public string? Avatar { get; set; }
        public decimal AccountBalance { get; set; }
        public List<ClassDetail>? ClassDetails { get; set; }
        public List<StudentExam>? StudentExams { get; set; }
        public List<Submission>? Submissions { get; set; }
        public List<Transaction>? Transactions { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Chat>? Chats { get; set; }
    }
}