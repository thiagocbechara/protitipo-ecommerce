using PrototipoEcommerce.Domain.Carrinhos.Entities;
using PrototipoEcommerce.Domain.Carrinhos.Repositories;

namespace PrototipoEcommerce.Domain.Carrinhos.Services;

internal class CarrinhoService : ICarrinhoService
{
    private readonly ICarrinhoRepository _repository;

    public CarrinhoService(ICarrinhoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Carrinho> AdicionarItemAsync(long produtoId, long? carrinhoId = null)
    {
        var carrinho = await _repository.SelecionarAsync(carrinhoId);
        var item = carrinho.Itens.FirstOrDefault(i => i.Produto.Id == produtoId);

        var task = item is not null ?
            _repository.AtualizarQuantidadeItemAsync(carrinho.Id, item.Id, item.Quantidade + 1) :
            _repository.AdicionarProdutoAsync(carrinho.Id, produtoId);

        return await task;
    }

    public Task<Carrinho> RemoverItemAsync(long carrinhoId, long itemId)
        => _repository.RemoverItemAsync(carrinhoId, itemId);

    public Task<Carrinho> SelecionarAsync(long? id = null) =>
        _repository.SelecionarAsync(id);
}
