using System;
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

            builder.HasKey(x => x.Question_ID);

            builder.Property(x => x.Question_ID).UseIdentityColumn();
            builder.Property(x => x.QuestionString).IsRequired();
            builder.Property(x => x.Point).IsRequired().HasDefaultValue(0);
            builder.HasOne(x => x.ExamSchedule).WithMany(x => x.Questions).HasForeignKey(x => x.ExamSchedule_ID);
        }
    }
}

