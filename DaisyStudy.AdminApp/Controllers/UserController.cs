using Microsoft.AspNetCore.Mvc;
using DaisyStudy.ViewModels.System.Users;
using DaisyStudy.AdminApp.Service;
using System.Security.Claims;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DaisyStudy.AdminApp.Controllers;

public class UserController : Controller
{
    private readonly IUserApiClient _userApiClient;
    private readonly IConfiguration _configuration;
    //private readonly IRoleApiClient _roleApiClient;
    public UserController(IUserApiClient userApiClient,
        //IRoleApiClient roleApiClient,
        IConfiguration configuration)
    {
        _userApiClient = userApiClient;
        _configuration = configuration;
        //_roleApiClient = roleApiClient;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
            return View(ModelState);
        var token = await _userApiClient.Authenticate(request);

        var userPrincipal = this.ValidateToken(token);
        var authProperties = new AuthenticationProperties{
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            IsPersistent = false
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            userPrincipal,
            authProperties);
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }

    private ClaimsPrincipal ValidateToken(string jwtToken)
    {
        IdentityModelEventSource.ShowPII = true;

        SecurityToken validatedToken;
        TokenValidationParameters validationParameters = new TokenValidationParameters();

        validationParameters.ValidateLifetime = true;

        validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
        validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
        validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

        ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

        return principal;
    }
}

