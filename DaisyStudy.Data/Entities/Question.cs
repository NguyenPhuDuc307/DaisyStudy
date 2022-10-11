using System;
namespace DaisyStudy.Data.Entities
{
    public class Question
    {
        public int Question_ID { set; get; }
        public int ExamSchedule_ID { set; get; }
        public ExamSchedule ExamSchedule { set; get; }
        public String QuestionString { set; get; }
        public float Point { set; get; }
        public List<Answer> Answers { set; get; }
    }
}

