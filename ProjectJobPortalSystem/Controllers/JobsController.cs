using Microsoft.AspNetCore.Mvc;
using ProjectJobPortalSystem.Models;

namespace ProjectJobPortalSystem.Controllers
{
    public class JobsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //GET : /Jobs/List
        public IActionResult List()
        {   
            return View(DataHelper.GetJobs());
           
        }

    }
}
