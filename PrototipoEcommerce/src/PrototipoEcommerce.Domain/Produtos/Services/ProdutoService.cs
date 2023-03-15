using PrototipoEcommerce.Domain.Exceptions;
using PrototipoEcommerce.Domain.Produtos.Entities;
using PrototipoEcommerce.Domain.Produtos.Repositories;

namespace PrototipoEcommerce.Domain.Produtos.Services;

internal class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repository;

    public ProdutoService(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public Task CriarAsync(string nome, decimal valor, long? promocaoId = null)
    {
        var produto = new Produto
        {
            Nome = nome,
            Valor = valor
        };

        if(promocaoId.HasValue)
        {
            produto.Promocao = new Promocao { Id = promocaoId.Value };
        }
        return _repository.CriarAsync(produto);
    }

    public async Task AtualizarAsync(Produto produto)
    {
        if (!await _repository.ExisteAsync(produto.Id))
        {
            throw new DomainException($"Produto {produto.Id} não encontrado");
        }
        await _repository.AtualizarAsync(produto);
    }

    public Task RemoverAsync(long produtoId) => _repository.RemoverAsync(produtoId);

    public Task<Produto> SelecionarAsync(long produtoId) => _repository.SelecionarAsync(produtoId);

    public Task<IEnumerable<Produto>> ListarAsync(int pagina, int resultadosPorPagina) =>
        _repository.PaginarAsync(pagina, resultadosPorPagina);

    public Task<IEnumerable<Promocao>> ListarPromocaoAsync() =>
        _repository.ListarPromocaoAsync();
}
