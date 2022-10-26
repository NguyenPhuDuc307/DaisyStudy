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

            builder.HasKey(x => x.AnswerID);

            builder.Property(x => x.AnswerID).UseIdentityColumn();
            builder.Property(x => x.QuestionID).IsRequired();
            builder.Property(x => x.AnswerString).IsRequired();
            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired();
            builder.Property(x => x.IsCorrect).IsRequired().HasDefaultValue(false);

            builder.HasOne(x => x.Question).WithMany(x => x.Answers).HasForeignKey(x => x.QuestionID);
        }
    }
}

