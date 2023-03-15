namespace PrototipoEcommerce.Domain.Produtos.Entities;

public class Produto
{
    public long Id { get; set; }
    public string Nome { get; set; } = default!;
    public decimal Valor { get; set; }
    public Promocao? Promocao { get; set; }
}
