using PrototipoEcommerce.Domain.Exceptions;
using PrototipoEcommerce.Domain.Produtos.Entities;
using PrototipoEcommerce.Domain.Produtos.Entities.Enums;

namespace PrototipoEcommerce.Domain.Carrinhos.Entities;

public class ItemCarrinho
{
    public long Id { get; set; }
    public int Quantidade { get; set; }
    public Produto Produto { get; set; } = default!;
    public Carrinho Carrinho { get; set; } = default!;
    public string? NomePromocao => Produto.Promocao?.Nome;

    public decimal ValorTotal()
    {
        if (Produto.Promocao is null || Produto.Promocao.Id == 0)
            return Quantidade * Produto.Valor;

        var quantidadeAplicarPromocao = Quantidade / Produto.Promocao.QuantidadeParaAplicar;
        var quantidadeSemAplicar = Quantidade - quantidadeAplicarPromocao * Produto.Promocao.QuantidadeParaAplicar;

        return quantidadeSemAplicar * Produto.Valor + ValorPromocao(quantidadeAplicarPromocao);
    }

    private decimal ValorPromocao(int quantidadeProdutos) => Produto.Promocao?.Tipo switch
    {
        TipoPromocao.ValorFixo => quantidadeProdutos * Produto.Promocao.Valor,
        TipoPromocao.PorcentagemDesconto => quantidadeProdutos * Produto.Promocao.QuantidadeParaAplicar * Produto.Valor * (1M - Produto.Promocao.Valor),
        _ => throw new DomainException($"Tipo de Promoção '{Produto.Promocao?.Tipo}' não suportado")
    };
}
