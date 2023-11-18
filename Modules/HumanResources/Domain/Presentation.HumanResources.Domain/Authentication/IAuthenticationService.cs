namespace Presentation.HumanResources.Domain.Authentication
{
    public interface IAuthenticationService
    {
        Task<HttpResponseMessage> Login(LoginRequestModel request);

        Task<HttpResponseMessage> Register(RegisterRequestModel request);
    }
}
