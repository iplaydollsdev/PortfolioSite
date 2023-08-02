namespace OnlineStoreLibrary.DataAccess
{
   public interface IOrder
   {
      Task CreateOrderAsync(OrderModel order);
      Task<List<ProductModel>> GetAllProductsInOrder(int id);
      Task<OrderModel> GetOrderAsync(int id);
      Task<List<OrderModel>> GetOrdersAsync(string id);
      Task UpdateOrderAsync(OrderModel order);
   }
}