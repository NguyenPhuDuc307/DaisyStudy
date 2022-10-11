using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.ToTable("Homeworks");

            builder.HasKey(x => x.Homework_ID);

            builder.Property(x => x.Homework_ID).UseIdentityColumn();
            builder.Property(x => x.Class_ID).IsRequired();
            builder.Property(x => x.Deadline).IsRequired();
            builder.Property(x => x.DatetimeCreated).IsRequired();

            builder.HasOne(x => x.Class).WithMany(x=> x.Homeworks).HasForeignKey(x => x.Class_ID);
        }
    }
}

