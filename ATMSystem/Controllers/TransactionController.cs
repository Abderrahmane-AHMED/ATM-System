using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATMSystem.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        public IActionResult Index()
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
    }
}
