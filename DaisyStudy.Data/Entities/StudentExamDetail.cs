using System;
namespace DaisyStudy.Data.Entities
{
    public class StudentExamDetail
    {
        public int StudentExamDetailID { set; get; }
        public int StudentExamID { set; get; }
        public StudentExam StudentExam { set; get; }
        public int AnswerID { set; get; }
        public Answer Answer { set; get; }
    }
}