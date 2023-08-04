namespace OnlineStoreLibrary.DataAccess
{
   public interface IOrder
   {
      Task CreateOrderAsync(OrderModel order);
      Task<List<ProductModel>> GetAllProductsInOrder(int id);
      Task<OrderModel> GetOrderAsync(int id);
      Task<List<OrderModel>> GetOrdersByIdAsync(string id);
      Task<List<OrderModel>> GetOrdersByEmailAsync(string email);
      Task<List<OrderModel>> GetAllOrdersAsync();

      Task UpdateOrderAsync(OrderModel order);
   }
}