using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");

            builder.HasKey(x => x.MessageID);

            builder.Property(x => x.MessageID).UseIdentityColumn();
            
            builder.HasOne(x => x.FromUser).WithMany(x => x.Messages).HasForeignKey(x => x.FromUserID);
            builder.HasOne(x => x.ToRoom).WithMany(x => x.Messages).HasForeignKey(x => x.ToRoomID).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}

