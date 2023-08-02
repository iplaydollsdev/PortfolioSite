using Microsoft.EntityFrameworkCore;

namespace OnlineStoreLibrary.DataAccess
{
   public class CartData : ICart
   {
      readonly AppDbContext _dbContext = new();
      public CartData(AppDbContext dbContext)
      {
         _dbContext = dbContext;
      }

      public async Task<CartModel> GetCartAsync(string id)
      {
         try
         {
            var cart = new CartModel();
            cart = await _dbContext.Cart.FirstOrDefaultAsync(c => c.UserId == id.ToString());
            return cart!;
         }
         catch
         {
            throw;
         }
      }

      public async Task CreateCartAsync(CartModel cart)
      {
         try
         {
            await _dbContext.Cart.AddAsync(cart);
            await _dbContext.SaveChangesAsync();
         }
         catch
         {
            throw;
         }
      }

      public async Task UpdateCartAsync(CartModel cart)
      {
         try
         {
            _dbContext.Entry(cart).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
         }
         catch
         {
            throw;
         }
      }

      public async Task<List<ProductModel>> GetAllProductsInCart(string id)
      {
         try
         {
            var cart = new CartModel();
            cart = await _dbContext.Cart.FirstOrDefaultAsync(c => c.UserId == id.ToString());
            return cart!.Products;
         }
         catch
         {
            throw;
         }
      }
   }
}
