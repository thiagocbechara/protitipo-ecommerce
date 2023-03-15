using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrototipoEcommerce.Database.Entities;

namespace PrototipoEcommerce.Database.Configurations
{
    internal class CarrinhoEntityConfiguration : IEntityTypeConfiguration<CarrinhoEntity>
    {
        public void Configure(EntityTypeBuilder<CarrinhoEntity> builder)
        {
            builder.ToTable("Carrinho");

            builder.HasIndex(c => c.Id);
        }
    }
}
