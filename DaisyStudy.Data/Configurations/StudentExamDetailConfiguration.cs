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

            builder.HasKey(x => x.StudentExamDetail_ID);

            builder.Property(x => x.StudentExamDetail_ID).UseIdentityColumn();
            builder.Property(x => x.StudentExam_ID).IsRequired();
            builder.Property(x => x.Answer_ID).IsRequired();

            builder.HasOne(x => x.StudentExam).WithMany(x => x.StudentExamDetails).HasForeignKey(x => x.StudentExam_ID).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(x => x.Answer).WithMany(x => x.StudentExamDetails).HasForeignKey(x => x.Answer_ID);
        }
    }
}

