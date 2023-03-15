using PrototipoEcommerce.Domain.Produtos.Entities;

namespace PrototipoEcommerce.Domain.Produtos.Repositories;

public interface IProdutoRepository
{
    Task CriarAsync(Produto produto);
    Task<bool> ExisteAsync(long id);
    Task<int> AtualizarAsync(Produto produto);
    Task<int> RemoverAsync(long id);
    Task<Produto> SelecionarAsync(long id);
    Task<IEnumerable<Produto>> PaginarAsync(int pagina, int resultadosPorPagina);
    Task<IEnumerable<Promocao>> ListarPromocaoAsync();
}
