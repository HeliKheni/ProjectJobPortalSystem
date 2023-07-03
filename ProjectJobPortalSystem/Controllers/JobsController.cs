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
        public IActionResult Create(int employerId)
        {
            ViewBag.JobTypes = new SelectList(DataHelper.jobTypes);
            // var employersIds = DataHelper.GetEmployers().Select(js => js.Id).ToList(); // Retrieve all job seeker IDs
            // ViewBag.empId = new SelectList(employersIds);
            ViewBag.EmployerId = employerId;
            return View();
        }


        // POST: /Jobs/Create
        [HttpPost]
        public IActionResult Create(JobsModel jm)
        {
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
                //return RedirectToAction("List");
                return RedirectToAction("Details", "Employer", new { id = jm.EmployerId });
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
        public IActionResult Apply(int jobId, int id)
        {
            var jobs = DataHelper.GetJobs();
            var jobSeekers = DataHelper.getJokSeekers();

            var job = jobs.FirstOrDefault(j => j.Id == jobId);
            var jobSeeker = jobSeekers.First(js => js.Id == id);

            if (jobSeeker != null)
            {
                // Check if the job seeker has already applied for the job
                if (jobSeeker.jobs.Any(j => j.Id == jobId))
                {
                    ModelState.AddModelError(string.Empty, "You have already applied for this job.");
                    var jobSeekersIds = jobSeekers.Select(js => js.Id).ToList();
                    ViewBag.jobseekerlist = new SelectList(jobSeekersIds);
                    return View(job);
                }

                jobSeeker.jobs.Add(job);
                return RedirectToAction("List", "Jobs");
            }
            return View();
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
            
            ViewBag.JobTypes = new SelectList(DataHelper.jobTypes);
            var employersIds = DataHelper.GetEmployers().Select(js => js.Id).ToList(); // Retrieve all job seeker IDs
            ViewBag.empId = new SelectList(employersIds);
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
                ViewBag.JobTypes = new SelectList(DataHelper.jobTypes);
                var employersIds = DataHelper.GetEmployers().Select(js => js.Id).ToList(); // Retrieve all job seeker IDs
                ViewBag.empId = new SelectList(employersIds);
                jm.PostedDate = DateTime.Now;
              
               // DataHelper.GetJobs()[jm.Id - 1] = jm;

                
                
                var jobtoedit = DataHelper.GetJobs().First(a => a.Id == jm.Id);
                jobtoedit.PostedDate = DateTime.Now;
                jobtoedit.JobTitle = jm.JobTitle;
                jobtoedit.Description = jm.Description;
                jobtoedit.Location = jm.Location;
                jobtoedit.SalaryInfo = jm.SalaryInfo;
                jobtoedit.TypeofJob = jm.TypeofJob;
                jobtoedit.Website = jm.Website;
                //return RedirectToAction("List");
                return RedirectToAction("Details", "Employer", new { id = jm.EmployerId });

            }
            return View(jm);
        }

        // GET: /Jobs/Delete/
        public IActionResult Delete(int id)
        {
            var jobForDelete = DataHelper.GetJobs().FirstOrDefault(x => x.Id == id);

            // Retrieve the jobseeker details who applied for this job
            var jobSeekers = DataHelper.getJokSeekers().Where(js => js.jobs.Any(j => j.Id == id)).ToList();
            ViewBag.JobSeekers = jobSeekers;
            if (jobForDelete == null)
            {
                return RedirectToAction("List");
            }
           
            ViewBag.EmployerId = jobForDelete.EmployerId;
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

            /*  var jobtoremove = DataHelper.GetJobs().FirstOrDefault(a => a.Id == jm.Id); 
              if (jobtoremove != null)
              {
                  // Find the job seeker that has the job
                  var jobSeeker = DataHelper.getJokSeekers().FirstOrDefault(js => js.jobs.Any(j => j.Id == jm.Id));
                  if (jobSeeker != null)
                  {
                      // Remove the job from the job seeker's jobs
                      var jobToDelete = jobSeeker.jobs.FirstOrDefault(j => j.Id == jm.Id);
                      if (jobToDelete != null)
                      {
                          jobSeeker.jobs.Remove(jobToDelete);
                      }
                  }
                  //Remove the job from jobs model
                  DataHelper.GetJobs().Remove(jobtoremove);
                  var employerId = jobtoremove.EmployerId;
                  return RedirectToAction("Details", "Employer", new { id = employerId });
              }
              return RedirectToAction("List", "Employer");*/
            // return RedirectToAction("List");
            // POST: /Jobs/Delete/
           
           var jobToDelete = DataHelper.GetJobs().FirstOrDefault(a => a.Id == jm.Id);

                if (jobToDelete != null)
                {
                    // Find the job seeker that has the job
                    var jobSeekers = DataHelper.getJokSeekers().Where(js => js.jobs.Any(j => j.Id == jm.Id)).ToList();
                        foreach (var jobSeeker in jobSeekers)
                        {
                            var jobToRemove = jobSeeker.jobs.FirstOrDefault(j => j.Id == jm.Id);
                            if (jobToRemove != null)
                            {
                                jobSeeker.jobs.Remove(jobToRemove);
                            }
                        }
                

                    // Remove the job from the jobs model
                    DataHelper.GetJobs().Remove(jobToDelete);

                    var employerId = jobToDelete.EmployerId;

                    return RedirectToAction("Details", "Employer", new { id = employerId });
                }

                return RedirectToAction("List");
            


        }

        //GET : /Jobs/Details
        public IActionResult DetailsForEmployer(int id)
        {

            var job = DataHelper.GetJobs().FirstOrDefault(j => j.Id == id);

            if (job != null)
            {
                // Retrieve the jobseeker details who applied for this job
                var jobSeekers = DataHelper.getJokSeekers().Where(js => js.jobs.Any(j => j.Id == id)).ToList();
                ViewBag.JobSeekers = jobSeekers;
                return View(job);
            }

            return NotFound();
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
