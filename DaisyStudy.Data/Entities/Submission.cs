using System;
namespace DaisyStudy.Data.Entities
{
    public class Submission
    {
        public int HomeworkID { set; get; }
        public Homework Homework { set; get; }
        public Guid StudentID { set; get; }
        public AppUser Student { set; get; }
        public String SubmissionName { set; get; }
        public float Mark { set; get; }
        public String Note { set; get; }
        public String Description { set; get; }
        public DateTime SubmissionDateTime { set; get; }
    }
}

