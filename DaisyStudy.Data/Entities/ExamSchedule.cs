using System;
namespace DaisyStudy.Data.Entities
{
    public class ExamSchedule
    {
        public int ExamSchedule_ID { set; get; }
        public int Class_ID { set; get; }
        public Class Class { set; get; }
        public String ExamScheduleName { set; get; }
        public DateTime DatetimeCreated { set; get; }
        public DateTime ExamDatetime { set; get; }
        public int ExamTime { set; get; }
        public String Image { set; get; }
        List<ExamPaper> ExamPapers { set; get; }
        List<StudentExam> StudentExams { set; get; }
    }
}

