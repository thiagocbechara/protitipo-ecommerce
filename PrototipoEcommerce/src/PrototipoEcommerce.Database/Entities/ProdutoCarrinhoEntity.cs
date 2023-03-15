namespace PrototipoEcommerce.Database.Entities;

internal class ItemCarrinhoEntity
{
    public long Id { get; set; }
    public int Quantidade { get; set; }
    public long ProdutoId { get; set; }
    public long CarrinhoId { get; set; }

    public ProdutoEntity Produto { get; set; } = default!;
    public CarrinhoEntity Carrinho { get; set; } = default!;
}
