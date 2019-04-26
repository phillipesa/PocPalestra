using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PocPalestra.Domain.Core.Events;
using PocPalestra.Infra.Data.Extensions;

namespace PocPalestra.Infra.Data.Mappings
{
    public class StoredEventMap : EntityTypeConfiguration<StoredEvent>
    {
        public override void Map(EntityTypeBuilder<StoredEvent> builder)
        {
            builder.Property(c => c.TimeStamp)
                .HasColumnName("CreationDate");

            builder.Property(c => c.MessageType)
                .HasColumnName("Action")
                .HasColumnType("varchar(100)");

        }
    }
}
