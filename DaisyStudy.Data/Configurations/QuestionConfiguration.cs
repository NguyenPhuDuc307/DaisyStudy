using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");

            builder.HasKey(x => x.QuestionID);

            builder.Property(x => x.QuestionID).UseIdentityColumn();
            builder.Property(x => x.QuestionString).IsRequired();
            builder.Property(x => x.Point).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired();
            builder.HasOne(x => x.ExamSchedule).WithMany(x => x.Questions).HasForeignKey(x => x.ExamScheduleID);
        }
    }
}

