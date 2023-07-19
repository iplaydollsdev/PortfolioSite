using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreLibrary.Models
{
   [Serializable]
   public class ProductModel
   {
      public int Id { get; set; }
      public string Name { get; set; } = null!;
      public string Description { get; set; } = null!;
      public string Image { get; set; } = "/images/none-img.png";
      [Column(TypeName = "decimal(18,2)")]
      public decimal Price { get; set; }
      public string Category { get; set; } = null!;
   }
}
