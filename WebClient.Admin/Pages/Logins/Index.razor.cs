using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Presentation.Core.Domain;
using Presentation.Core.Domain.Authentication;
using Presentation.Core.Service;
using Presentation.HumanResources.Domain.Authentication;
using WebClient.Admin.Shared.Alert;

namespace WebClient.Admin.Pages.Logins
{
    public partial class Index : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IAuthenticationService AuthenticationService { get; set; }
        [Inject] public ILocalStorageService LocalStorageService { get; set; }
        public PopUp PopUp { get; set; }


        private LoginRequestModel loginRequestModel = new();

        private string usernameValidator = "";
        private string passwordValidator = "";

        private bool ValidatorUsername()
        {
            if (string.IsNullOrEmpty(loginRequestModel.UserName))
            {
                usernameValidator = "is-invalid";
                return false;
            }
            else
            {
                usernameValidator = "is-valid";
                return true;
            }
        }

        private bool ValidatorPassword()
        {
            if (string.IsNullOrEmpty(loginRequestModel.Password))
            {
                passwordValidator = "is-invalid";
                return false;
            }
            else
            {
                passwordValidator = "is-valid";
                return true;
            }
        }

        private bool ValidatorForm()
        {
            return this.ValidatorUsername() && this.ValidatorPassword();
        }

        private async Task Login()
        {
            if (!this.ValidatorForm())
            {
                return;
            }

            var response = await this.AuthenticationService.Login(loginRequestModel);

            if (!response.IsSuccessStatusCode)
            {
                var error = response.ConvertResponse<ErrorModel>().Data;
                await this.PopUp.Error($"{error?.ErrorCode}", $"{error?.ErrorMessage}");
                return;
            }
            var loginResponseModel = response.ConvertResponse<LoginResponseModel>().Data;
            if (loginResponseModel is not { Result: not null })
            {
                await this.PopUp.Error("Opp!", "Somethings went wrong");
                return;
            };
            var token = new Token(loginResponseModel.Result);
            await LocalStorageService.SetItemAsync("token", token);

            this.NavigationManager.NavigateTo("/");
        }
    }
}