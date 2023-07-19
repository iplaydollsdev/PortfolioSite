namespace OnlineStoreLibrary.DataAccess
{
   public interface ICart
   {
      Task CreateCartAsync(CartModel cart);
      Task<CartModel> GetCartAsync(string id);
      Task UpdateCartAsync(CartModel cart);
   }
}