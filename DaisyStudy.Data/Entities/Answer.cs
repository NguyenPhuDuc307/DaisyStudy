using System;
namespace DaisyStudy.Data.Entities
{
    public class Answer
    {
        public int Answer_ID { set; get; }
        public int Question_ID { set; get; }
        public Question Question { set; get; }
        public String AnswerString { set; get; }
        public bool IsCorrect { set; get; }
        public List<StudentExamDetail> StudentExamDetails { set; get; }
    }
}