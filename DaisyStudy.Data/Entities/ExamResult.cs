using System;
namespace DaisyStudy.Data.Entities
{
    public class ExamResult
    {
        public int ExamResult_ID { set; get; }
        public float Mark { set; get; }
        public String Note { set; get; }
        public List<StudentExam> StudentExams { set; get; }
        public List<ExamResultDetail> ExamResultDetails { set; get; }
    }
}