using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Classifications;
using Presentation.Product.Domain.Colors;
using Presentation.Product.Domain.Materials;
using Presentation.Product.Domain.Origins;
using Presentation.Product.Domain.Products;
using Presentation.Product.Domain.Sizes;
using Presentation.Product.Domain.Suppliers;
using Presentation.Product.Domain.Trademarks;
using WebClient.Admin.Shared.Alert;
#pragma warning disable CS8618

namespace WebClient.Admin.Pages.Products
{
    public class CreateTitle
    {
        public string Title { get; set; }
        public string Button { get; set; }
    }

    public partial class Create : ComponentBase
    {
        [Parameter] public string Mode { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ILocalStorageService LocalStorageService { get; set; }
        [Inject] private IClassificationService ClassificationService { get; set; }
        [Inject] private IColorService ColorService { get; set; }
        [Inject] private IMaterialService MaterialService { get; set; }
        [Inject] private IOriginService OriginService { get; set; }
        [Inject] private IProductService ProductService { get; set; }
        [Inject] private ISizeService SizeService { get; set; }
        [Inject] private ISupplierService SupplierService { get; set; }
        [Inject] private ITrademarkService TrademarkService { get; set; }

        private PopUp PopUp { get; set; }

        private readonly CreateTitle pageModel = new();

        private List<ColorModel> colors = new();
        private List<SizeModel> sizes = new();
        private List<ClassificationModel> classifications = new();
        private List<MaterialModel> materials = new();
        private List<SupplierModel> suppliers = new();
        private List<TrademarkModel> trademarks = new();
        private List<OriginModel> origins = new();

        private ProductModel productModel = new();

        #region InitData

        protected async override Task<Task> OnInitializedAsync()
        {
            var token = await this.LocalStorageService.GetItemAsStringAsync("token");

            if (string.IsNullOrEmpty(token))
            {
                this.NavigationManager.NavigateTo("login");
                return base.OnInitializedAsync();
            }

            await GetColors();
            await GetSizes();
            await GetClassifications();
            await GetMaterials();
            await GetSuppliers();
            await GetTrademarks();
            await GetOrigins();

            if (Mode == "create")
            {
                pageModel.Title = "Thêm sản phẩm";
                pageModel.Button = "Thêm mới";
            }
            else
            {
                if (!int.TryParse(Mode, out int id))
                {
                    this.NavigationManager.NavigateTo("product");
                    return base.OnInitializedAsync();
                }

                pageModel.Title = "Chi tiết sản phẩm";
                pageModel.Button = "Cập nhật";
                await GetProduct(id);
            }

            productModel.ProductColors = colors.GroupJoin(
                productModel.ProductColors,
                c => c.Id,
                pc => pc.ColorId,
                (c, pc) => new { c, pc }
            ).Select(
                s => new ProductColor
                {
                    ColorId = s.c.Id,
                    Image = s.pc.SingleOrDefault()?.Image ?? "",
                    Adoption = s.pc.Any()
                }
            ).ToList();

            productModel.ProductSizes = sizes.GroupJoin(
                productModel.ProductSizes,
                s => s.Id,
                ps => ps.SizeId,
                (s, ps) => new { s, ps }
            ).Select(
                s => new ProductSize
                {
                    SizeId = s.s.Id,
                    Adoption = s.ps.Any()
                }
            ).ToList();

            return base.OnInitializedAsync();
        }

        private async Task GetProduct(int id)
        {
            var result = await this.ProductService.GetProduct(id);

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<ProductModel>().Data;

                if (response != null)
                {
                    productModel = response;
                }
                else
                {
                    this.Error("", "");
                }
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                this.Error(error?.ErrorCode, error?.ErrorMessage);
            }
        }

        private async Task GetColors()
        {
            var result = await this.ColorService.GetColors();

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<ColorResponseModel>().Data;

                if (response != null)
                {
                    colors = response.Colors;
                }
                else
                {
                    this.Error("", "");
                }
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                this.Error(error?.ErrorCode, error?.ErrorMessage);
            }
        }

