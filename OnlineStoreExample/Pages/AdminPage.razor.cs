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
using System.Xml.Serialization;
using Microsoft.AspNetCore.Identity;
using OfficeOpenXml;

namespace OnlineStoreExample.Pages
{
   public partial class AdminPage
   {
      bool isSending = false;
      string selectedCategory = string.Empty;
      private List<ProductModel> products = new();
      private List<ProductModel> filteredProducts = new();
      private List<OrderModel> orders = new();
      private List<OrderModel> filteredOrders = new();
      private List<CategoryModel> categories = new();
      private bool tabCatalog = true;
      private bool tabOrders = false;

      protected override async Task OnInitializedAsync()
      {
         products = await productData.GetAllProducts();
         categories = await categoryData.GetCategories();
         orders = await orderData.GetAllOrdersAsync();
         DropFilter();
         DropFilterOrders();
      }
      private void SwitchTabs(int tab)
      {
         switch (tab)
         {
            case 1:
               tabCatalog = true;
               tabOrders = false;
               break;
            case 2:
               tabCatalog = false;
               tabOrders = true; 
               break;
            default:
               break;
         }
      }

      #region FilterCatalog
      private string filterName = string.Empty;
      private string filterCategory = "Все";
      private uint filterIdMin;
      private uint filterIdMax;
      public uint FilterIdMin
      {
         get
         {
            return filterIdMin;
         }

         set
         {
            if (value < GetMinId())
            {
               filterIdMin = GetMinId();
            }
            else
            {
               if (value < filterIdMax)
               {
                  filterIdMin = value;
               }
               else
               {
                  filterIdMin = filterIdMax - 1;
               }
            }
         }
      }
      public uint FilterIdMax
      {
         get
         {
            return filterIdMax;
         }

         set
         {
            if (value > GetMaxId())
            {
               filterIdMax = GetMaxId();
            }
            else
            {
               if (value > filterIdMin)
               {
                  filterIdMax = value;
               }
               else
               {
                  filterIdMax = filterIdMin + 1;
               }
            }
         }
      }

      private uint filterPriceMin;
      private uint filterPriceMax;
      public uint FilterPriceMin
      {
         get
         {
            return filterPriceMin;
         }

         set
         {
            if (value < GetMinPrice())
            {
               filterPriceMin = GetMinPrice();
            }
            else
            {
               if (value < filterPriceMax)
               {
                  filterPriceMin = value;
               }
               else
               {
                  filterPriceMin = filterPriceMax - 10;
               }
            }
         }
      }
      public uint FilterPriceMax
      {
         get
         {
            return filterPriceMax;
         }

         set
         {
            if (value > GetMaxPrice())
            {
               filterPriceMax = GetMaxPrice();
            }
            else
            {
               if (value > filterPriceMin)
               {
                  filterPriceMax = value;
               }
               else
               {
                  filterPriceMax = filterPriceMin + 10;
               }
            }
         }
      }

      private void DropFilter()
      {
         filteredProducts = products;
         filterIdMin = GetMinId();
         filterIdMax = GetMaxId();
         filterPriceMin = GetMinPrice();
         filterPriceMax = GetMaxPrice();
         filterName = string.Empty;
         filterCategory = "Все";
      }

      private void Filter()
      {
         if (string.IsNullOrWhiteSpace(filterCategory) || filterCategory == "Все")
         {
            filteredProducts = products.Where(i => i.Id >= filterIdMin && i.Id <= filterIdMax).Where(p => p.Price >= filterPriceMin && p.Price <= filterPriceMax).Where(n => n.Name.Contains(filterName, StringComparison.InvariantCultureIgnoreCase)).ToList();
         }
         else
         {
            filteredProducts = products.Where(i => i.Id >= filterIdMin && i.Id <= filterIdMax).Where(c => c.Category == filterCategory).Where(p => p.Price >= filterPriceMin && p.Price <= filterPriceMax).Where(n => n.Name.Contains(filterName, StringComparison.InvariantCultureIgnoreCase)).ToList();
         }
      }
      private uint GetMaxId()
      {
         if (products.Count > 0)
         {
            return (uint)products.Max(p => p.Id);
         }
         else
         {
            return 0;
         }
      }
      private uint GetMinId()
      {
         if (products.Count > 0)
         {
            return (uint)products.Min(p => p.Id);
         }
         else
         {
            return 0;
         }
      }
      private uint GetMaxPrice()
      {
         if (products.Count > 0)
         {
            return (uint)products.Max(p => p.Price);
         }
         else
         {
            return 0;
         }
      }
      private uint GetMinPrice()
      {
         if (products.Count > 0)
         {
            return (uint)products.Min(p => p.Price);
         }
         else
         {
            return 0;
         }
      }

