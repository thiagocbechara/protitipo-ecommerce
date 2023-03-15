using AutoMapper;
using PrototipoEcommerce.Domain.Produtos.Entities;
using PrototipoEcommerce.Web.Models;

namespace PrototipoEcommerce.Web.Profiles;

public class WebProfile : Profile
{
    public WebProfile()
    {
        CreateMap<Produto, ProdutoViewModel>()
            .ForMember(vm => vm.PromocaoId, opt => opt.MapFrom(p => p.Promocao.Id))
            .ReverseMap();

        CreateMap<Promocao, PromocaoViewModel>().ReverseMap();
    }
}
