using Microsoft.AspNetCore.Components;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Colors;
using Presentation.Product.Domain.Products;
using Presentation.Product.Domain.Sizes;
using WebClient.Shop.Shared.Alert;

namespace WebClient.Shop.Pages.Product
{
    public partial class Item : ComponentBase
    {
        #region Paramater

        [Parameter] public string Id { get; set; }

        #endregion

        #region Inject

        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IProductService ProductService { get; set; }
        [Inject] public IColorService ColorService { get; set; }
        [Inject] public ISizeService SizeService { get; set; }

        public PopUp PopUp { get; set; }

        #endregion

        #region Variable

        private ProductItem product = new();

        private IEnumerable<ProductColor> productColors = new List<ProductColor>();

        private IEnumerable<ProductSize> productSizes = new List<ProductSize>();

        private IEnumerable<ProductDetailView> productDetails = new List<ProductDetailView>();

        private ProductPreview productPreview = new();

        private int? colorId;

        public int? ColorId
        {
            get => colorId;
            set
            {
                colorId = value;
                if (value.HasValue)
                {
                    this.ChangeColor(value.Value);
                }
            }
        }

        private int? sizeId;

        public int? SizeId
        {
            get => sizeId;
            set
            {
                sizeId = value;
                if (value.HasValue)
                {
                    this.ChangeSize(value.Value);

                }
            }
        }

        private int? quantity;

        #endregion

        protected async override Task<Task> OnInitializedAsync()
        {
            await this.GetProductItem();

            this.productPreview = new()
            {
                ProductId = product.Id,
                Price =
                    string.Join(" ~ ", new List<string> { $"{product.MinPrice}", $"{product.MaxPrice}" }.Distinct()),
                Quantity = 1,
                Image = product.Image
            };

            return base.OnInitializedAsync();
        }

        private async Task GetProductItem()
        {
            if (!int.TryParse(Id, out int id))
            {
                this.NavigationManager.NavigateTo("product");
            }

            var result = await this.ProductService.GetProductItem(id);

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<ProductItemResponseModel>().Data;

                this.product = response.Product;

                this.productDetails = response.ProductDetails;

                this.productColors = response.ProductColors;

                this.productSizes = response.ProductSizes;
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await this.PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }
        }

        
        private void Plus()
        {
            if (this.productPreview.Quantity < this.quantity)
            {
                this.productPreview.Quantity++;
            }
        }

        private void Minus()
        {
            if (this.productPreview.Quantity > 1)
            {
                this.productPreview.Quantity--;
            }
        }

        private void ChangeImage(int? id)
        {
            this.productPreview.Image = this.productColors.FirstOrDefault(x => x.ColorId == id)?.Image;
            if (this.productPreview is { Image: null })
            {
                this.productPreview.Image = product.Image;
            }
        }

        private void ChangeColor(int id)
        {
            this.productPreview.ColorId = id;
            this.ChangeImage(id);

            this.ChangeProduct();
        }

        private void ChangeSize(int id)
        {
            this.productPreview.SizeId = id;

            this.ChangeProduct();
        }

        private void ChangeProduct()
        {
            var temp = this.productDetails.SingleOrDefault(x =>
                x.ColorId == this.productPreview.ColorId && x.SizeId == this.productPreview.SizeId);

            if (temp is not null)
            {
                this.quantity = temp.Quantity;
                this.productPreview.Price = $"{temp.Price}";
                if (this.productPreview.Quantity >= quantity)
                {
                    this.productPreview.Quantity = quantity;
                }

                this.StateHasChanged();
            }
        }

        private void AddToCard()
        {
            Console.WriteLine(productPreview.ColorId);
            Console.WriteLine(productPreview.SizeId);
            Console.WriteLine(productPreview.Quantity);
        }
    }
}