        private async Task GetSizes()
        {
            var result = await this.SizeService.GetSize();

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<SizeResponseModel>().Data;

                if (response != null)
                {
                    sizes = response.Sizes;
                }
                else
                {
                    this.Error("", "");
                }
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                this.Error(error?.ErrorCode, error?.ErrorMessage);
            }
        }

        private async Task GetClassifications()
        {
            var result = await this.ClassificationService.GetClassfication();

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<ClassficationResponseModel>().Data;

                if (response != null)
                {
                    classifications = response.Classifications;
                }
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await PopUp.Error(error?.ErrorCode, error?.ErrorMessage);
            }
        }

        private async Task GetMaterials()
        {
            var result = await this.MaterialService.GetMaterial();

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<MaterialsResponseModel>().Data;

                if (response != null)
                {
                    materials = response.Materials;
                }
                else
                {
                    this.Error("", "");
                }
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                this.Error(error?.ErrorCode, error?.ErrorMessage);
            }
        }

        private async Task GetSuppliers()
        {
            var result = await this.SupplierService.GetSupplier();

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<SupplierResponseModel>().Data;

                if (response != null)
                {
                    suppliers = response.Suppliers;
                }
                else
                {
                    this.Error("", "");
                }
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                this.Error(error?.ErrorCode, error?.ErrorMessage);
            }
        }

        private async Task GetTrademarks()
        {
            var result = await this.TrademarkService.GetTrademark();

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<TrademarkResponseModel>().Data;

                if (response != null)
                {
                    trademarks = response.Trademarks;
                }
                else
                {
                    this.Error("", "");
                }
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                this.Error(error?.ErrorCode, error?.ErrorMessage);
            }
        }

        private async Task GetOrigins()
        {
            var result = await this.OriginService.GetOrigin();

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<OriginResponseModel>().Data;

                if (response != null)
                {
                    origins = response.Origins;
                }
                else
                {
                    this.Error("", "");
                }
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                this.Error(error?.ErrorCode, error?.ErrorMessage);
            }
        }

        #endregion

        private void ChangeColorCheckBox(int? colorId)
        {
            var productColor = productModel.ProductColors.Find(x => x.ColorId == colorId);
            if (productColor is not null)
            {
                productColor.Adoption = !productColor.Adoption;
            }

            productModel.ProductDetails = this.ChangeProductItem().ToList();
        }

        private void ChangeSizeCheckBox(int? sizeId)
        {
            var productSize = productModel.ProductSizes.Find(x => x.SizeId == sizeId);
            if (productSize is not null)
            {
                productSize.Adoption = !productSize.Adoption;
            }

            productModel.ProductDetails = this.ChangeProductItem().ToList();
        }

        private IEnumerable<ProductDetail> ChangeProductItem()
        {
            foreach (var s in productModel.ProductColors.Where(x => x.Adoption))
            {
                foreach (var i in productModel.ProductSizes.Where(x => x.Adoption))
                {
                    var pro = productModel.ProductDetails.Find(x => x.ColorId == s.ColorId && x.SizeId == i.SizeId);
                    if (pro is not null)
                    {
                        yield return pro;
                    }
                    else
                    {
                        yield return new ProductDetail
                        {
                            ColorId = s.ColorId,
                            SizeId = i.SizeId
                        };
                    }
                }
            }
        }

        #region ProductModel

        private async Task Update()
        {
            var result = await this.ProductService.CreateProduct(productModel);

            if (result.IsSuccessStatusCode)
            {
                if (Mode == "create")
                {
                    NavigationManager.NavigateTo("product");
                }
                else
                {
                    await this.GetProduct(productModel.Product.Id);
                }
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                this.Error(error?.ErrorCode, error?.ErrorMessage);
            }
        }

        private async void Cancel()
        {
            if (this.CheckDataInScreen())
            {
                await PopUp.Confirm("Are you sure?", "your change will be lost",
                    () => this.NavigationManager.NavigateTo("product"));
            }
            else
            {
                this.NavigationManager.NavigateTo("product");
            }
        }

        private bool CheckDataInScreen()
        {
            if (!string.IsNullOrEmpty(productModel.Product.Code)) return true;
            if (!string.IsNullOrEmpty(productModel.Product.Name)) return true;
            if (productModel.Product.ClassificationId.HasValue) return true;
            if (productModel.Product.MaterialId.HasValue) return true;
            if (productModel.Product.SupplierId.HasValue) return true;
            if (productModel.Product.TrademarkId.HasValue) return true;
            if (productModel.Product.OriginId.HasValue) return true;
            //if (!string.IsNullOrEmpty(ProductModel.Product.Image)) return true;
            if (!string.IsNullOrEmpty(productModel.Product.Description)) return true;
            if (productModel.ProductColors.Any(x=>x.Adoption)) return true;
            if (productModel.ProductSizes.Any(x=>x.Adoption)) return true;
            if (productModel.ProductDetails.Any()) return true;

            return false;
        }

        #endregion

        #region MessageBox

        private async void Error(string? title, string? text)
        {
            await PopUp.Error(title ?? "", text ?? "");
        }

        private async void Success()
        {
            await PopUp.Success();
            this.StateHasChanged();
        }

        #endregion
    }
}