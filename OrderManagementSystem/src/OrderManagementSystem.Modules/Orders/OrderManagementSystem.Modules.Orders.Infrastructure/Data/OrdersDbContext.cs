using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Modules.Orders.Core.Entities;

namespace OrderManagementSystem.Modules.Orders.Infrastructure.Data;

public class OrdersDbContext : DbContext
{
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
}
