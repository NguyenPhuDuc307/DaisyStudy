using System;
namespace DaisyStudy.Data.Entities
{
    public class Question
    {
        public int Question_ID { set; get; }
        public String QuestionString { set; get; }
        public float Point { set; get; }
        public List<ExamPaper> ExamPapers { set; get; }
        public List<Answer> Answers { set; get; }
        public List<ExamResultDetail> ExamResultDetails { set; get; }

    }
}

