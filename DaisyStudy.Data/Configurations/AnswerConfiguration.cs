using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers");

            builder.HasKey(x => x.Answer_ID);

            builder.Property(x => x.Question_ID).IsRequired();
            builder.Property(x => x.AnswerString).IsRequired();
            builder.Property(x => x.IsCorrect).IsRequired().HasDefaultValue(false);

            builder.HasOne(x => x.Question).WithMany(x => x.Answers).HasForeignKey(x => x.Question_ID);
        }
    }
}

