using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagementSystem.Modules.Catalog.Core.Entities;

namespace OrderManagementSystem.Modules.Catalog.Core.Services;

public interface IProductService
{
    Task<IReadOnlyList<Product>> GetProductsAsync();
    Task<Product?> GetByIdAsync(Guid id);
}
