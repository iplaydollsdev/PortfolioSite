using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreLibrary.Models
{
   public class UserModel : IdentityUser
   {
      public string? Name { get; set; }
   }
}
