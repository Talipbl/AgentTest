using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Modules.Catalog.Core.Entities;
using OrderManagementSystem.Modules.Catalog.Core.Services;
using OrderManagementSystem.Modules.Catalog.Infrastructure.Data;

namespace OrderManagementSystem.Modules.Catalog.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly CatalogDbContext _dbContext;

    public CategoryService(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Category>> GetCategoriesAsync()
    {
        return await _dbContext.Categories.AsNoTracking().ToListAsync();
    }
}
