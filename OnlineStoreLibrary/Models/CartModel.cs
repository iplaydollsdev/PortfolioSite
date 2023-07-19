namespace OnlineStoreLibrary.Models
{
   public class CartModel
   {
      public int Id { get; set; }
      public string UserId { get; set; } = string.Empty;
      public List<ProductModel> Products { get; set; } = new();
   }
}
