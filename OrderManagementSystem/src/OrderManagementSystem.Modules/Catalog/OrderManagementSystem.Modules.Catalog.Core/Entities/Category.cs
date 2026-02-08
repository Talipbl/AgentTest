using System;
using System.Collections.Generic;

namespace OrderManagementSystem.Modules.Catalog.Core.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = new();
}