      #endregion

      #region FilterOrders
      private string filterOrderName = string.Empty;
      private string filterOrderStatus = "Все";
      private uint filterOrderIdMin;
      private uint filterOrderIdMax;
      public uint FilterOrderIdMin
      {
         get
         {
            return filterOrderIdMin;
         }

         set
         {
            if (value < GetMinId())
            {
               filterOrderIdMin = GetMinId();
            }
            else
            {
               if (value < filterOrderIdMax)
               {
                  filterOrderIdMin = value;
               }
               else
               {
                  filterOrderIdMin = filterOrderIdMax - 1;
               }
            }
         }
      }
      public uint FilterOrderIdMax
      {
         get
         {
            return filterOrderIdMax;
         }

         set
         {
            if (value > GetMaxId())
            {
               filterOrderIdMax = GetMaxId();
            }
            else
            {
               if (value > filterOrderIdMin)
               {
                  filterOrderIdMax = value;
               }
               else
               {
                  filterOrderIdMax = filterOrderIdMin + 1;
               }
            }
         }
      }

      private uint filterOrderPriceMin;
      private uint filterOrderPriceMax;
      public uint FilterOrderPriceMin
      {
         get
         {
            return filterOrderPriceMin;
         }

         set
         {
            if (value < GetMinPrice())
            {
               filterOrderPriceMin = GetMinPrice();
            }
            else
            {
               if (value < filterOrderPriceMax)
               {
                  filterOrderPriceMin = value;
               }
               else
               {
                  filterOrderPriceMin = filterOrderPriceMax - 10;
               }
            }
         }
      }
      public uint FilterOrderPriceMax
      {
         get
         {
            return filterOrderPriceMax;
         }

         set
         {
            if (value > GetMaxPrice())
            {
               filterOrderPriceMax = GetMaxPrice();
            }
            else
            {
               if (value > filterOrderPriceMin)
               {
                  filterOrderPriceMax = value;
               }
               else
               {
                  filterOrderPriceMax = filterOrderPriceMin + 10;
               }
            }
         }
      }

      private void DropFilterOrders()
      {
         filteredOrders = orders;
         filterOrderIdMin = GetMinOrderId();
         filterOrderIdMax = GetMaxOrderId();
         filterOrderPriceMin = GetMinOrderPrice();
         filterOrderPriceMax = GetMaxOrderPrice();
         filterOrderName = string.Empty;
         filterOrderStatus = "Все";
      }

      private void FilterOrders()
      {
         if (string.IsNullOrWhiteSpace(filterOrderStatus) || filterOrderStatus == "Все")
         {
            filteredOrders = orders.Where(i => i.Id >= filterOrderIdMin && i.Id <= filterOrderIdMax).Where(order => order.Products.Sum(product => product.Price) >= filterOrderPriceMin && order.Products.Sum(product => product.Price) <= filterOrderPriceMax).Where(n => n.UserEmail.Contains(filterOrderName, StringComparison.InvariantCultureIgnoreCase)).ToList();
         }
         else
         {
            filteredOrders = orders.Where(i => i.Id >= filterOrderIdMin && i.Id <= filterOrderIdMax).Where(c => c.OrderStatus == filterOrderStatus).Where(order => order.Products.Sum(product => product.Price) >= filterOrderPriceMin && order.Products.Sum(product => product.Price) <= filterOrderPriceMax).Where(n => n.UserEmail.Contains(filterOrderName, StringComparison.InvariantCultureIgnoreCase)).ToList();
         }
      }
      private uint GetMaxOrderId()
      {
         if (orders.Count > 0)
         {
            return (uint)orders.Max(p => p.Id);
         }
         else
         {
            return 0;
         }
      }
      private uint GetMinOrderId()
      {
         if (orders.Count > 0)
         {
            return (uint)orders.Min(p => p.Id);
         }
         else
         {
            return 0;
         }
      }
      private uint GetMaxOrderPrice()
      {
         if (orders.Count > 0)
         {
            return (uint)orders.Max(order => order.Products.Sum(product => product.Price));
         }
         else
         {
            return 0;
         }
      }
      private uint GetMinOrderPrice()
      {
         if (orders.Count > 0)
         {
            return (uint)orders.Min(order => order.Products.Sum(product => product.Price));
         }
         else
         {
            return 0;
         }
      }

