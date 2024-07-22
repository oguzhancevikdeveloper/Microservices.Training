using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace Order.API.Models;

public class OrderAPIDbContext : DbContext
{
    public OrderAPIDbContext(DbContextOptions<OrderAPIDbContext> options) : base(options) { }

    public DbSet<Entities.Order> Orders => Set<Entities.Order>();
    public DbSet<Entities.OrderItem> OrderItems => Set<Entities.OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

}
