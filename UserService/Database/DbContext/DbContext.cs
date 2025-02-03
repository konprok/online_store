using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Database.Configuration;
using UserService.Database.Entities;
using UserService.Services.Interfaces;

namespace UserService.Database.DbContext;

public class UserDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly IPasswordHasher _passwordHasher;

    public UserDbContext(DbContextOptions<UserDbContext> options, IPasswordHasher passwordHasher) : base(options)
    {
        _passwordHasher = passwordHasher;
    }
    
    public DbSet<UserEntity> Users { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
    }
    public void Seed()
    {
        if (!Users.Any(u => u.Email == "admin@example.com"))
        {
            Users.Add(new UserEntity
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                PasswordHash = _passwordHasher.Hash("admin"), 
                Email = "admin@example.com",
                IsAdmin = true
            });

            SaveChanges();
        }
    }
}