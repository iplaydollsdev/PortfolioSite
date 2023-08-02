using Microsoft.EntityFrameworkCore;

namespace OnlineStoreLibrary.DataAccess
{
   public class OrderData : IOrder
   {
      readonly AppDbContext _dbContext = new();
      public OrderData(AppDbContext dbContext)
      {
         _dbContext = dbContext;
      }
      public async Task<List<OrderModel>> GetOrdersAsync(string id)
      {
         try
         {
            return await _dbContext.Order.Where(c => c.UserId == id.ToString()).ToListAsync();
         }
         catch
         {
            throw;
         }
      }
      public async Task<OrderModel> GetOrderAsync(int id)
      {
         try
         {
            var order = new OrderModel();
            order = await _dbContext.Order.FirstOrDefaultAsync(c => c.Id == id);
            return order!;

         }
         catch
         {
            throw;
         }
      }
      public async Task CreateOrderAsync(OrderModel order)
      {
         try
         {
            await _dbContext.Order.AddAsync(order);
            await _dbContext.SaveChangesAsync();
         }
         catch
         {
            throw;
         }
      }
      public async Task UpdateOrderAsync(OrderModel order)
      {
         try
         {
            _dbContext.Entry(order).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
         }
         catch
         {
            throw;
         }
      }
      public async Task<List<ProductModel>> GetAllProductsInOrder(int id)
      {
         try
         {
            var order = new OrderModel();
            order = await _dbContext.Order.FirstOrDefaultAsync(o => o.Id == id);
            return order!.Products;
         }
         catch
         {
            throw;
         }
      }
   }
}
