using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreLibrary.Models
{
   public class OrderModel
   {
      public int Id { get; set; }
      public string UserId { get; set; } = string.Empty;
      public string UserEmail { get; set; } = string.Empty;
      public string OrderStatus { get; set; } = "На проверке";
      public List<OrderProductModel> Products { get; set; } = new();
   }
}
