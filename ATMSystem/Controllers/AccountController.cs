using Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ATMSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IClientService _clientService;

        public AccountController(IClientService clientService)
        {
            _clientService = clientService;
        }

        #region Login / Logout

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string accountNumber, string pinCode, string? returnUrl = null)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                ModelState.AddModelError("", "Account Number is required.");
                return View();
            }

            if (!int.TryParse(pinCode, out int pin))
            {
                ModelState.AddModelError("", "PIN must be numbers only.");
                return View();
            }

            var client = _clientService.FindByAccountNumber(accountNumber);
            if (client == null)
            {
                ModelState.AddModelError("", "Invalid Account Number.");
                return View();
            }

            if (client.PinCode != pin)
            {
                ModelState.AddModelError("", "Invalid PIN.");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, client.FullName()),
                new Claim("ClientId", client.ClientId.ToString()),
                new Claim("AccountNumber", client.AccountNumber)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // تسجيل الدخول
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
                });

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("MainMenueScreen", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/Account/Login");
        }

        #endregion
    }
}
