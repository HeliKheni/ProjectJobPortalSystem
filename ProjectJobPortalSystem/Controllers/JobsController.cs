﻿using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectJobPortalSystem.Data;
using ProjectJobPortalSystem.Models;
using System.Security.Policy;
using System.Threading.Channels;

namespace ProjectJobPortalSystem.Controllers
{
    public class JobsController : Controller
    {
        public readonly ApplicationDbContext _context;

        private UserManager<IdentityUser> _userManager;

        public JobsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        //GET : /Jobs/List
        public IActionResult List()
        {
            String userEmail = @User.Identity?.Name;
            String id = "";
           
            if (!string.IsNullOrEmpty(userEmail))
            {
                var user = _userManager.FindByEmailAsync(userEmail).Result;
                if (user != null)
                {
                    id = user.Id;
                }
            }
            // Get a list of job IDs applied by the current job seeker
            var appliedJobIds = _context.JobSeekers
                .Where(js => js.Id == id)
                .SelectMany(js => js.jobs.Select(j => j.Id))
                .ToList();

            ViewBag.UserId = id;
            ViewBag.AppliedJobIds = appliedJobIds;

            // Retrieve a list of jobs with their associated employers - for displaying employer email not id
            var jobsWithEmployers = _context.Jobs
                .Include(j => j.JobsEmployer)
                .ToList();

            ViewBag.JobsWithEmployers = jobsWithEmployers;

            return View(_context.Jobs.ToList());
        }

        //GET : //Jobs/Create
        public IActionResult Create(string employerId)
        {
            ViewBag.JobTypes = new SelectList(DataHelper.jobTypes);
          
            ViewBag.EmployerId = employerId;
            ViewBag.EmployerName = @User.Identity?.Name;
            return View();
        }


        // POST: /Jobs/Create
        [HttpPost]
        public IActionResult Create(JobsModel jm)
        {
           
            jm.PostedDate = DateTime.Now;
                _context.Jobs.Add(jm);
                _context.SaveChanges();

                return RedirectToAction("Index_Employer", "Home");
        }

        //GET : //Jobs/Apply
        public IActionResult Apply(int id)
        {
           
            var jobForApply = _context.Jobs.Find(id);
            return View(jobForApply);
        }

        // POST: /Jobs/Apply
        [HttpPost]
        public IActionResult Apply(JobsModel jm)
        {
            string id = "" ;

            string userEmail = User.Identity.Name; // Get the logged-in user's email
            //Getting logged-in user id from userEmail
            if (!string.IsNullOrEmpty(userEmail))
            {
                var user = _userManager.FindByEmailAsync(userEmail).Result;
                if (user != null)
                {
                    id = user.Id;
                }
            }
            var job = _context.Jobs.Include(j => j.appliedJobSeekers).FirstOrDefault(j => j.Id == jm.Id);
             var jobSeeker = _context.JobSeekers.FirstOrDefault(js => js.Id == id);

             if (job != null && jobSeeker != null)
             {
                    // Check if the job seeker has already applied for the job
                    if (job.appliedJobSeekers.Any(js => js.Id == id))
                    {
                        ModelState.AddModelError(string.Empty, "You have already applied for this job.");
                        ViewBag.jobseekerlist = new SelectList(_context.JobSeekers.Select(js => js.Id).ToList());
                        return View(job);
                    }

                    job.appliedJobSeekers.Add(jobSeeker);
               
                    _context.SaveChanges();

                    return RedirectToAction("List", "Jobs");
                }

                return View();
          

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
                    /* var jobs = _context.Jobs.Find(jobId);
                     var jobSeeker = _context.JobSeekers.Find(id);

                     jobs.appliedJobSeekers.Add(jobSeeker);
                      jobSeeker.jobs.Add(jobs);
                      _context.Jobs.Update(jobs);
                      _context.JobSeekers.Update(jobSeeker);
                      _context.SaveChanges();

                      return RedirectToAction("List", "Jobs");*/
        }

        //GET : /Jobs/Edit
        public IActionResult Edit(int id)
        {
            ViewBag.JobTypes = new SelectList(DataHelper.jobTypes);


            var jobForEdit = _context.Jobs.Find(id);
            ViewBag.EmployerName = @User.Identity?.Name;
            return View(jobForEdit);

        }

        // POST: /Jobs/Edit

        [HttpPost]
        public IActionResult Edit(JobsModel jm)
        {
             ViewBag.JobTypes = new SelectList(DataHelper.jobTypes);
             jm.PostedDate = DateTime.Now;
             _context.Jobs.Update(jm);
             _context.SaveChanges();

             return RedirectToAction("Details", "Employer", new { id = jm.EmployerId });

        }

        // GET: /Jobs/Delete/
        public IActionResult Delete(int id)
        {
            var jobForDelete = _context.Jobs.Find(id);
            ViewBag.EmployerId = jobForDelete.EmployerId;
            ViewBag.EmployerName = @User.Identity?.Name;
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
            var job = _context.Jobs.Find(jm.Id);
            var empid = job.EmployerId;
            _context.Jobs.Remove(job);
            _context.SaveChanges();
            return RedirectToAction("Details", "Employer", new { id = empid });

        }

        //GET : /Jobs/Details
        public IActionResult DetailsForEmployer(int id)
        {

            var job = _context.Jobs.Include(j => j.appliedJobSeekers).FirstOrDefault(j => j.Id == id);

            if (job != null)
            {
                // Retrieve the jobseeker details who applied for this job
                var jobSeekers = _context.JobSeekers.Where(js => js.jobs.Any(j => j.Id == id)).ToList();

                ViewBag.JobSeekers = jobSeekers;
                ViewBag.Employer = _context.Employers.FirstOrDefault(e => e.Id == job.EmployerId);

                return View(job);
            }

            return NotFound();


        }


        //GET : /Jobs/Details
        public IActionResult Details(int id)
        {
           // var jobModel = _context.Jobs.Include(t => t.JobsEmployer).FirstOrDefault(m => m.Id == id);
            var jobModel = _context.Jobs.Find(id);
            var employer = _context.Employers.Find(jobModel.EmployerId);
            ViewBag.Employer = employer;
            return View(jobModel);
        }


    }
}
