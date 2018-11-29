using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AndreasBank.Models;
using AndreasBank.Services;
using Microsoft.AspNetCore.Mvc;

namespace AndreasBank.Controllers
{
    public class TransferController : Controller
    {
        private readonly IBankService _bankService;

        public TransferController(IBankService bankService)
        {
            _bankService = bankService;
        }

        public IActionResult Index()
        {
            return View(new TransferViewModel());
        }

        [HttpPost]
        public IActionResult Transfer(TransferViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = _bankService.Transfer(vm.FromAccountId, vm.ToAccountId, vm.Amount);
                if (isSuccess)
                {
                    var fromAccount = _bankService.GetAccountById(vm.FromAccountId);
                    var toAccount = _bankService.GetAccountById(vm.ToAccountId);

                    return View(nameof(Index), new TransferViewModel
                    {
                        FromAccountId = vm.FromAccountId,
                        ToAccountId = vm.ToAccountId,
                        Amount = vm.Amount,
                        FromActualSumString = fromAccount.ActualSum.ToString("C"),
                        ToActualSumString = toAccount.ActualSum.ToString("C")
                    });
                }
                else
                {
                    ModelState.AddModelError("FromAccountId", "Det finns inte tillräckligt med pengar på ditt konto.");
                }
            }
            return View(nameof(Index), vm);
        }
    }
}