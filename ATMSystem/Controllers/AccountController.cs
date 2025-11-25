using Domain;
using Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        public async Task<IActionResult> Login(string accountNumber, int pinCode, string? returnUrl = null)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                ModelState.AddModelError("", "Account Number is required.");
                return View();
            }

            if (pinCode <= 0)
            {
                ModelState.AddModelError("", "PIN Code is required.");
                return View();
            }

            var client = _clientService.FindByAccountNumber(accountNumber);

            if (client == null)
            {
                ModelState.AddModelError("", "Invalid Account Number.");
                return View();
            }

            if (client.PinCode != pinCode)
            {
                ModelState.AddModelError("", "Invalid PIN.");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, client.FullName()),
                new Claim("ClientId", client.ClientId.ToString()),
                new Claim("AccountNumber", client.AccountNumber),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(2)
                });

            return Redirect(returnUrl ?? "/");
        }

       
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/");
        }


        #endregion
    }
}
