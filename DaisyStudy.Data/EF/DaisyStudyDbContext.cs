using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DaisyStudy.Data.EF
{
    public class DaisyStudyDbContext : DbContext
    {
        public DaisyStudyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Answer> Answer { set; get; }
        public DbSet<AppConfig> AppConfig { set; get; }
        public DbSet<Chat> Chat { set; get; }
        public DbSet<Class> Class { set; get; }
        public DbSet<ClassDetail> ClassDetail { set; get; }
        public DbSet<Comment> Comment { set; get; }
        public DbSet<ExamPaper> ExamPaper { set; get; }
        public DbSet<ExamResult> ExamResult { set; get; }
        public DbSet<ExamResultDetail> ClaExamResultDetailss { set; get; }
        public DbSet<ExamSchedule> ExamSchedule { set; get; }
        public DbSet<Homework> Homework { set; get; }
        public DbSet<Notification> Notification { set; get; }
        public DbSet<Question> Question { set; get; }
        public DbSet<StudentExam> StudentExam { set; get; }
        public DbSet<Submission> Submission { set; get; }
        public DbSet<Transaction> Transaction { set; get; }
    }
}