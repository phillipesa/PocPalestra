using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PocPalestra.Domain.Palestras;
using PocPalestra.Infra.Data.Extensions;

namespace PocPalestra.Infra.Data.Mappings
{
    public class PalestraMapping : EntityTypeConfiguration<Palestra>
    {
        public override void Map(EntityTypeBuilder<Palestra> builder)
        {
            builder.Property(e => e.Nome)
               .HasColumnType("varchar(150)")
               .IsRequired();

            builder.Property(e => e.DescricaoAbrev)
                .HasColumnType("varchar(150)");

            builder.Property(e => e.Descricao)
                .HasColumnType("varchar(max)");

            builder.Property(e => e.NomeEmpresa)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Ignore(e => e.ValidationResult);

            builder.Ignore(e => e.Tags);

            builder.Ignore(e => e.CascadeMode);

            builder.ToTable("Palestras");

            builder.HasOne(e => e.Organizador)
                .WithMany(o => o.Palestras)
                .HasForeignKey(e => e.OrganizadorId);

            builder.HasOne(e => e.Categoria)
                .WithMany(e => e.Palestra)
                .HasForeignKey(e => e.CategoriaId)
                .IsRequired(false);
        }
    }
}
