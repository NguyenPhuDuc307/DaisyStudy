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

            builder.HasKey(x => x.StudentExamID);

            builder.Property(x => x.StudentExamID).UseIdentityColumn();
            builder.Property(x => x.ExamScheduleID).IsRequired();
            builder.Property(x => x.StudentID).IsRequired();
            builder.Property(x => x.Mark).IsRequired();
            builder.Property(x => x.StudentExamDateTime).IsRequired();

            builder.HasOne(x => x.ExamSchedule).WithMany(x => x.StudentExams).HasForeignKey(x => x.ExamScheduleID);
            builder.HasOne(x => x.Student).WithMany(x => x.StudentExams).HasForeignKey(x => x.StudentID);
        }
    }
}

