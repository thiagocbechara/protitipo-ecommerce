using PrototipoEcommerce.Domain.Carrinhos.Entities;

namespace PrototipoEcommerce.Domain.Carrinhos.Services;

public interface ICarrinhoService
{
    Task<Carrinho> SelecionarAsync(long? id = null);
    Task<Carrinho> AdicionarItemAsync(long produtoId, long? carrinhoId = null);
    Task<Carrinho> RemoverItemAsync(long carrinhoId, long itemId);
}