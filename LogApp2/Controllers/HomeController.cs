using LogApp2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LogApp2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "Страница контроллера HomeController");
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Открытие главной страницы сайта (HomeController/Index)");
            return View();
        }
    }
}
