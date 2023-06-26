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
        public IActionResult Create(JobsModel jm)
        {
            /* var jobs = DataHelper.getJokSeekers();
             ViewBag.empId = new SelectList(DataHelper.empId);
             int newJobId = jobs.SelectMany(js => js.jobs).Max(j => j.Id) + 1;
             job.Id = newJobId;
             jobs[1].jobs.Add(job);
             return RedirectToAction("List");*/
           ViewBag.empId = new SelectList(DataHelper.empId);
            var jobs = DataHelper.GetJobs();
            if (jobs.Count == 0)
            {
                jm.Id = 1;
            }
            else
            {
                //qm.Id = DataHelper.getQuestionList().Count() + 1;
                jm.Id = jobs.Max(x => x.Id) + 1;
            }
            if (ModelState.IsValid)
            {
                jm.PostedDate = DateTime.Now;
                DataHelper.GetJobs().Add(jm);
                return RedirectToAction("List");
            }
            return View(jm);

        }

        //GET : //Jobs/Apply
        public IActionResult Apply(int id)
        {
            var jobForApply = DataHelper.GetJobs().First(x => x.Id == id);
            
            var jobSeekers = DataHelper.getJokSeekers();
            var jobSeekerIds = jobSeekers.Select(js => js.Id).ToList(); // Retrieve all job seeker IDs
            ViewBag.jobseekerlist = new SelectList(jobSeekerIds);
            return View(jobForApply);
        }

        // POST: /Jobs/Apply
        [HttpPost]
        public IActionResult Apply(int jobId, int Id)
        {
           
           var job = DataHelper.GetJobs().First(x => x.Id == jobId);
            var jobSeeker = DataHelper.getJokSeekers().FirstOrDefault(js => js.Id == Id);

                if (jobSeeker != null)
                {
                    jobSeeker.jobs.Add(job);
                    
                    return RedirectToAction("List", "Jobs"); 
                }
            
            // If the job seeker is not found or the model state is invalid, return back to the apply page
            var jobSeekerIds = DataHelper.getJokSeekers().Select(js => js.Id).ToList();
            ViewBag.jobseekerlist = new SelectList(jobSeekerIds);
            return View();

           // var jobs = DataHelper.getJokSeekers();
             //ViewBag.jobseekerid = new SelectList(DataHelper.getJokSeekers());
           /*  int newJobId = jobs.SelectMany(js => js.jobs).Max(j => j.Id) + 1;
             jm.Id = newJobId;
             jobs[1].jobs.Add(jm);
             return RedirectToAction("List");*/

            // Retrieve the Job Seeker based on the selected Job Seeker ID
           /* var selectedJobSeeker = DataHelper.getJokSeekers().FirstOrDefault(js => js.Id == jm.Id);

            if (selectedJobSeeker != null)
            {
                int newJobId = jobs.SelectMany(js => js.jobs).Max(j => j.Id) + 1;
                jm.Id = newJobId;

                // Assign the Job Seeker ID to the job
                jm.Id = selectedJobSeeker.Id;

                // Add the job to the selected Job Seeker's list of jobs
                selectedJobSeeker.jobs.Add(jm);

                return RedirectToAction("List");
            }

            return BadRequest("Invalid Job Seeker ID");*/

        }
        //GET : /Jobs/Edit
        public IActionResult Edit(int id)
        {

            /*var jobSeekers = DataHelper.getJokSeekers();
            var job = jobSeekers.SelectMany(j => j.jobs).FirstOrDefault(j => j.Id == id);
            ViewBag.empId = new SelectList(DataHelper.empId);
            if (job != null)
            {
                return View(job);
            }
            return NotFound();*/
            ViewBag.empId = new SelectList(DataHelper.empId);
            var jobForEdit = DataHelper.GetJobs().First(x => x.Id == id);
            return View(jobForEdit);

        }

        // POST: /Jobs/Edit
        [HttpPost]
        public IActionResult Edit(JobsModel jm)
        {
           /* if (ModelState.IsValid)
            {
                var jobSeekers = DataHelper.getJokSeekers();
                var existingJob = jobSeekers.SelectMany(j => j.jobs).FirstOrDefault(j => j.Id == job.Id);
                ViewBag.empId = new SelectList(DataHelper.empId);
                if (existingJob != null)
                {
                    return RedirectToAction("List");
                }
            }

            return View(job);*/
            if (ModelState.IsValid)
            {
                ViewBag.empId = new SelectList(DataHelper.empId);
                jm.PostedDate = DateTime.Now;
                DataHelper.GetJobs()[jm.Id - 1] = jm;
                return RedirectToAction("List");
            }
            return View(jm);
        }

        // GET: /Jobs/Delete/
        public IActionResult Delete(int id)
        {
            /*  var jobSeeker = DataHelper.getJokSeekers().FirstOrDefault(js => js.jobs.Any(j => j.Id == id));
              if (jobSeeker != null)
              {
                  var jobToDelete = jobSeeker.jobs.FirstOrDefault(j => j.Id == id);
                  if (jobToDelete != null)
                  {
                      return View(jobToDelete);
                  }
              }
              return NotFound();*/
            var jobForDelete = DataHelper.GetJobs().FirstOrDefault(x => x.Id == id);

            if (jobForDelete == null)
            {
                return RedirectToAction("List");
            }

            return View(jobForDelete);
        }

        // POST: /Jobs/Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(JobsModel jm)
        {
            /*var jobSeeker = DataHelper.getJokSeekers().FirstOrDefault(js => js.jobs.Any(j => j.Id == job.Id));
            if (jobSeeker != null)
            {
                var jobToDelete = jobSeeker.jobs.FirstOrDefault(j => j.Id == job.Id);
                if (jobToDelete != null)
                {
                    jobSeeker.jobs.Remove(jobToDelete);
                    return RedirectToAction("List");
                }
            }
            return NotFound();*/
            var jobtoremove = DataHelper.GetJobs().FirstOrDefault(a => a.Id == jm.Id);

            if (jobtoremove != null)
            {
                DataHelper.GetJobs().Remove(jobtoremove);

            }
            return RedirectToAction("List");
        }

        //GET : /Jobs/Details
        public IActionResult Details(int id)
        {
            var job = DataHelper.GetJobs().FirstOrDefault(j => j.Id == id);

            if (job != null)
            {
                // Retrieve the jobseeker details who applied for this job
                var jobSeekers = DataHelper.getJokSeekers().Where(js => js.jobs.Any(j => j.Id == id)).ToList();
                ViewBag.JobSeekers = jobSeekers;

                // Retrieve the employer details
                // Fetch employer details based on the EmployerId in the job
                var employer = DataHelper.GetEmployers().FirstOrDefault(e => e.Id == job.EmployerId);
                ViewBag.Employer = employer;

                return View(job);
            }

            return NotFound();
        }


    }
}
