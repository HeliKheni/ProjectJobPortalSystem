using Microsoft.AspNetCore.Mvc;

namespace ProjectJobPortalSystem.Controllers
{
    public class EmployerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
