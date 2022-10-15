using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class ClassDetailConfiguration : IEntityTypeConfiguration<ClassDetail>
    {
        public void Configure(EntityTypeBuilder<ClassDetail> builder)
        {
            builder.ToTable("ClassDetails");

            builder.HasKey(x => new { x.Class_ID , x.User_ID});

            builder.HasOne(x => x.Class).WithMany(x => x.ClassDetails).HasForeignKey(x => x.Class_ID);
            builder.HasOne(x => x.User).WithMany(x => x.ClassDetails).HasForeignKey(x => x.User_ID);
        }
    }
}

