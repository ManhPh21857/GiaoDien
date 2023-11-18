using Microsoft.AspNetCore.Components;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Sales.Domain.Cart;
using WebClient.Shop.Shared.Alert;

namespace WebClient.Shop.Pages.Cart
{
    public partial class Index : ComponentBase
    {
        #region Inject
        
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Inject] public ICartDetailService cartdetailService { get; set; }
        public PopUp PopUp { get; set; }
        public UpdateCartdetailModal UpdateCartdetailModal { get; set; }
        #endregion

        public List<CartDetailModel> Cartdetails = new();
        private float? countTotal = 0;
        public CartDetailModel CartDetail { get; set; }
        protected async override Task<Task> OnInitializedAsync()
        { 
            await this.GetCartdetails();
            return base.OnInitializedAsync();
        }

        public void SelectItem(CartDetailModel model)
        {
            this.UpdateCartdetailModal.Load(model);
        }

        public async Task GetCartdetails()
        {
             
            var result = await this.cartdetailService.GetCartdetail();
          
            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<CartDetailResponseModel>().Data;
                var temp = response?.Cartdetails ?? new();
                temp.AddRange(Cartdetails.Where(x => string.IsNullOrEmpty(x.DataVersion)));
                Cartdetails = temp;
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }
          
        }

        private async void GotoProductDetail(int id)
        {
            var param = Cartdetails.Find(x => x.ProductId == id);
        }
        //public async Task UpdateCartdetail()
        //{
        //    var result = await this.cartdetailService.UpdateCartdetail(Cartdetails);

        //    if (result.IsSuccessStatusCode)
        //    {
        //        Cartdetails.Clear();
        //        //await PopUp.Success();
        //        await this.GetCartdetails();
        //    }
        //    else
        //    {
        //        var error = result.ConvertResponse<ErrorModel>().Data;
        //        NavigationManager.NavigateTo("/error");
        //       // await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
        //    }
        //}

        public async Task DeleteCartdetail(int? idcart, int? idproduct)
        {
            var param = Cartdetails.Find(x => x.CartId == idcart && x.ProductDetailId == idproduct);

            if (param is null)
            {
                await PopUp.Error("Opp!!", "Somethings went wrong");
            }
            else
            {
                if (string.IsNullOrEmpty(param.DataVersion))
                {
                    Cartdetails.Remove(param);
                    this.StateHasChanged();
                }
                else
                {
                    var result = await this.cartdetailService.DeleteCartdetail(param);

                    if (result.IsSuccessStatusCode)
                    {
                        await GetCartdetails();
                        this.StateHasChanged();
                    }
                    else
                    {
                        var error = result.ConvertResponse<ErrorModel>().Data;

                        await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
                    }
                }
            }
        }

        public async Task DeleteProduct()
        {
            foreach(var item in Cartdetails.Where(x => x.Adoption))
            {
                var delete = await cartdetailService.DeleteCartdetail(item);
                if (delete.IsSuccessStatusCode)
                {
                    await this.GetCartdetails();
                    checkAll = false;
                     this.CountTotal();
                }
                
            }
        }

        public bool checkAll = false;
        public void CheckAll()
        {
            checkAll = !checkAll;
            Cartdetails.ForEach(x => x.Adoption = checkAll);

            this.CountTotal();
            this.StateHasChanged();
        }

        public void SelectProduct(int? id)
        {
            var item = Cartdetails.Find(p=>p.ProductDetailId == id);
            if(item is not null)
            {
                item.Adoption = !item.Adoption;
            }
            checkAll = Cartdetails.All(c => c.Adoption) ? true : false;

            this.CountTotal();
           
        }
        public void CountTotal()
        {
            countTotal = 0;
            foreach(var item in Cartdetails.Where(x=>x.Adoption))
            {
                countTotal += item.Price * item.Quantity;

            }

        }

        public void ContinueShopping()
        {
            navigationManager.NavigateTo("/product");
        }
    }
}
