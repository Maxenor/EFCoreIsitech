using EFCoreIsitech.Data.Configurations;
using EFCoreIsitech.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreIsitech.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Define your DbSets (tables) here
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    
    // New DbSets for the enhanced model
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<ProductInventory> ProductInventories { get; set; } = null!;
    public DbSet<ProductRating> ProductRatings { get; set; } = null!;
    public DbSet<ProductPriceHistory> ProductPriceHistories { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new ProductInventoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductRatingConfiguration());
        modelBuilder.ApplyConfiguration(new ProductPriceHistoryConfiguration());
    }
}