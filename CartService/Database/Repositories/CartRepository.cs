using CartService.Database.DbContext;
using CartService.Database.Entities;
using CartService.Database.Repositories.Interfaces;
using CartService.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CartService.Database.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CartDbContext _dbContext;
        public CartRepository(CartDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task InsertCartAsync(CartEntity cartEntity)
        {
            var result = await _dbContext.Carts.AnyAsync(x => x.UserId == cartEntity.UserId);
            if (result)
            {
                throw new CartAlreadyExist();
            }
            await _dbContext.Carts.AddAsync(cartEntity);
        }

        public async Task<CartEntity> GetCartByUserId(Guid userId)
        {
            var cart = await _dbContext.Carts.FirstOrDefaultAsync(x => x.UserId == userId);
            if (cart == null)
            {
                throw new CartNotFound();
            }

            return cart;
        }
        
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

