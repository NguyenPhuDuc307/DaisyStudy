using System;
namespace DaisyStudy.Data.Entities
{
    public class StudentExam
    {
        public int StudentExam_ID { set; get; }
        
        public ExamResult ExamResult { set; get; }
        public int ExamSchedule_ID { set; get; }
        public ExamSchedule ExamSchedule { set; get; }
        public int Student_ID { set; get; }
    }
}

