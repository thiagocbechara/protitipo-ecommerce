using PrototipoEcommerce.Domain.Carrinhos.Entities;

namespace PrototipoEcommerce.Domain.Carrinhos.Repositories;

public interface ICarrinhoRepository
{
    Task<Carrinho> AdicionarProdutoAsync(long carrinhoId, long produtoId);
    Task<Carrinho> AtualizarQuantidadeItemAsync(long carrinhoId, long itemId, long quantidade);
    Task<Carrinho> CriarAsync(params long[] produtosId);
    Task<Carrinho> RemoverItemAsync(long carrinhoId, long itemId);
    Task<Carrinho> SelecionarAsync(long? id);
}