using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class ExamScheduleConfiguration : IEntityTypeConfiguration<ExamSchedule>
    {
        public void Configure(EntityTypeBuilder<ExamSchedule> builder)
        {
            builder.ToTable("ExamSchedules");

            builder.HasKey(x => x.ExamScheduleID);

            builder.Property(x => x.ExamScheduleID).UseIdentityColumn();
            builder.Property(x => x.ClassID).IsRequired();
            builder.Property(x => x.ExamScheduleName).IsRequired();
            builder.Property(x => x.DateTimeCreated).IsRequired();
            builder.Property(x => x.ExamDateTime).IsRequired();
            builder.Property(x => x.ExamTime).IsRequired();

            builder.HasOne(x => x.Class).WithMany(x => x.ExamSchedules).HasForeignKey(x => x.ClassID);
        }
    }
}

