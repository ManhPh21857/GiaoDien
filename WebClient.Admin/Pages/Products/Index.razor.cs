using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Products;
using WebClient.Admin.Shared.Alert;
#pragma warning disable CS8618

namespace WebClient.Admin.Pages.Products
{
    public partial class Index : ComponentBase
    {
        [Inject] private IProductService ProductService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ILocalStorageService LocalStorageService { get; set; }

        private PopUp PopUp { get; set; }

        private List<ProductViewModel> products = new();

        protected async override Task<Task> OnInitializedAsync()
        {
            var token = await this.LocalStorageService.GetItemAsStringAsync("token");

            if (string.IsNullOrEmpty(token))
            {
                this.NavigationManager.NavigateTo("login");
                return base.OnInitializedAsync();
            }

            await this.GetProducts(1);

            return base.OnInitializedAsync();
        }

        private async Task GetProducts(int pageNo)
        {
            var result = await this.ProductService.GetProducts(pageNo);

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<GetProductViewResponseModel>().Data;

                if (response != null)
                {
                    products = response.Products;
                }
                else
                {
                    await this.Error("", "");
                }
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await this.Error(error?.ErrorCode, error?.ErrorMessage);
            }
        }

        private async Task DeleteProduct(int id)
        {
            var result = await this.ProductService.DeleteProduct(id);

            if (result.IsSuccessStatusCode)
            {
                products.RemoveAll(x => x.Id == id);
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await this.PopUp.Error(error?.ErrorCode, error?.ErrorMessage);
            }
        }

        private async Task Error(string? title, string? text)
        {
            await this.PopUp.Error(title, text);

        }
    }
}