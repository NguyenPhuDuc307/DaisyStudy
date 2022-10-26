using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class ClassImageConfiguration : IEntityTypeConfiguration<ClassImage>
    {
        public void Configure(EntityTypeBuilder<ClassImage> builder)
        {
            builder.ToTable("ClassImages");

            builder.HasKey(x => x.ImageID);

            builder.Property(x => x.ClassID).IsRequired();
            builder.Property(x => x.ImageID).UseIdentityColumn();
            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired();

            builder.HasOne(x => x.Class).WithMany(x => x.ClassImages).HasForeignKey(x => x.ClassID);
        }
    }
}

