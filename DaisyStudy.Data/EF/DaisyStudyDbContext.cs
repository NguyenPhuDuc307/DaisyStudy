using DaisyStudy.Data.Configurations;
using DaisyStudy.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DaisyStudy.Data.EF
{
    public class DaisyStudyDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DaisyStudyDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new ClassConfiguration());
            modelBuilder.ApplyConfiguration(new ClassDetailConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new StudentExamDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ExamScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new HomeworkConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new StudentExamConfiguration());
            modelBuilder.ApplyConfiguration(new SubmissionConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new ChatImageConfiguration());
            modelBuilder.ApplyConfiguration(new CommentImageConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationImageConfiguration());

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<Answer> Answers { set; get; }
        public DbSet<AppConfig> AppConfigs { set; get; }
        public DbSet<Chat> Chats { set; get; }
        public DbSet<Class> Classes { set; get; }
        public DbSet<ClassDetail> ClassDetails { set; get; }
        public DbSet<Comment> Comments { set; get; }
        public DbSet<Contact> Contacts { set; get; }
        public DbSet<ExamSchedule> ExamSchedules { set; get; }
        public DbSet<Homework> Homeworks { set; get; }
        public DbSet<Notification> Notifications { set; get; }
        public DbSet<Question> Questions { set; get; }
        public DbSet<StudentExam> StudentExams { set; get; }
        public DbSet<StudentExamDetail> StudentExamDetails { set; get; }
        public DbSet<Submission> Submissions { set; get; }
        public DbSet<Transaction> Transactions { set; get; }
        public DbSet<ChatImage> ChatImages { set; get; }
        public DbSet<CommentImage> CommentImages { set; get; }
        public DbSet<NotificationImage> NotificationImages { set; get; }
    }
}