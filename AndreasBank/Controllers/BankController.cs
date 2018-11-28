using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AndreasBank.Exceptions;
using AndreasBank.Models;
using AndreasBank.Services;
using Microsoft.AspNetCore.Mvc;

namespace AndreasBank.Controllers
{
    public class BankController : Controller
    {
        private readonly IBankService _bankService;

        public BankController(IBankService bankService)
        {
            _bankService = bankService;
        }

        public IActionResult Index()
        {
            return View(new IndexViewModel());
        }

        [HttpPost]
        public IActionResult Deposit(IndexViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var isSuccess = _bankService.Deposit(vm.AccountId, vm.Amount);
                    if (isSuccess)
                    {
                        var account = _bankService.GetAccountById(vm.AccountId);
                        return View(nameof(Index), new IndexViewModel
                        {
                            AccountId = vm.AccountId,
                            Amount = vm.Amount,
                            ActualSumString = account.ActualSum.ToString("C")
                        });
                    }
                    else
                    {
                        ModelState.AddModelError("AccountId", "Det gick inte att utföra en insättning. Vänligen kontakta banken.");
                    }
                }
                catch (AccountNotFoundException e)
                {
                    ModelState.AddModelError("AccountId", e.Message);
                }
                catch (NonPositiveValueException e)
                {
                    ModelState.AddModelError("Amount", e.Message);
                }
            }
            return View(nameof(Index), new IndexViewModel
            {
                AccountId = vm.AccountId,
                Amount = vm.Amount,
                ActualSumString = vm.ActualSumString
            });
        }

        [HttpPost]
        public IActionResult Withdraw(IndexViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var isSuccess = _bankService.Withdraw(vm.AccountId, vm.Amount);
                    if (isSuccess)
                    {
                        var account = _bankService.GetAccountById(vm.AccountId);
                        return View(nameof(Index), new IndexViewModel
                        {
                            AccountId = vm.AccountId,
                            Amount = vm.Amount,
                            ActualSumString = account.ActualSum.ToString("C")
                        });
                    }
                    else
                    {
                        ModelState.AddModelError("AccountId", "Det gick inte att utföra ett uttag. Vänligen kontakta banken.");
                    }
                }
                catch (AccountNotFoundException e)
                {
                    ModelState.AddModelError("AccountId", e.Message);
                }
                catch (InsufficientFundsException e)
                {
                    ModelState.AddModelError("Amount", e.Message);
                }
                catch (NonPositiveValueException e)
                {
                    ModelState.AddModelError("Amount", e.Message);
                }
            }
            return View(nameof(Index), new IndexViewModel
            {
                AccountId = vm.AccountId,
                Amount = vm.Amount,
                ActualSumString = vm.ActualSumString
            });
        }

        public IActionResult GetActualSumByAccountId(int id)
        {
            var account = _bankService.GetAccountById(id);
            if(account == null)
            {
                return Json(new { isSuccess = false, text = "Det finns inget konto med angivet kontonummer" });
            }
            else
            {
                return Json(new { isSuccess = true, text = account.ActualSum.ToString("C") });
            }
        }
    }
}
