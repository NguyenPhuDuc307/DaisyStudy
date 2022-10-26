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

            builder.HasKey(x => new { x.ClassID , x.UserID});

            builder.HasOne(x => x.Class).WithMany(x => x.ClassDetails).HasForeignKey(x => x.ClassID);
            builder.HasOne(x => x.User).WithMany(x => x.ClassDetails).HasForeignKey(x => x.UserID);
        }
    }
}

