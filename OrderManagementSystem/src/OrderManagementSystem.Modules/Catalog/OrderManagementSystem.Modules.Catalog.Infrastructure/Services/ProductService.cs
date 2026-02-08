using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Modules.Catalog.Core.Entities;
using OrderManagementSystem.Modules.Catalog.Core.Services;
using OrderManagementSystem.Modules.Catalog.Infrastructure.Data;

namespace OrderManagementSystem.Modules.Catalog.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly CatalogDbContext _dbContext;

    public ProductService(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _dbContext.Products.AsNoTracking().ToListAsync();
    }

    public Task<Product?> GetByIdAsync(Guid id)
    {
        return _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(product => product.Id == id);
    }
}
