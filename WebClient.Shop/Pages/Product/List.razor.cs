using Microsoft.AspNetCore.Components;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Products;
using WebClient.Shop.Shared.Alert;

namespace WebClient.Shop.Pages.Product
{
    public partial class List : ComponentBase
    {
        #region Inject

        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IProductService ProductService { get; set; }

        public PopUp PopUp { get; set; }

        #endregion

        #region MyRegion

        private int page;
        private int totalPage;

        private IEnumerable<ProductView> products = new List<ProductView>();

        #endregion

        #region Init

        protected async override Task<Task> OnInitializedAsync()
        {
            this.page = 1;
            await this.GetProduct(1);

            return base.OnInitializedAsync();
        }

        #endregion

        #region Navigation

        private void GotoProductDetail(int id)
        {
            this.NavigationManager.NavigateTo($"product/{id}");
        }

        #endregion

        #region Paging

        private async Task GetProduct(int pageNo)
        {
            var filter = new ProductFilter();

            var result = await this.ProductService.GetProductView(pageNo, filter);

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<ProductViewResponseModel>().Data;

                this.products = response.Products;
                this.totalPage = response.TotalProduct / 12;
                if (response.TotalProduct % 12 > 0)
                {
                    this.totalPage += 1;
                }
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await this.PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }

            Console.WriteLine(pageNo);
        }

        private async Task GotoPage(int pageNo)
        {
            await this.GetProduct(pageNo);
            this.page = pageNo;

            Console.WriteLine(pageNo);

            this.StateHasChanged();
        }

        #endregion
    }
}