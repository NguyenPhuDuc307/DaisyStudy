using System;
namespace DaisyStudy.Data.Entities
{
    public class Answer
    {
        public int AnswerID { set; get; }
        public int QuestionID { set; get; }
        public Question? Question { set; get; }
        public String? AnswerString { set; get; }
        public bool IsCorrect { set; get; }
        public string? ImagePath { set; get; }
        public long ImageFileSize { set; get; }
        public List<StudentExamDetail>? StudentExamDetails { set; get; }
    }
}