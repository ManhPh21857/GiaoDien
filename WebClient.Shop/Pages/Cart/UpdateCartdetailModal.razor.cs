using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Colors;
using Presentation.Product.Domain.Materials;
using Presentation.Product.Domain.Products;
using Presentation.Product.Domain.Sizes;
using Presentation.Sales.Domain.Cart;
using WebClient.Shop.Shared.Alert;

namespace WebClient.Shop.Pages.Cart
{
    public partial class UpdateCartdetailModal : ComponentBase
    {
        #region Inject

        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IProductService ProductService { get; set; }
        [Inject] public ICartDetailService CartDetailService { get; set; }
        [Inject] public IColorService ColorService { get; set; }
        [Inject] public ISizeService SizeService { get; set; }

        public PopUp PopUp { get; set; }

        #endregion

        #region Variable
        public ProductDetailIdModel Productdetails;
        private ProductItem product = new();
        private IEnumerable<ProductColor> productColors = new List<ProductColor>();

        private IEnumerable<ProductSize> productSizes = new List<ProductSize>();

        private IEnumerable<ProductDetailView> productDetails = new List<ProductDetailView>();

        private ProductPreview productPreview = new();
        private CartDetailModel cartDetailModels = new();
        public List<CartDetailModel> Cartdetails = new();
        private int? colorId;
        private readonly string modalId = "updateCartdetailModal";
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

        protected async Task CustomOnInitializedAsync()
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

            this.StateHasChanged();
        }

        public CartDetailModel cartDetailModelInput { get; set; }

        public async void Load(CartDetailModel cartDetail)
        {
            cartDetailModelInput = cartDetail;
            await this.CustomOnInitializedAsync();
        }
      
        private async Task GetProductItem()
        {
            var result = await this.ProductService.GetProductItem(cartDetailModelInput.ProductId.Value);

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


        private async Task UpdateCart()
        {

         
            cartDetailModels.ProductDetailId = Convert.ToInt32(cartDetailModelInput.Id);
            cartDetailModels.Quantity = productPreview.Quantity;
            cartDetailModels.DataVersion = "AAAAAAAAhYU=";
            cartDetailModels.ProductId =cartDetailModelInput.ProductId;
            cartDetailModels.ColorId = ColorId;
            cartDetailModels.SizeId = SizeId;
            var result = await CartDetailService.UpdateCartdetail(cartDetailModels);
            if (result.IsSuccessStatusCode)
            {
                await PopUp.Success();

            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }
        }
    }
}
