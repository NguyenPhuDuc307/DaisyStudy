using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class AppConfigConfiguration: IEntityTypeConfiguration<AppConfig>
    {
        public void Configure(EntityTypeBuilder<AppConfig> buider)
        {
            buider.ToTable("AppConfigs");

            buider.HasKey(x => x.Key);

            buider.Property(x => x.Value).IsRequired();
        }
    }
}

