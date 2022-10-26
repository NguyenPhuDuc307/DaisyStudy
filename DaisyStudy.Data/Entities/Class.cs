using System;
namespace DaisyStudy.Data.Entities
{
    public class Class
    {
        public int ID { set; get; }
        public String ClassID { set; get; }
        public String ClassName { set; get; }
        public String Topic { set; get; }
        public String Image { set; get; }
        public String ClassRoom { set; get; }
        public String Description { set; get; }
        public String SEOClassName { set; get; }
        public String SEODescriptione { set; get; }
        public String SEOAlias { set; get; }
        public decimal Tuition { set; get; }
        public DateTime DateCreated { set; get; }
        public int ViewCount { set; get; }
        public Status Status { set; get; }
        public IsPublic isPublic { set; get; }
        public List<ClassDetail> ClassDetails { set; get; }
        public List<Homework> Homeworks { set; get; }
        public List<Notification> Notifications { set; get; }
        public List<ExamSchedule> ExamSchedules { set; get; }
        public List<Chat> Chats { set; get; }
        public List<ClassImage> ClassImages { set; get; }

    }
}

