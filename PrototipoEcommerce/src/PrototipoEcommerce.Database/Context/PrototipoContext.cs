using Microsoft.EntityFrameworkCore;
using PrototipoEcommerce.Database.Entities;

namespace PrototipoEcommerce.Database.Context;

internal class PrototipoContext : DbContext
{
    public PrototipoContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ProdutoEntity> Produtos { get; set; }
    public DbSet<PromocaoEntity> Promocoes { get; set; }
    public DbSet<CarrinhoEntity> Carrinhos { get; set; }
    public DbSet<ItemCarrinhoEntity> ItensCarrinho { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PrototipoContext).Assembly);

        modelBuilder.Entity<PromocaoEntity>()
            .HasData(
            new PromocaoEntity { Id = 1, Nome = "3 por R$ 10,00", QuantidadeParaAplicar = 3, Tipo = 0, Valor = 10M },
            new PromocaoEntity { Id = 2, Nome = "Leve 2 e Pague 1", QuantidadeParaAplicar = 2, Tipo = 1, Valor = .5M }
            );
    }
}
