using System;
namespace DaisyStudy.Data.Entities
{
    public class ExamPaper
    {
        public int ExamPaper_ID { set; get; }
        public int ExamSchedule_ID { set; get; }
        public ExamSchedule ExamSchedule { set; get; }
        public int Question_ID { set; get; }
        public Question Question { set; get; }
    }
}

