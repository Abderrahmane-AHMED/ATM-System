using Microsoft.AspNetCore.Mvc;

namespace ATMSystem.Controllers
{
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
        public IActionResult QuickWithdrow()
        {
            return View();
        }
        public IActionResult NormalWithdrow()
        {
            return View();
        }
        public IActionResult Deposit()
        {
            return View();
        }
        public IActionResult CheckBalance()
        {
            return View();
        }
    }
}
