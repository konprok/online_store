using Microsoft.EntityFrameworkCore;
using OfferService.Database.Configuration;
using OfferService.Database.Entities;

namespace OfferService.Database.DbContext;
public sealed class OfferDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public OfferDbContext(DbContextOptions<OfferDbContext> options) : base(options) { }
    
    public DbSet<OfferEntity> Offers { get; set; } = null!;
    public DbSet<CategoryEntity> Category { get; set; } = null!;
    public DbSet<RatingEntity> Rating { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new OfferEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RatingEntityConfiguration());
    }
}