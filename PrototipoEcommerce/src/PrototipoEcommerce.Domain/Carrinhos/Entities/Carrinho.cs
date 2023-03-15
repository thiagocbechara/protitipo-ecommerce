namespace PrototipoEcommerce.Domain.Carrinhos.Entities;

public class Carrinho
{
    public long Id { get; set; }
    public ICollection<ItemCarrinho> Itens { get; set; } = default!;
    public decimal ValorTotal => Itens.Sum(p => p.ValorTotal());
}
