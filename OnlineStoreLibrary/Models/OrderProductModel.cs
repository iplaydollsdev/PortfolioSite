using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreLibrary.Models
{
   public class OrderProductModel
   {
      public int Id { get; set; }
      public string Name { get; set; } = string.Empty;
      [Column(TypeName = "decimal(18,2)")]
      public decimal Price { get; set; }
   }
}
