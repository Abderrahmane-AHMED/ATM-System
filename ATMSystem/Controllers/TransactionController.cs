using Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATMSystem.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {

        private readonly IClientService _clientService;
        private readonly ILogger<TransactionController> _logger;
        public TransactionController(IClientService clientService, ILogger<TransactionController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region  Quick Withdrow 
        [HttpGet]
        public async Task<IActionResult> QuickWithdrow()
        {
           string? accountNumber = User.FindFirst("AccountNumber")?.Value;

            var client = await _clientService.FindByAccountNumberAsync(accountNumber);
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuickWithdraw(decimal amount)
        {
            string? accountNumber = User.FindFirst("AccountNumber")?.Value;

            var allowedAmounts = new decimal[] { 10, 20, 50, 100, 200, 300 , 400 ,500 };
            if (!allowedAmounts.Contains(amount))
                return BadRequest("Invalid quick withdraw amount.");

            try
            {
                await _clientService.ClientWithdrawAsync(accountNumber, amount);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ErrorPage");
            }

            return RedirectToAction("WithdrawConfirmation");
        }
        #endregion
        #region   Deposit

        [HttpGet]
        public async Task<IActionResult> Deposit()
        {
            string? accountNumber = User.FindFirst("AccountNumber")?.Value;

            if (string.IsNullOrEmpty(accountNumber))
            {
                TempData["ErrorMessage"] = "Account number not found.";
                return RedirectToAction("ErrorPage");
            }

            var client = await _clientService.FindByAccountNumberAsync(accountNumber);

            if (client == null)
            {
                TempData["ErrorMessage"] = "Client not found.";
                return RedirectToAction("ErrorPage");
            }

            return View(client);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(decimal depositAmount)
        {
            string? accountNumber = User.FindFirst("AccountNumber")?.Value;

            if (string.IsNullOrEmpty(accountNumber))
            {
                TempData["ErrorMessage"] = "Account number not found.";
                return RedirectToAction("ErrorPage");
            }

            if (depositAmount <= 0)
            {
                var existing = await _clientService.FindByAccountNumberAsync(accountNumber);
                ModelState.AddModelError("", "Please enter a valid amount greater than 0.");
                return View(existing);
            }

            await _clientService.ClientDepositAsync(accountNumber, depositAmount);

            return RedirectToAction("DepositConfirmation");
        }


        [HttpGet]
        public async Task<IActionResult> DepositConfirmation()
        {
            string? accountNumber = User.FindFirst("AccountNumber")?.Value;
            var client = await _clientService.FindByAccountNumberAsync(accountNumber);
            return View(client);
        }

        #endregion

        #region Normal Withdrow

        [HttpGet]
        public async Task<IActionResult> NormalWithdrow()
        {
            string? accountNumber = User.FindFirst("AccountNumber")?.Value;

            var client = await _clientService.FindByAccountNumberAsync(accountNumber);

            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NormalWithdrow(decimal withdrowAmount)
        {
            string? accountNumber = User.FindFirst("AccountNumber")?.Value;
            if (withdrowAmount < 10 || withdrowAmount % 10 != 0)
            {
                ModelState.AddModelError("", "You can only withdraw in multiples of ten .");
                var existing = await _clientService.FindByAccountNumberAsync(accountNumber);
                return View(existing);
            }
            try
            {
                await _clientService.ClientWithdrawAsync(accountNumber, withdrowAmount);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var existing = await _clientService.FindByAccountNumberAsync(accountNumber);
                return View(existing);
            }
            return RedirectToAction("WithdrawConfirmation");
        }


        [HttpGet]
        public async Task<IActionResult> WithdrawConfirmation()
        {
            string? accountNumber = User.FindFirst("AccountNumber")?.Value;
            var client = await _clientService.FindByAccountNumberAsync(accountNumber);
            return View(client);
        }
        #endregion


        [HttpGet]
        public IActionResult ErrorPage()
        {

            ViewBag.ErrorMessage = TempData["ErrorMessage"]?.ToString();
            return View();
        }
    }
}
