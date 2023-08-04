namespace OnlineStoreLibrary.Models
{
   public class OrderModel
   {
      public int Id { get; set; }
      public string UserId { get; set; } = string.Empty;
      public string UserEmail { get; set; } = string.Empty;
      public string OrderStatus { get; set; } = "На проверке";
      public List<ProductModel> Products { get; set; } = new();
   }
}
