using System;
namespace DaisyStudy.Data.Entities
{
    public class Submission
    {
        public int HomeworkID { set; get; }
        public Homework? Homework { set; get; }
        public Guid StudentID { set; get; }
        public AppUser? Student { set; get; }
        public float Mark { set; get; }
        public string? Note { set; get; }
        public string? Description { set; get; }
        public DateTime SubmissionDateTime { set; get; }
        public DateTime? DateTimeUpdated { set; get; }
        public Delay Delay { set; get; }
    }
}

