using PrototipoEcommerce.Domain.Produtos.Entities;

namespace PrototipoEcommerce.Domain.Produtos.Services;

public interface IProdutoService
{
    Task AtualizarAsync(Produto produto);
    Task CriarAsync(string nome, decimal valor, long? promocaoId = null);
    Task<IEnumerable<Produto>> ListarAsync(int pagina, int resultadosPorPagina);
    Task RemoverAsync(long produtoId);
    Task<Produto> SelecionarAsync(long produtoId);
    Task<IEnumerable<Promocao>> ListarPromocaoAsync();
}