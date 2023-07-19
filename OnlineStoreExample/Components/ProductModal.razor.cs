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
    public partial class ProductModal
    {
        [Parameter]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }

        [Parameter]
        public CartModel? Cart { get; set; }

        [Parameter]
        public EventCallback OnChange { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }
        public bool IsInside { get; set; }

        private bool addButtonDisabled = false;
        private bool deleteButtonDisabled = false;
        protected override async Task OnInitializedAsync()
        {
            var product = await productData.GetProduct(Id);
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Image = product.Image;
        }

        private async Task NotifyStateChanged()
        {
            await OnChange.InvokeAsync();
        }

        private Task ModalClose()
        {
            return OnClose.InvokeAsync();
        }

        private Task ModalCloseOnOutsideClick()
        {
            if (IsInside == false)
            {
                return OnClose.InvokeAsync();
            }

            return (new EventCallback
            {
            }

            ).InvokeAsync();
        }

        private void MouseIn()
        {
            IsInside = true;
        }

        private void MouseOut()
        {
            IsInside = false;
        }

        public async Task AddToCart()
        {
            addButtonDisabled = true;
            var product = await productData.GetProduct(Id);
            if (Cart is not null)
            {
                Cart.Products.Add(product);
                await cartData.UpdateCartAsync(Cart);
                await NotifyStateChanged();
            }

            addButtonDisabled = false;
        }

        public async Task RemoveFromCart()
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