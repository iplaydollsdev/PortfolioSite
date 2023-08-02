using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using OnlineStoreExample;
using OnlineStoreExample.Shared;
using OnlineStoreExample.Components;
using Microsoft.AspNetCore.Components;

namespace OnlineStoreExample.Components
{
   public partial class ProductForCart
   {
      [Parameter]
      public int Id { get; set; }

      [Parameter]
      public string? Name { get; set; }
      [Parameter]
      public string? Desc { get; set; }

      [Parameter]
      public decimal? Price { get; set; }

      [Parameter]
      public string? Image { get; set; }

      [Parameter]
      public CartModel? Cart { get; set; }

      [Parameter]
      public EventCallback OnChange { get; set; }

      [Parameter]
      public EventCallback OnModalOpen { get; set; }

      [Parameter]
      public EventCallback OnModalPictureOpen { get; set; }

      private bool deleteButtonDisabled = false;
      private async Task NotifyStateChanged()
      {
         await OnChange.InvokeAsync();
      }

      public async void RemoveFromCart()
      {
         deleteButtonDisabled = true;
         var product = await productData.GetProduct(Id);
         if (Cart is not null)
         {
            Cart.Products.Remove(product);
            await cartData.UpdateCartAsync(Cart);
            await NotifyStateChanged();
         }

         deleteButtonDisabled = false;
      }
   }
}