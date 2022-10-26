using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class StudentExamDetailConfiguration : IEntityTypeConfiguration<StudentExamDetail>
    {
        public void Configure(EntityTypeBuilder<StudentExamDetail> builder)
        {
            builder.ToTable("StudentExamDetails");

            builder.HasKey(x => x.StudentExamDetailID);

            builder.Property(x => x.StudentExamDetailID).UseIdentityColumn();
            builder.Property(x => x.StudentExamID).IsRequired();
            builder.Property(x => x.AnswerID).IsRequired();

            builder.HasOne(x => x.StudentExam).WithMany(x => x.StudentExamDetails).HasForeignKey(x => x.StudentExamID).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(x => x.Answer).WithMany(x => x.StudentExamDetails).HasForeignKey(x => x.AnswerID);
        }
    }
}

