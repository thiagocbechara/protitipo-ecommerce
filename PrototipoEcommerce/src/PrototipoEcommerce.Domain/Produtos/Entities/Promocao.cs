using PrototipoEcommerce.Domain.Produtos.Entities.Enums;

namespace PrototipoEcommerce.Domain.Produtos.Entities;

public class Promocao
{
    public long Id { get; set; }
    public string Nome { get; set; } = default!;
    public int QuantidadeParaAplicar { get; set; }
    public TipoPromocao Tipo { get; set; }
    public decimal Valor { get; set; }
}
