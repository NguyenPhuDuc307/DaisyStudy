using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class ExamPaperConfiguration : IEntityTypeConfiguration<ExamPaper>
    {
        public void Configure(EntityTypeBuilder<ExamPaper> builder)
        {
            builder.ToTable("ExamPapers");

            builder.HasKey(x => x.ExamPaper_ID);

            builder.Property(x => x.ExamPaper_ID).UseIdentityColumn();
            builder.Property(x => x.ExamSchedule_ID).IsRequired();
            builder.Property(x => x.Question_ID).IsRequired();

            builder.HasOne(x => x.ExamSchedule).WithMany(x => x.ExamPapers).HasForeignKey(x => x.ExamSchedule_ID);
            builder.HasOne(x => x.Question).WithMany(x => x.ExamPapers).HasForeignKey(x => x.Question_ID);
        }
    }
}

