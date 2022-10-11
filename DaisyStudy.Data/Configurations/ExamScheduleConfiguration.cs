using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class ExamScheduleConfiguaration : IEntityTypeConfiguration<ExamSchedule>
    {
        public void Configure(EntityTypeBuilder<ExamSchedule> builder)
        {
            builder.ToTable("ExamSchedules");

            builder.HasKey(x => x.ExamSchedule_ID);

            builder.Property(x => x.ExamSchedule_ID).UseIdentityColumn();
            builder.Property(x => x.Class_ID).IsRequired();
            builder.Property(x => x.ExamScheduleName).IsRequired();
            builder.Property(x => x.DatetimeCreated).IsRequired();
            builder.Property(x => x.ExamDatetime).IsRequired();
            builder.Property(x => x.ExamTime).IsRequired();

            builder.HasOne(x => x.Class).WithMany(x => x.ExamSchedules).HasForeignKey(x => x.ExamSchedule_ID);
        }
    }
}

