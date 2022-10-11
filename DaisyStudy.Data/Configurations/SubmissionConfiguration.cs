using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder.ToTable("Submissions");

            builder.HasKey(x => new { x.Homework_ID });

            builder.HasOne(x => x.Homework).WithMany(x => x.Submissions).HasForeignKey(x => x.Homework_ID);
        }
    }
}

