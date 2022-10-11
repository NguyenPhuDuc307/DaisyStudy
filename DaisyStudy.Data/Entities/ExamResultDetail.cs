using System;
namespace DaisyStudy.Data.Entities
{
    public class ExamResultDetail
    {
        public int ExamResultDetail_ID { set; get; }
        public int ExamResult_ID { set; get; }
        public ExamResult ExamResult { set; get; }
        public int Question_ID { set; get; }
        public Question Question { set; get; }
        public int Answer_ID { set; get; }
        public Answer Answer { set; get; }
        public bool IsCorrect { set; get; }
    }
}