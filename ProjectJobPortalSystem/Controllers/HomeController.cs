using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectJobPortalSystem.Models;
using System.Diagnostics;

namespace ProjectJobPortalSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Employer")]
        public IActionResult Index_Employer()
        {
            return View();
        }
        [Authorize(Roles = "JobSeeker")]
        public IActionResult Index_Jobseeker()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index_Admin()
        {
            return View();
        }

        [Route("/Home/NotFound")]
        public IActionResult NotFound(int? statusCode)
        {
            ViewData["StatusCode"] = statusCode;
            return View("404");
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