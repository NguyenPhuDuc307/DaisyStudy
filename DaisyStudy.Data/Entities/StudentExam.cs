using System;
namespace DaisyStudy.Data.Entities
{
    public class StudentExam
    {
        public int StudentExamID { set; get; }
        public int ExamScheduleID { set; get; }
        public ExamSchedule ExamSchedule { set; get; }
        public Guid StudentID { set; get; }
        public AppUser Student { set; get; }
        public float Mark { set; get; }
        public String Note { set; get; }
        public DateTime StudentExamDateTime { set; get; }
        public List<StudentExamDetail> StudentExamDetails { set; get; }
    }
}

