using System;
namespace DaisyStudy.Data.Entities
{
    public class ClassDetail
    {
        public int ClassID { set; get; }
        public Class Class { set; get; }
        public Guid UserID { set; get; }
        public AppUser User { set; get; }
        public String Note { set; get; }
    }
}