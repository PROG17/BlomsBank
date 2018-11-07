using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AndreasBank.Models;
using AndreasBank.Repositories;

namespace AndreasBank.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBankRepository _bankRepository;

        public HomeController(IBankRepository bankRepo)
        {
            _bankRepository = bankRepo;
        }

        public IActionResult Index()
        {
            return View(_bankRepository.Customers);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
