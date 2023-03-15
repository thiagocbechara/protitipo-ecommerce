using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrototipoEcommerce.Database.Entities;

namespace PrototipoEcommerce.Database.Configurations;

internal class ItemCarrinhoEntityConfiguration : IEntityTypeConfiguration<ItemCarrinhoEntity>
{
    public void Configure(EntityTypeBuilder<ItemCarrinhoEntity> builder)
    {
        builder.ToTable("ItemCarrinho");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Quantidade).IsRequired();
        builder.Property(i => i.ProdutoId).IsRequired();
        builder.Property(i => i.CarrinhoId).IsRequired();

        builder.HasOne(i => i.Produto)
            .WithMany(p => p.ItensCarrinho)
            .HasForeignKey(i => i.ProdutoId);

        builder.HasOne(i => i.Carrinho)
            .WithMany(c => c.Itens)
            .HasForeignKey(i => i.CarrinhoId);
    }
}
