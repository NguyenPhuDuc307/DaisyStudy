using System;
namespace DaisyStudy.Data.Entities
{
    public class ClassDetail
    {
        public int Class_ID { set; get; }
        public Class Class { set; get; }
        public Guid Student_ID { set; get; }
        public String Note { set; get; }
    }
}