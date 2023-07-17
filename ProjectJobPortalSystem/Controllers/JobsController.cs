using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectJobPortalSystem.Data;
using ProjectJobPortalSystem.Models;

namespace ProjectJobPortalSystem.Controllers
{
    public class JobsController : Controller
    {
        public readonly ApplicationDbContext _context;

        public JobsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        //GET : /Jobs/List
        public IActionResult List()
        {
            //return View(DataHelper.GetJobs());
            return View(_context.Jobs.ToList());
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
           /* var jobs = DataHelper.GetJobs();
            if (jobs.Count == 0)
            {
                jm.Id = 1;
            }
            else
            {
                jm.Id = jobs.Max(x => x.Id) + 1;
            }*/
            jm.PostedDate = DateTime.Now;
                //DataHelper.GetJobs().Add(jm);
                _context.Jobs.Add(jm);
                _context.SaveChanges();

                return RedirectToAction("Details", "Employer", new { id = jm.EmployerId });
           

        }

        //GET : //Jobs/Apply
        public IActionResult Apply(int id)
        {
            /* var jobForApply = DataHelper.GetJobs().First(x => x.Id == id);
             var jobSeekers = DataHelper.getJokSeekers();
             var jobSeekerIds = jobSeekers.Select(js => js.Id).ToList(); // Retrieve all job seeker IDs
             ViewBag.jobseekerlist = new SelectList(jobSeekerIds);
             return View(jobForApply);*/

            var jobForApply = _context.Jobs.Find(id);
            var jobSeekerIds = _context.JobSeekers.Select(js => js.Id).ToList();
            ViewBag.jobseekerlist = new SelectList(jobSeekerIds);
            return View(jobForApply);
        }

        // POST: /Jobs/Apply
        [HttpPost]
        public IActionResult Apply(int jobId, int id)
        {
           /* var jobs = DataHelper.GetJobs();
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
           */
           var jobs = _context.Jobs.Find(jobId);
           var jobSeeker = _context.JobSeekers.Find(id);
           
           jobs.appliedJobSeekers.Add(jobSeeker);
            jobSeeker.jobs.Add(jobs);
            _context.Jobs.Update(jobs);
            _context.JobSeekers.Update(jobSeeker);
            _context.SaveChanges();

            return RedirectToAction("List", "Jobs");
        }

        //GET : /Jobs/Edit
        public IActionResult Edit(int id)
        {
            ViewBag.JobTypes = new SelectList(DataHelper.jobTypes);
            /*var employersIds = DataHelper.GetEmployers().Select(js => js.Id).ToList(); // Retrieve all job seeker IDs
            ViewBag.empId = new SelectList(employersIds);
            var jobForEdit = DataHelper.GetJobs().First(x => x.Id == id);
            return View(jobForEdit);*/

            var jobForEdit = _context.Jobs.Find(id);
            return View(jobForEdit);

        }

        // POST: /Jobs/Edit

        [HttpPost]
        public IActionResult Edit(JobsModel jm)
        {
           
                ViewBag.JobTypes = new SelectList(DataHelper.jobTypes);
                //var employersIds = DataHelper.GetEmployers().Select(js => js.Id).ToList(); // Retrieve all job seeker IDs
                //ViewBag.empId = new SelectList(employersIds);
                jm.PostedDate = DateTime.Now;
              
               // DataHelper.GetJobs()[jm.Id - 1] = jm;

             /*   var jobtoedit = DataHelper.GetJobs().First(a => a.Id == jm.Id);
                jobtoedit.PostedDate = DateTime.Now;
                jobtoedit.JobTitle = jm.JobTitle;
                jobtoedit.Description = jm.Description;
                jobtoedit.Location = jm.Location;
                jobtoedit.SalaryInfo = jm.SalaryInfo;
                jobtoedit.TypeofJob = jm.TypeofJob;
                jobtoedit.Website = jm.Website;*/
                //return RedirectToAction("List");

                _context.Jobs.Update(jm);
                _context.SaveChanges();

                return RedirectToAction("Details", "Employer", new { id = jm.EmployerId });

        }

        // GET: /Jobs/Delete/
        public IActionResult Delete(int id)
        {
           /* var jobForDelete = DataHelper.GetJobs().FirstOrDefault(x => x.Id == id);

            // Retrieve the jobseeker details who applied for this job
            var jobSeekers = DataHelper.getJokSeekers().Where(js => js.jobs.Any(j => j.Id == id)).ToList();
            ViewBag.JobSeekers = jobSeekers;
            if (jobForDelete == null)
            {
                return RedirectToAction("List");
            }
            ViewBag.EmployerId = jobForDelete.EmployerId;
            return View(jobForDelete);*/

            var jobForDelete = _context.Jobs.Find(id);
            ViewBag.EmployerId = jobForDelete.EmployerId;
            if (jobForDelete == null)
            {
                return RedirectToAction("List");
            }
            return View(jobForDelete);
            
        }

        // POST: /Jobs/Delete/
        [HttpPost]
        public IActionResult Delete(JobsModel jm)
        {
            /* var jobToDelete = DataHelper.GetJobs().FirstOrDefault(a => a.Id == jm.Id);
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
            */
            
            var job = _context.Jobs.Find(jm.Id);
            var empid = job.EmployerId;
            _context.Jobs.Remove(job);
            _context.SaveChanges();
            return RedirectToAction("Details", "Employer", new { id = empid });

        }

        //GET : /Jobs/Details
        public IActionResult DetailsForEmployer(int id)
        {

            /* var job = DataHelper.GetJobs().FirstOrDefault(j => j.Id == id);
             if (job != null)
             {
                 // Retrieve the jobseeker details who applied for this job
                 var jobSeekers = DataHelper.getJokSeekers().Where(js => js.jobs.Any(j => j.Id == id)).ToList();
                 ViewBag.JobSeekers = jobSeekers;
                 return View(job);
             }
             return NotFound();*/

           // var jobModel = _context.Jobs.Include(t => t.appliedJobSeekers).FirstOrDefault(m => m.Id == id);
           // return View(jobModel);

            var jobModel = _context.Jobs.Find(id);
            var jobSeekers = jobModel.appliedJobSeekers;
            ViewBag.JobSeekers = jobSeekers;
            return View(jobModel);

          
        }


        //GET : /Jobs/Details
        public IActionResult Details(int id)
        {
            /* var job = DataHelper.GetJobs().FirstOrDefault(j => j.Id == id);
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
             return NotFound();*/
            // var jobModel = _context.Jobs.Include(t => t.JobsEmployer).FirstOrDefault(m => m.Id == id);
            var jobModel = _context.Jobs.Find(id);
            var employer = _context.Employers.Find(jobModel.EmployerId);
            ViewBag.Employer = employer;
            return View(jobModel);
        }


    }
}
