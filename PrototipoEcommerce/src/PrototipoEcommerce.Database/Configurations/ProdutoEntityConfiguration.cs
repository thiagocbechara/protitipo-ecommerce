using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrototipoEcommerce.Database.Entities;

namespace PrototipoEcommerce.Database.Configurations;

internal class ProdutoEntityConfiguration : IEntityTypeConfiguration<ProdutoEntity>
{
    public void Configure(EntityTypeBuilder<ProdutoEntity> builder)
    {
        builder.ToTable("Produto");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nome).IsRequired();
        builder.Property(p => p.Valor).HasPrecision(10, 2).IsRequired();
        builder.Property(p => p.PromocaoId).IsRequired(false);

        builder.HasOne(prod => prod.Promocao)
            .WithMany(promo => promo.Produtos)
            .HasForeignKey(prod => prod.PromocaoId);
    }
}
