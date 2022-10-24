using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts");

            builder.HasKey(x => x.ContactID);

            builder.Property(x => x.ContactID).UseIdentityColumn();
            builder.Property(x => x.CustomerName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(200).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Message).IsRequired();
        }
    }
}

