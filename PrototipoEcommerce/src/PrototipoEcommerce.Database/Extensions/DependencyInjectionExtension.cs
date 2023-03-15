using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrototipoEcommerce.Database.Context;
using PrototipoEcommerce.Database.Profiles;
using PrototipoEcommerce.Database.Repositories;
using PrototipoEcommerce.Domain.Carrinhos.Repositories;
using PrototipoEcommerce.Domain.Produtos.Repositories;

namespace PrototipoEcommerce.Database.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PrototipoContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")));

        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();

        services.AddAutoMapper(typeof(DatabaseProfile));
        return services;
    }
}
