using Microsoft.AspNetCore.Mvc;

namespace ark.forms
{
    [Route("ark/form")]
    public class FormController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
