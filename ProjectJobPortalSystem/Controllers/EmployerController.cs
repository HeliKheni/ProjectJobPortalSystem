using Microsoft.AspNetCore.Mvc;
using ProjectJobPortalSystem.Models;

namespace ProjectJobPortalSystem.Controllers
{
    public class EmployerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //GET : /Employer/List
        public IActionResult List()
        {
            return View(DataHelper.GetEmployers());
        }

    }
}
