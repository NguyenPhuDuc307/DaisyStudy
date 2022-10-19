using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class StudentExamConfiguration : IEntityTypeConfiguration<StudentExam>
    {
        public void Configure(EntityTypeBuilder<StudentExam> builder)
        {
            builder.ToTable("StudentExams");

            builder.HasKey(x => x.StudentExam_ID);

            builder.Property(x => x.StudentExam_ID).UseIdentityColumn();
            builder.Property(x => x.ExamSchedule_ID).IsRequired();
            builder.Property(x => x.Student_ID).IsRequired();
            builder.Property(x => x.Mark).IsRequired();
            builder.Property(x => x.DateTimeStudentExam).IsRequired();

            builder.HasOne(x => x.ExamSchedule).WithMany(x => x.StudentExams).HasForeignKey(x => x.ExamSchedule_ID);
            builder.HasOne(x => x.Student).WithMany(x => x.StudentExams).HasForeignKey(x => x.Student_ID);
        }
    }
}

