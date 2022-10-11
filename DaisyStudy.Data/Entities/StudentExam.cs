using System;
namespace DaisyStudy.Data.Entities
{
    public class StudentExam
    {
        public int ExamSchedule_ID { set; get; }
        public ExamSchedule ExamSchedule { set; get; }
        public int Student_ID { set; get; }
        public int ExamResult_ID { set; get; }
        public ExamResult ExamResult { set; get; }
    }
}

