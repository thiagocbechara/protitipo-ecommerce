using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrototipoEcommerce.Database.Context;
using PrototipoEcommerce.Database.Entities;
using PrototipoEcommerce.Domain.Carrinhos.Entities;
using PrototipoEcommerce.Domain.Carrinhos.Repositories;

namespace PrototipoEcommerce.Database.Repositories;

internal class CarrinhoRepository : ICarrinhoRepository
{
    private readonly PrototipoContext _context;
    private readonly IMapper _mapper;

    public CarrinhoRepository(PrototipoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Carrinho> AdicionarProdutoAsync(long carrinhoId, long produtoId)
    {
        var produto = new ItemCarrinhoEntity
        {
            CarrinhoId = carrinhoId,
            ProdutoId = produtoId,
            Quantidade = 1
        };
        await _context.ItensCarrinho.AddAsync(produto);
        await _context.SaveChangesAsync();
        return await SelecionarAsync(carrinhoId);
    }

    public async Task<Carrinho> AtualizarQuantidadeItemAsync(long carrinhoId, long itemId, long quantidade)
    {
        await _context.ItensCarrinho
            .Where(c => c.CarrinhoId == carrinhoId && c.Id == itemId)
            .ExecuteUpdateAsync(s => s.SetProperty(c => c.Quantidade, c => quantidade));
        return await SelecionarAsync(carrinhoId);
    }

    public async Task<Carrinho> RemoverItemAsync(long carrinhoId, long itemId)
    {
        await _context.ItensCarrinho
            .Where(c => c.CarrinhoId == carrinhoId && c.Id == itemId)
            .ExecuteDeleteAsync();
        return await SelecionarAsync(carrinhoId);
    }

    public async Task<Carrinho> SelecionarAsync(long? id)
    {
        var query = _context.Carrinhos
                    .Include(c => c.Itens)
                        .ThenInclude(i => i.Produto)
                            .ThenInclude(p => p.Promocao)
                    .AsNoTrackingWithIdentityResolution();

        CarrinhoEntity? entity = id.HasValue ?
            await query.FirstOrDefaultAsync(c => c.Id == id) :
            await query.OrderBy(c => c.Id).FirstOrDefaultAsync();
        
        return entity is null ? 
            await CriarAsync() :
            _mapper.Map<Carrinho>(entity);
    }

    public async Task<Carrinho> CriarAsync(params long[] produtosId)
    {
        var carrinho = new CarrinhoEntity();
        if (produtosId.Length > 0)
        {
            carrinho.Itens = produtosId.Select(id => new ItemCarrinhoEntity { ProdutoId = id });
        }
        var entry = await _context.Carrinhos.AddAsync(carrinho);
        await _context.SaveChangesAsync();

        var entity = await _context.Carrinhos
                    .Include(c => c.Itens)
                        .ThenInclude(i => i.Produto)
                            .ThenInclude(p => p.Promocao)
                    .AsNoTrackingWithIdentityResolution()
                    .FirstOrDefaultAsync(c => c.Id == entry.Entity.Id);

        return _mapper.Map<Carrinho>(entity);
    }
}
