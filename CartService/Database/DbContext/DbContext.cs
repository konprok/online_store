using CartService.Database.Entities;
using Microsoft.EntityFrameworkCore;
using CartService.Database.Configuration;

namespace CartService.Database.DbContext;

public class CartDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public CartDbContext(DbContextOptions<CartDbContext> options) : base(options) { }

    public DbSet<CartEntity> Carts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CartEntityConfiguration());
    }
}