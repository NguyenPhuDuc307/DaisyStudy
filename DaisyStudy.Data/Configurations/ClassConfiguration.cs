using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("Classes");

            builder.HasKey(x => x.ID);

            builder.Property(x=> x.ID).UseIdentityColumn();
            builder.Property(x => x.ClassID).IsUnicode(false).IsRequired();
            builder.Property(x => x.ClassName).IsRequired();
            builder.Property(x => x.Tuition).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.isPublic).IsRequired();
            builder.Property(x => x.ViewCount).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.DateCreated).IsRequired();
        }
    }
}

