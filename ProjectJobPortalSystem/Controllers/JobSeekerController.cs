using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectJobPortalSystem.Models;
using Microsoft.AspNetCore.Http;
using ProjectJobPortalSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace ProjectJobPortalSystem.Controllers
{
    public class JobSeekerController : Controller
    {
        public readonly ApplicationDbContext _context;

        public JobSeekerController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        //GET : /JobSeeker/List
        public IActionResult List()
        {
            //return View(DataHelper.getJokSeekers());
            return View(_context.JobSeekers.ToList());
        }

        //GET : //JobSeeker/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /JobSeeker/Create
        [HttpPost]
        public IActionResult Create(JobSeekerModel js)
        {
            /*var jobSeekerList = DataHelper.getJokSeekers();
            if (jobSeekerList.Count == 0)
            {
                js.Id = 1;
            }
            else
            {
                js.Id = jobSeekerList.Max(x => x.Id) + 1;
            }*/
         
            if (js.ResumeFile != null && js.ResumeFile.Length > 0)
             {
                    // Save the resume file to a desired location
                    string resumeFileName =js.ResumeFile.FileName;
                    string resumeFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "resumes", resumeFileName);
                    using (var fileStream = new FileStream(resumeFilePath, FileMode.Create))
                    {
                        js.ResumeFile.CopyTo(fileStream);
                    }
                    js.Resume = resumeFileName;
            }
            //jobSeekerList.Add(js);
            _context.JobSeekers.Add(js);
            _context.SaveChanges();
            return RedirectToAction("List");
     
        }

        //GET : /JobSeeker/Edit
        public IActionResult Edit(int id)
        {
            //var jobSeekerEdit = DataHelper.getJokSeekers().First(x => x.Id == id);
            var jobSeekerEdit = _context.JobSeekers.Find(id);
            return View(jobSeekerEdit);
        }
        //POST : /JobSeeker/Edit
        [HttpPost]
        public IActionResult Edit(JobSeekerModel js)
        {
           if (js.ResumeFile != null && js.ResumeFile.Length > 0)
                {
                    // Save the new resume file to a desired location
                    string resumeFileName = js.ResumeFile.FileName;
                    string resumeFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "resumes", resumeFileName);
                    using (var fileStream = new FileStream(resumeFilePath, FileMode.Create))
                    {
                        js.ResumeFile.CopyTo(fileStream);
                    }
                    js.Resume = resumeFileName;
                }
            else
            {
                //js.Resume = DataHelper.getJokSeekers()[js.Id - 1].Resume;
                //js.Resume = _context.JobSeekers.Find(js.Id).Resume;
                JobSeekerModel existingEntity = _context.JobSeekers.AsNoTracking().FirstOrDefault(j => j.Id == js.Id);
                if (existingEntity != null)
                {
                    js.Resume = existingEntity.Resume;
                    _context.Entry(existingEntity).State = EntityState.Detached;
                }
            }
          //  DataHelper.getJokSeekers()[js.Id - 1] = js;
          //  return RedirectToAction("List");

            _context.JobSeekers.Update(js);
            _context.SaveChanges();
            return RedirectToAction("List");

        }

        //GET : /JobSeeker/Delete
        public IActionResult Delete(int id)
        {
            // var jobSeekerDelete = DataHelper.getJokSeekers().First(x => x.Id == id);
            var jobSeekerDelete = _context.JobSeekers.Find(id);
            return View(jobSeekerDelete);
        }

        //POST : /JobSeeker/Delete
        [HttpPost]
        public IActionResult Delete(JobSeekerModel qt)
        {
           // var jobSeekerDelete = DataHelper.getJokSeekers().First(a => a.Id == qt.Id);
           // DataHelper.getJokSeekers().Remove(jobSeekerDelete);
          
            _context.JobSeekers.Remove(qt);
            _context.SaveChanges();

            return RedirectToAction("List");
        }

        //GET : /JobSeeker/Details
        public IActionResult Details(int id)
        {
            //var jobSeekerDetails = DataHelper.getJokSeekers().FirstOrDefault(x => x.Id == id);
            /* var jobSeekerDetails = _context.JobSeekers.Find(id);
             return View(jobSeekerDetails);*/

           // var jobSeekerDetails = DataHelper.getJokSeekers().FirstOrDefault(x => x.Id == id);
                var jobSeekerDetails = _context.JobSeekers.Include(js => js.jobs).FirstOrDefault(js => js.Id == id);

                if (jobSeekerDetails != null)
                {
                    return View(jobSeekerDetails);
                }

                return NotFound();
           
        }

    }
}
