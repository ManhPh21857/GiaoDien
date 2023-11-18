using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Presentation.Core.Domain.Authentication;
using Presentation.Core.Service;
using Presentation.HumanResources.Domain.Authentication;

namespace WebClient.Shop.Pages
{
  public class Login : ComponentBase
  {
    [Inject] public IAuthenticationService AuthenticationService { get; set; }
    [Inject] public ILocalStorageService LocalStorageService { get; set; }

    public LoginRequestModel login = new();

    public async void LoginMethod()
    {
      var response = await AuthenticationService.Login(login);

      if (!response.IsSuccessStatusCode)
      {
        Console.WriteLine("false");
        return;
      }
      var loginResponseModel = response.ConvertResponse<LoginResponseModel>().Data;
      if (loginResponseModel is not { Result: not null })
      {
        Console.WriteLine("false");
        return;
      };
      var token = new Token(loginResponseModel.Result);

      await LocalStorageService.SetItemAsync("token", token);

      Console.WriteLine("success");

      var newToken = await LocalStorageService.GetItemAsStringAsync("token");

      Console.WriteLine(newToken);
    }
  }
}