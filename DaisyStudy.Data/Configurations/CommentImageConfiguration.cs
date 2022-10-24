using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class CommentImageConfiguration : IEntityTypeConfiguration<CommentImage>
    {
        public void Configure(EntityTypeBuilder<CommentImage> builder)
        {
            builder.ToTable("CommentImages");

            builder.HasKey(x => x.ImageID);

            builder.Property(x => x.CommentID).IsRequired();
            builder.Property(x => x.ImageID).UseIdentityColumn();
            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired();

            builder.HasOne(x => x.Comment).WithMany(x => x.CommentImages).HasForeignKey(x => x.CommentID);
        }
    }
}

