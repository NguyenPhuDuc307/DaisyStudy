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

            builder.HasKey(x => x.HomeworkID);

            builder.Property(x => x.HomeworkID).UseIdentityColumn();
            builder.Property(x => x.ClassID).IsRequired();
            builder.Property(x => x.Deadline).IsRequired();
            builder.Property(x => x.DateTimeCreated).IsRequired();

            builder.HasOne(x => x.Class).WithMany(x=> x.Homeworks).HasForeignKey(x => x.ClassID);
        }
    }
}

