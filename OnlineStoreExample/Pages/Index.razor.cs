using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Completion;
using System.Timers;
using System.Threading;
using System.Text;
using System.Reflection;

namespace OnlineStoreExample.Pages
{
    public partial class Index
    {

      #region Initializing

      [CascadingParameter]
      private Task<AuthenticationState>? authenticationStateTask { get; set; }
      List<ProductModel> products = new();
      private List<CategoryModel> categories = new();
      public CartModel cart = new();
      List<ProductModel> productsToShow = new();

      protected override async Task OnInitializedAsync()
      {
         products = await productData.GetAllProducts();
         categories = await categoryData.GetCategories();

         productsToShow = products;

         var user = (await authenticationStateTask!).User;

         if (user.Identity!.IsAuthenticated)
         {
            var _user = await userManager.GetUserAsync(user);
            if (_user is not null)
            {
               var _userId = await userManager.GetUserIdAsync(_user);
               cart = await cartData.GetCartAsync(_userId!);
               if (cart == null)
               {
                  var newCart = new CartModel() { UserId = _userId };
                  await cartData.CreateCartAsync(newCart);
                  cart = await cartData.GetCartAsync(_userId!);
               }
            }
         }
         else
         {
            // User is not logged in
         }
      }
      #endregion

      #region BGSlider
      private int sliderIndex = 0;
      private System.Timers.Timer? timerForSlider;

      protected override void OnInitialized()
      {
         timerForSlider = new System.Timers.Timer(10000);
         timerForSlider.Elapsed += TimerElapsed!;
         timerForSlider.Start();
      }
      private void TimerElapsed(object sender, ElapsedEventArgs e)
      {
         sliderIndex++;
         if (sliderIndex >= 3)
         {
            sliderIndex = 0;
         }

         InvokeAsync(StateHasChanged);
      }

      public void Dispose()
      {
         timerForSlider?.Dispose();
      }

      List<string> sliderOptions = new()
      {
         "Окунись в мир игры с головой",
         "Начни Играть со вкусом",
         "Преврати Идеи в реальность"
      };
      List<string> slideBGOptions = new()
      {
         "url(../images/pc.jpg);",
         "url(../images/mobile.jpg);",
         "url(../images/web.jpg);"
      };

      private void SlideNext()
      {
         if (sliderIndex < 2)
         {
            sliderIndex++;
         }
         else
         {
            sliderIndex = 0;
         }
         if (timerForSlider is not null) 
         {
            timerForSlider.Stop();
            timerForSlider.Start();
         } 
         StateHasChanged();
      }

      private void SlidePrevious()
      {
         if (sliderIndex != 0)
         {
            sliderIndex--;
         }
         else
         {
            sliderIndex = 2;
         }
         if (timerForSlider is not null)
         {
            timerForSlider.Stop();
            timerForSlider.Start();
         }
         StateHasChanged();
      }

      #endregion

      #region ModalWindow

      public bool ModalWindowOpen { get; set; }
      public int ModalId { get; set; }

      private void OnModalClose()
      {
         ModalWindowOpen = false;
         StateHasChanged();
      }
      private void OpenModal(int id)
      {
         ModalId = id;
         ModalWindowOpen = true;
         StateHasChanged();
      }
      #endregion

      #region ModalPictureWindow
      public bool ModalPictureWindowOpen { get; set; }

      private void OnModalPictureClose()
      {
         ModalPictureWindowOpen = false;
         StateHasChanged();
      }
      private void OpenPictureModal(int id)
      {
         ModalId = id;
         ModalPictureWindowOpen = true;
         StateHasChanged();
      }
      #endregion

      #region Search

      string selectedCategory = string.Empty;
      string selectedCategoryInSearch = string.Empty;
      string searchInput = string.Empty;
      bool isSearching = false;
      List<ProductModel> productsInSearch = new();

      private void OnSearch(KeyboardEventArgs e)
      {
         if (e.Key == "Enter")
         {
            var newSearchResult = new List<ProductModel>();

            selectedCategory = selectedCategoryInSearch;

            if (String.IsNullOrWhiteSpace(searchInput) == false)
            {
               newSearchResult = products.Where(p => p.Name.Contains(searchInput, StringComparison.InvariantCultureIgnoreCase) 
                                                  || p.Description.Contains(searchInput, StringComparison.InvariantCultureIgnoreCase)).ToList();
               productsInSearch = newSearchResult;
               productsToShow = productsInSearch;
            }
            else
            {
               productsToShow = products;
            }
            isSearching = true;
            StateHasChanged();
         }
      }

      private void OnSearchButton()
      {
         OnSearch(new KeyboardEventArgs { Key = "Enter"});
      }

      private void OnSearchClosed()
      {
         searchInput = string.Empty;
         selectedCategory = string.Empty;
         productsToShow = products;
         isSearching = false;
         StateHasChanged();
      }

      private void SelectCategory(string category)
      {
         if (category == selectedCategory)
         {
            selectedCategory = string.Empty;
         }
         else
         {
            selectedCategory = category;
         }
      }
      #endregion

      #region Footer
      private string email = "iplaydollsdev@gmail.com";

      private bool isCopying = false;

      private async Task CopyToClipboard()
      {
         await JSRuntime.InvokeVoidAsync("copyTextToClipboard", email);

         //await JSRuntime.InvokeVoidAsync("navigator.clipboard.writeText", email);
         isCopying = true;
         StateHasChanged();
         await Task.Delay(1000);
         isCopying = false;
         StateHasChanged();
      }
      #endregion
   }
}