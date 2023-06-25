using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        //GET : //Jobs/Create
        public IActionResult Create()
        {
            ViewBag.empId = new SelectList(DataHelper.empId);
            return View();
        }


        // POST: /Jobs/Create
        [HttpPost]
        public IActionResult Create(JobsModel job)
        {
            var jobs = DataHelper.getJokSeekers();
            ViewBag.empId = new SelectList(DataHelper.empId);
            int newJobId = jobs.SelectMany(js => js.jobs).Max(j => j.Id) + 1;
            job.Id = newJobId;
            jobs[1].jobs.Add(job);
            return RedirectToAction("List");
        }

        //GET : /Jobs/Edit
        public IActionResult Edit(int id)
        {

            var jobSeekers = DataHelper.getJokSeekers();
            var job = jobSeekers.SelectMany(j => j.jobs).FirstOrDefault(j => j.Id == id);
            ViewBag.empId = new SelectList(DataHelper.empId);
            if (job != null)
            {
                return View(job);
            }

            return NotFound();
        }

        // POST: /Jobs/Edit
        [HttpPost]
        public IActionResult Edit(JobsModel job)
        {
            if (ModelState.IsValid)
            {
                var jobSeekers = DataHelper.getJokSeekers();
                var existingJob = jobSeekers.SelectMany(j => j.jobs).FirstOrDefault(j => j.Id == job.Id);
                ViewBag.empId = new SelectList(DataHelper.empId);
                if (existingJob != null)
                {
                    return RedirectToAction("List");
                }
            }

            return View(job);
        }

        // GET: /Jobs/Delete/
        public IActionResult Delete(int id)
        {
            var jobSeeker = DataHelper.getJokSeekers().FirstOrDefault(js => js.jobs.Any(j => j.Id == id));

            if (jobSeeker != null)
            {
                var jobToDelete = jobSeeker.jobs.FirstOrDefault(j => j.Id == id);
                if (jobToDelete != null)
                {
                    return View(jobToDelete);
                }
            }

            return NotFound();
        }

        // POST: /Jobs/Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(JobsModel job)
        {
            var jobSeeker = DataHelper.getJokSeekers().FirstOrDefault(js => js.jobs.Any(j => j.Id == job.Id));

            if (jobSeeker != null)
            {
                var jobToDelete = jobSeeker.jobs.FirstOrDefault(j => j.Id == job.Id);
                if (jobToDelete != null)
                {
                    jobSeeker.jobs.Remove(jobToDelete);
                    return RedirectToAction("List");
                }
            }

            return NotFound();
        }

        //GET : /Jobs/Details
        public IActionResult Details(int id)
        {
            var job = DataHelper.GetJobs().FirstOrDefault(j => j.Id == id);

            if (job != null)
            {
                return View(job);
            }

            return NotFound();
        }

    }
}
