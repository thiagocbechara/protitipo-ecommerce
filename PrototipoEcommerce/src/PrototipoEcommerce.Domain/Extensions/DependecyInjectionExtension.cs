using Microsoft.Extensions.DependencyInjection;
using PrototipoEcommerce.Domain.Carrinhos.Services;
using PrototipoEcommerce.Domain.Produtos.Services;

namespace PrototipoEcommerce.Domain.Extensions;

public static class DependecyInjectionExtension
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddTransient<ICarrinhoService, CarrinhoService>();
        services.AddTransient<IProdutoService, ProdutoService>();
        return services;
    }
}
