using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class RoomChatConfiguration : IEntityTypeConfiguration<RoomChat>
    {
        public void Configure(EntityTypeBuilder<RoomChat> builder)
        {
            builder.ToTable("RoomChats");

            builder.HasKey(x => x.RoomChatID);

            builder.Property(x => x.RoomChatID).UseIdentityColumn();
            builder.Property(x => x.RoomChatName).IsRequired();
            
            builder.HasOne(x => x.Admin).WithMany(x => x.RoomChats).HasForeignKey(x => x.AdminID);
        }
    }
}

