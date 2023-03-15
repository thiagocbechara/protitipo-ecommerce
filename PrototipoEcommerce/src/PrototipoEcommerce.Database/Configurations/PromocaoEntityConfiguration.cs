using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrototipoEcommerce.Database.Entities;

namespace PrototipoEcommerce.Database.Configurations;

internal class PromocaoEntityConfiguration : IEntityTypeConfiguration<PromocaoEntity>
{
    public void Configure(EntityTypeBuilder<PromocaoEntity> builder)
    {
        builder.ToTable("Promocao");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nome).IsRequired();
        builder.Property(p => p.QuantidadeParaAplicar).IsRequired();
        builder.Property(p => p.Tipo).IsRequired();
        builder.Property(p => p.Valor).HasPrecision(10, 4).IsRequired();
    }
}
