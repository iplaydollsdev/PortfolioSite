namespace OnlineStoreLibrary.DataAccess
{
   public interface IOrder
   {
      Task CreateOrderAsync(OrderModel order);
      Task<List<OrderProductModel>> GetAllProductsInOrder(int id);
      Task<OrderModel> GetOrderAsync(int id);
      Task<List<OrderModel>> GetOrdersByIdAsync(string id);
      Task<List<OrderModel>> GetOrdersByEmailAsync(string email);
      Task<List<OrderModel>> GetAllOrdersAsync();
      Task DeleteOrder(OrderModel order);
      Task UpdateOrderAsync(OrderModel order);
   }
}