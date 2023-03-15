using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrototipoEcommerce.Database.Context;
using PrototipoEcommerce.Database.Entities;
using PrototipoEcommerce.Domain.Exceptions;
using PrototipoEcommerce.Domain.Produtos.Entities;
using PrototipoEcommerce.Domain.Produtos.Repositories;

namespace PrototipoEcommerce.Database.Repositories;

internal class ProdutoRepository : IProdutoRepository
{
    private readonly PrototipoContext _context;
    private readonly IMapper _mapper;

    public ProdutoRepository(PrototipoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<int> AtualizarAsync(Produto produto) =>
        _context.Produtos
                .Where(p => p.Id == produto.Id)
                .ExecuteUpdateAsync(prop => prop
                    .SetProperty(p => p.Nome, p => produto.Nome)
                    .SetProperty(p => p.Valor, p => produto.Valor)
                    .SetProperty(p => p.PromocaoId, p => produto.Promocao != null && produto.Promocao.Id != 0 ? produto.Promocao.Id : null));

    public async Task CriarAsync(Produto produto)
    {
        var entity = _mapper.Map<ProdutoEntity>(produto);
        entity.Promocao = null!; // Para não atualizar a entidade Promoção

        await _context.Produtos.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public Task<bool> ExisteAsync(long id) =>
        _context.Produtos.AnyAsync(p => p.Id == id);

    public async Task<IEnumerable<Promocao>> ListarPromocaoAsync() =>
        _mapper.Map<IEnumerable<Promocao>>(
            await _context.Promocoes.AsNoTracking().ToListAsync());

    public async Task<IEnumerable<Produto>> PaginarAsync(int pagina, int resultadosPorPagina) =>
        await _context.Produtos
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(p => p.Id)
            .Include(p => p.Promocao)
            .Skip(resultadosPorPagina * (pagina - 1))
            .Take(resultadosPorPagina)
            .Select(p => _mapper.Map<Produto>(p))
            .ToListAsync();

    public Task<int> RemoverAsync(long id) =>
        _context.Produtos
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();

    public async Task<Produto> SelecionarAsync(long id)
    {
        var entity = await _context.Produtos
            .Include(p => p.Promocao)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (entity is null)
        {
            throw new NotFoundDomainException($"Produto '{id}' não encontrado.");
        }

        return _mapper.Map<Produto>(entity);
    }
}
