using Microsoft.AspNetCore.Mvc;

namespace project.Pages.client
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult loginForm()
        {

            formmodel students = new formmodel();
            return View(students);
        }
    }
}
