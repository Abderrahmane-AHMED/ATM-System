using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATMSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MainMenueScreen()
        {
            return View();
        }
    
        public IActionResult CheckBalance()
        {
            return View();
        }
    }
}
