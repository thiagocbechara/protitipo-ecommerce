namespace PrototipoEcommerce.Database.Entities;

internal class PromocaoEntity
{
    public long Id { get; set; }
    public string Nome { get; set; } = default!;
    public int QuantidadeParaAplicar { get; set; }
    public int Tipo { get; set; }
    public decimal Valor { get; set; }

    public virtual IEnumerable<ProdutoEntity> Produtos { get; set; } = default!;
}
