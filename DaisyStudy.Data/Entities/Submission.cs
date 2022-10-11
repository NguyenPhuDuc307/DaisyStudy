using System;
namespace DaisyStudy.Data.Entities
{
    public class Submission
    {
        public int Homework_ID { set; get; }
        public Homework Homework { set; get; }
        public Guid Student_ID { set; get; }
        public String SubmissionName { set; get; }
        public float Mark { set; get; }
        public String Note { set; get; }
    }
}

