#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineStoreExample.Areas.Identity.Pages.Account.Manage
{
   public class CartModel : PageModel
   {
      private readonly UserManager<UserModel> _userManager;
      private readonly SignInManager<UserModel> _signInManager;
      private readonly IEmailSender _emailSender;

      public CartModel(
          UserManager<UserModel> userManager,
          SignInManager<UserModel> signInManager,
          IEmailSender emailSender)
      {
         _userManager = userManager;
         _signInManager = signInManager;
         _emailSender = emailSender;
      }

      public List<ProductModel> Products { get; set; }
      public decimal Subtotal { get; set; }
      public decimal Shipping { get; set; }
      public decimal Tax { get; set; }
      public decimal Total { get; set; }

      public CartModel()
      {
         Products = new List<ProductModel>();
      }

      public void AddProduct(ProductModel product)
      {
         Products.Add(product);
         CalculateTotal();
      }

      public void RemoveProduct(int productId)
      {
         var product = Products.SingleOrDefault(p => p.Id == productId);
         if (product != null)
         {
            Products.Remove(product);
            CalculateTotal();
         }
      }

      private void CalculateTotal()
      {
         Subtotal = 0;
         Shipping = 0;
         Tax = 0;

         foreach (var product in Products)
         {
            Subtotal += product.Price/* * product.Quantity */;
         }

         Shipping = 5;
         Tax = Subtotal * (decimal)0.125;

         Total = Subtotal + Shipping + Tax;
      }
   }
}
