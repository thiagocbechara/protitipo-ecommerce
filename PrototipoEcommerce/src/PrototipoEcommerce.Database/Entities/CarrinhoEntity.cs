namespace PrototipoEcommerce.Database.Entities;

internal class CarrinhoEntity
{
    public long Id { get; set; }
    public virtual IEnumerable<ItemCarrinhoEntity> Itens { get; set; } = default!;
}
