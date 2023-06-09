﻿using System;
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

            builder.HasKey(x => new { x.HomeworkID , x.StudentID});

            builder.Property(x=> x.Mark).IsRequired().HasDefaultValue(0);
            builder.Property(x=> x.Mark).IsRequired();

            builder.HasOne(x => x.Homework).WithMany(x => x.Submissions).HasForeignKey(x => x.HomeworkID);
            builder.HasOne(x => x.Student).WithMany(x => x.Submissions).HasForeignKey(x => x.StudentID);
        }
    }
}

