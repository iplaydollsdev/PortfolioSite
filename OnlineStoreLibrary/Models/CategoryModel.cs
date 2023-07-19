using System.ComponentModel.DataAnnotations;

namespace OnlineStoreLibrary.Models
{
   public class CategoryModel
   {
      public int Id { get; set; }
      public string CategoryName { get; set; } = null!;
      public string CategoryDescription { get; set; } = null!;
   }
}
