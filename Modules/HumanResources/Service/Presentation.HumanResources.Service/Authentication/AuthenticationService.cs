using Blazored.LocalStorage;
using Presentation.Core.Service;
using Presentation.HumanResources.Domain.Authentication;
using System.Text.Json;

namespace Presentation.HumanResources.Service.Authentication;

public class AuthenticationService : ApiClient, IAuthenticationService 
    {
    public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorageService) : base(httpClient, localStorageService)
    {
    }

    public async Task<HttpResponseMessage> Login(LoginRequestModel request) {
        var response = await this.PostAsync(Endpoint.LOGIN, request);

        return response;
    }

    public async Task<HttpResponseMessage> Register(RegisterRequestModel request)
    {
        var response = await this.PostAsync(Endpoint.Register, request);
        return response;
    }
}