      #endregion

      private async void DeleteProduct(ProductModel p)
      {
         await productData.DeleteProduct(p);
         products.Remove(p);
         DropFilter();
         StateHasChanged();
      }

      private async void ChangeOrderStatus(OrderModel order, string status)
      {
         order.OrderStatus = status;
         await orderData.UpdateOrderAsync(order);
      }

      #region Export
      private async Task ExportToXml()
      {
         isSending = true;
         if (string.IsNullOrWhiteSpace(selectedCategory) == false)
         {
            products = await productData.GetProductsByCategory(categories.Where(c => c.CategoryName == selectedCategory).FirstOrDefault() ?? new CategoryModel());
         }
         else
         {
            products = await productData.GetAllProducts();
         }

         var serializer = new XmlSerializer(typeof(List<ProductModel>));
         using (var fs = new FileStream("Products.xml", FileMode.Create))
         {
            serializer.Serialize(fs, products);
            await fs.DisposeAsync();
         }

         using (var attachmentFile = new System.Net.Mail.Attachment("Products.xml"))
         {
            var email = await GetUseremail();
            await emailSender.SendEmailWithFileAsync(email, subject: "XML", htmlMessage: "Çàïðàøèâàåìûé ôàéë", attachmentFile);
         }

         isSending = false;
      }
      public async Task ExportToExcel()
      {
         isSending = true;
         if (string.IsNullOrWhiteSpace(selectedCategory) == false)
         {
            products = await productData.GetProductsByCategory(categories.Where(c => c.CategoryName == selectedCategory).FirstOrDefault() ?? new CategoryModel());
         }
         else
         {
            products = await productData.GetAllProducts();
         }

         ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
         using (var package = new ExcelPackage())
         {
            var worksheet = package.Workbook.Worksheets.Add("Products");
            worksheet.Cells[1, 1].Value = "Id";
            worksheet.Cells[1, 2].Value = "Íàçâàíèå";
            worksheet.Cells[1, 3].Value = "Îïèñàíèå";
            worksheet.Cells[1, 4].Value = "Öåíà";
            worksheet.Cells[1, 5].Value = "Êàòåãîðèÿ";
            for (int i = 0; i < products.Count; i++)
            {
               worksheet.Cells[i + 2, 1].Value = products[i].Id;
               worksheet.Cells[i + 2, 2].Value = products[i].Name;
               worksheet.Cells[i + 2, 3].Value = products[i].Description;
               worksheet.Cells[i + 2, 4].Value = products[i].Price;
               worksheet.Cells[i + 2, 5].Value = products[i].Category;
            }

            using (var fs = new FileStream("Products.xlsx", FileMode.Create))
            {
               package.SaveAs(fs);
               await fs.DisposeAsync();
            }

            using (var attachmentFile = new System.Net.Mail.Attachment("Products.xlsx"))
            {
               var email = await GetUseremail();
               await emailSender.SendEmailWithFileAsync(email, subject: "XLSX", htmlMessage: "Çàïðàøèâàåìûé ôàéë", attachmentFile);
            }
         }

         isSending = false;
      }
      #endregion

      private async Task<string> GetUseremail()
      {
         var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
         var claimsPrincipal = authState.User as System.Security.Claims.ClaimsPrincipal;
         var email = claimsPrincipal?.Identity?.Name ?? "";
         return email;
      }

      private static string GetPercent(uint max, uint min, uint number, bool invert = false)
      {
         if (max != 0 && min != max)
         {
            decimal dec = ((decimal)number - min) / ((decimal)max - min);
            if (invert)
            {
               return (100 - (uint)(dec * 100)).ToString() + "%";
            }

            return ((uint)(dec * 100)).ToString() + "%";
         }

         return "0%";
      }
   }
}
