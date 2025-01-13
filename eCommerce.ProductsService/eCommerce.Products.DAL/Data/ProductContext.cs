using eCommerce.Products.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.DAL.Data;

public class ProductContext(DbContextOptions<ProductContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}