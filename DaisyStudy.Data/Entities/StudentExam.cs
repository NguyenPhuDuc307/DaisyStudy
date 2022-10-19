using System;
namespace DaisyStudy.Data.Entities
{
    public class StudentExam
    {
        public int StudentExam_ID { set; get; }
        public int ExamSchedule_ID { set; get; }
        public ExamSchedule ExamSchedule { set; get; }
        public Guid Student_ID { set; get; }
        public AppUser Student { set; get; }
        public float Mark { set; get; }
        public String Note { set; get; }
        public DateTime DateTimeStudentExam { set; get; }
        public List<StudentExamDetail> StudentExamDetails { set; get; }
    }
}

