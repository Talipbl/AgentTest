using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Modules.Catalog.Core.Services;
using OrderManagementSystem.Modules.Catalog.Infrastructure.Data;
using OrderManagementSystem.Modules.Catalog.Infrastructure.Services;

namespace OrderManagementSystem.Modules.Catalog.Api.DependencyInjection;

public static class CatalogModuleExtensions
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}
