using System;
namespace DaisyStudy.Data.Entities
{
    public class Homework
    {
        public int Homework_ID { set; get; }
        public int Class_ID { set; get; }
        public Class Class { set; get; }
        public String HomeworkName { set; get; }
        public String Description { set; get; }
        public DateTime DatetimeCreated { set; get; }
        public DateTime Deadline { set; get; }
        public List<Submission> Submissions { set; get; }
    }
}

