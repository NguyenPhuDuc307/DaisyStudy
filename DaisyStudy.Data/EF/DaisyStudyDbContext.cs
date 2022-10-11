using DaisyStudy.Data.Configurations;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DaisyStudy.Data.EF
{
    public class DaisyStudyDbContext : DbContext
    {
        public DaisyStudyDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguaration());
            modelBuilder.ApplyConfiguration(new ClassConfiguration());
            modelBuilder.ApplyConfiguration(new ClassDetailConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguaration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new ExamResultConfiguration());
            modelBuilder.ApplyConfiguration(new ExamResultDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ExamScheduleConfiguaration());
            modelBuilder.ApplyConfiguration(new HomeworkConfiguration());
            modelBuilder.ApplyConfiguration(new NotifycationConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new StudentExamConfiguration());
            modelBuilder.ApplyConfiguration(new SubmissionConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<Answer> Answer { set; get; }
        public DbSet<AppConfig> AppConfig { set; get; }
        public DbSet<Chat> Chat { set; get; }
        public DbSet<Class> Class { set; get; }
        public DbSet<ClassDetail> ClassDetail { set; get; }
        public DbSet<Comment> Comment { set; get; }
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