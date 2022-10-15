using System;
namespace DaisyStudy.Data.Entities
{
    public class StudentExamDetail
    {
        public int StudentExamDetail_ID { set; get; }
        public int StudentExam_ID { set; get; }
        public StudentExam StudentExam { set; get; }
        public int Answer_ID { set; get; }
        public Answer Answer { set; get; }
    }
}