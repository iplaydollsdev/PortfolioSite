using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreLibrary.Models
{
   [Serializable]
   public class ProductModel
   {
      public int Id { get; set; }
      [Required(ErrorMessage = "Поле 'Название' обязательно для заполнения.")]
      [StringLength(50, ErrorMessage = "Поле 'Название' должно содержать не более 50 символов.")]
      public string Name { get; set; } = null!;
      [Required(ErrorMessage = "Поле 'Описание' обязательно для заполнения.")]
      [StringLength(200, ErrorMessage = "Поле 'Описание' должно содержать не более 200 символов.")]
      public string Description { get; set; } = null!;
      [Required(ErrorMessage = "Изображение обязательно должно быть выбрано.")]
      public string Image { get; set; } = "/images/none-img.png";
      [Required(ErrorMessage = "Поле 'Цена' обязательно для заполнения.")]
      [Range(0.00, 100000.00,
        ErrorMessage = "Цена должна быть от {1} до {2}.")]
      [Column(TypeName = "decimal(18,2)")]
      public decimal Price { get; set; } = 0.00m;
      public string Category { get; set; } = "Десктопные приложения";
   }
}
