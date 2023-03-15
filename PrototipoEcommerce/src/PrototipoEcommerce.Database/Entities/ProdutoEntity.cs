namespace PrototipoEcommerce.Database.Entities;

internal class ProdutoEntity
{
    public long Id { get; set; }
    public string Nome { get; set; } = default!;
    public decimal Valor { get; set; }
    public long? PromocaoId { get; set; }

    public PromocaoEntity? Promocao { get; set; }

    public virtual IEnumerable<ItemCarrinhoEntity> ItensCarrinho { get; set; } = default!;
}
