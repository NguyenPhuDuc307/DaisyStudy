using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class ExamResultDetailConfiguration : IEntityTypeConfiguration<ExamResultDetail>
    {
        public void Configure(EntityTypeBuilder<ExamResultDetail> builder)
        {
            builder.ToTable("ExamResultDetails");

            builder.HasKey(x => x.ExamResultDetail_ID);

            builder.Property(x => x.ExamResult_ID).IsRequired();
            builder.Property(x => x.Question_ID).IsRequired();
            builder.Property(x => x.Answer_ID).IsRequired();
            builder.Property(x => x.IsCorrect).IsRequired();

            builder.HasOne(x => x.ExamResult).WithMany(x => x.ExamResultDetails).HasForeignKey(x => x.ExamResult_ID);
            builder.HasOne(x => x.Question).WithMany(x => x.ExamResultDetails).HasForeignKey(x => x.Question_ID);
            builder.HasOne(x => x.Answer).WithMany(x => x.ExamResultDetails).HasForeignKey(x => x.Answer_ID);
        }
    }
}

