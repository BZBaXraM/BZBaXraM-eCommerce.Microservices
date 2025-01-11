using eCommerce.Products.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.DAL.Data;

public class ProductContext(DbContextOptions<ProductContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Category).IsRequired();
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.QuantityInStock).IsRequired();
        });
    }
}