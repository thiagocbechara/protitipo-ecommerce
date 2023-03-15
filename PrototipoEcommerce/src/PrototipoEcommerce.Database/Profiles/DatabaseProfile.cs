using AutoMapper;
using PrototipoEcommerce.Database.Entities;
using PrototipoEcommerce.Domain.Carrinhos.Entities;
using PrototipoEcommerce.Domain.Produtos.Entities;

namespace PrototipoEcommerce.Database.Profiles;

internal class DatabaseProfile : Profile
{
	public DatabaseProfile()
	{
		CreateMap<Carrinho, CarrinhoEntity>().ReverseMap();
		CreateMap<ItemCarrinho, ItemCarrinhoEntity>()
			.ForMember(e => e.ProdutoId, opt => opt.MapFrom(i => i.Produto.Id))
			.ForMember(e => e.CarrinhoId, opt => opt.MapFrom(i => i.Carrinho.Id))
			.ReverseMap();
		CreateMap<Produto, ProdutoEntity>()
			.ForMember(e => e.PromocaoId, opt => opt.MapFrom(p => p.Promocao.Id))
			.ReverseMap(); ;
		CreateMap<Promocao, PromocaoEntity>()
			.ForMember(e => e.Tipo, opt => opt.MapFrom(p => (int) p.Tipo))
			.ReverseMap();
	}
}
