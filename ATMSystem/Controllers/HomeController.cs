using Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATMSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IClientService _clientService;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IClientService clientService, ILogger<HomeController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MainMenueScreen()
        {
            return View();
        }
    
        public async Task<IActionResult> CheckBalance()
        {
            string? accontNumber = User.FindFirst("AccountNumber")?.Value;

            var client = await _clientService.FindByAccountNumberAsync(accontNumber);

            return View(client);
        }
    }
}
