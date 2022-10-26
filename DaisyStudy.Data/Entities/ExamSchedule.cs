using System;
namespace DaisyStudy.Data.Entities
{
    public class ExamSchedule
    {
        public int ExamScheduleID { set; get; }
        public int ClassID { set; get; }
        public Class Class { set; get; }
        public String ExamScheduleName { set; get; }
        public DateTime DateTimeCreated { set; get; }
        public DateTime ExamDateTime { set; get; }
        public int ExamTime { set; get; }
        public String Image { set; get; }
        public List<StudentExam> StudentExams { set; get; }
        public List<Question> Questions { set; get; }
    }
}

