using LogApp2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace LogApp2.Controllers
{
    public class mvcLoggingModelController : Controller
    {

        [HttpGet]
        [Route("mvcLoggingModel/{id}/{yroven}/{text}/{start}/{end}")]
        public IActionResult Index(int? id, string yroven, string text, string start, string end)
        {
            id = 0;
            IEnumerable<mvcLoggingModel> emplist;
            HttpResponseMessage response;
            if (!string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(yroven) || !string.IsNullOrEmpty(start) || !string.IsNullOrEmpty(end))
            {
                response = GlobalVariables.WebApiClient.GetAsync("NLogs/" + id.ToString() + "/" + yroven + "/" + text + "/" + start + "/" + end).Result;

                emplist = response.Content.ReadAsAsync<IEnumerable<mvcLoggingModel>>().Result.OrderByDescending(s => s.Logged);
            }
            else
            {
                response = GlobalVariables.WebApiClient.GetAsync("NLogs").Result;
                emplist = response.Content.ReadAsAsync<IEnumerable<mvcLoggingModel>>().Result.OrderByDescending(s => s.Logged);
            }

            return View(emplist);
        }


        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcLoggingModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("NLogs/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcLoggingModel>().Result);
            }
        }

        [HttpPost]
        public IActionResult AddOrEdit(mvcLoggingModel mvc)
        {
            if (mvc.Id == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("NLogs", mvc).Result;
                TempData["SuccessMessage"] = "Запись добавлена!!!";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("NLogs/" + mvc.Id, mvc).Result;
                TempData["SuccessMessage"] = "Данные записи изменены!!!";
            }

            return RedirectToAction("Index", new { id = 0, yroven = "null", text = "null", start = "null", end = "null" });
        }

        
        public IActionResult Delete(int id = 0)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("NLogs/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Запись №" + id.ToString() + " удалена!!!";
            Program.ch++;
            TempData["DeleteMessage"] = "Всего удалено записей за последнее время: " + Program.ch.ToString();
            return RedirectToAction("Index", new { id = 0, yroven = "null", text  = "null", start = "null", end = "null" }) ;
        }
    }
}
