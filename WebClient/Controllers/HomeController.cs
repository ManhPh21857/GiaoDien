using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.HumanResources.Domain.Authentication;


namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthenticationService _authenticationService;
        public HomeController(ILogger<HomeController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            this._authenticationService = authenticationService;
           
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Privacy(LoginResponseModel model)
        {
            return View(model);
        }

        public IActionResult Error(ErrorModel model)
        {
            return View(model);
        }

        public async Task<IActionResult> Login(string username, string password) {
            var loginModel = new LoginRequestModel {
                UserName = username,
                Password = password
            };

            var response = await _authenticationService.Login(loginModel);

            if(response.IsSuccessStatusCode) {
                return RedirectToAction(nameof(Privacy), response.ConvertResponse<LoginResponseModel>().Data);
            }

            return RedirectToAction(nameof(Error), response.ConvertResponse<ErrorModel>().Data);
        }

        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> Register123(string username, string password, string email, string firstname, string lastname, string repassword)
        {
            var Register = new RegisterRequestModel
            {
                Username = username,
                Password = password,
                Email = email,
                FirstName = firstname,
                LastName = lastname,
                rePassword = repassword
            };
            var result = await _authenticationService.Register(Register);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index), JsonConvert.DeserializeObject<ResponseBaseModel<LoginResponseModel>>(result.Content.ReadAsStringAsync().Result).Data);
            }
            return RedirectToAction(nameof(Error), JsonConvert.DeserializeObject<ResponseBaseModel<ErrorModel>>(result.Content.ReadAsStringAsync().Result).Data);
        }

        public IActionResult Accuracy()
        {
            return View();
        }

    }
}