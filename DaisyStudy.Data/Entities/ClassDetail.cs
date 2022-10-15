using System;
namespace DaisyStudy.Data.Entities
{
    public class ClassDetail
    {
        public int Class_ID { set; get; }
        public Class Class { set; get; }
        public Guid User_ID { set; get; }
        public AppUser User { set; get; }
        public String Note { set; get; }
    }
}