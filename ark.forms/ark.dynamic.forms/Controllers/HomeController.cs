using ark.dynamic.forms.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ark.dynamic.forms.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] string full_name, 
            [FromForm] string email_phone, 
            [FromForm] string profession, 
            [FromForm] string accomponied,
            [FromForm] string fellowship_src)
        {
            if (string.IsNullOrEmpty(full_name))
            {
                ViewBag.error = "Please enter your name.";
                return RedirectToAction("Index");
            }
            new FormCapture("cg_ggg").CaptureData(new Dictionary<string, string?>() {
                { "full_name", full_name },
                { "email_phone", email_phone },
                { "profession", profession },
                { "accomponied", accomponied },
                { "fellowship_src", fellowship_src }
            });
            return RedirectToAction("Thanks");
        }
        public IActionResult Thanks()
        {
            return View();
        }

        public IActionResult List()
        {
            ViewBag.Count = new FormCapture("cg_ggg").FetchCount();
            ViewBag.List = new FormCapture("cg_ggg").FetchData();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}