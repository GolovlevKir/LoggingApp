using LogApp2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LogApp2.Controllers
{
    public class StatistikaController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<mvcLoggingModel> emplist;
            HttpResponseMessage response;
            response = GlobalVariables.WebApiClient.GetAsync("NLogs").Result;
            emplist = response.Content.ReadAsAsync<IEnumerable<mvcLoggingModel>>().Result.OrderByDescending(s => s.Logged);

            return View(emplist);
        }
    }
}
