using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectJobPortalSystem.Models;
using Microsoft.AspNetCore.Http;
using ProjectJobPortalSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ProjectJobPortalSystem.Controllers
{
    public class JobSeekerController : Controller
    {
        public readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public JobSeekerController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
       
        [Authorize(Roles = "Admin,JobSeeker")]
        //GET : /JobSeeker/List
        public IActionResult List()
        {
            return View(_context.JobSeekers.ToList());
        }

        [Authorize(Roles = "JobSeeker")]
        //GET : //JobSeeker/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /JobSeeker/Create
        [Authorize(Roles = "JobSeeker")]
        [HttpPost]
        public IActionResult Create(JobSeekerModel js)
        {
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
        [Authorize(Roles = "JobSeeker")]
        public IActionResult Edit(string id)
        {
            //var jobSeekerEdit = DataHelper.getJokSeekers().First(x => x.Id == id);
            var jobSeekerEdit = _context.JobSeekers.Find(id);
            bool isJobSeeker = User.IsInRole("JobSeeker");
            if (isJobSeeker)
            {
                ViewData["role"] = "JobSeeker";
            }
            else
            {
                ViewData["role"] = "Employer";
            }
            return View(jobSeekerEdit);
        }
        //POST : /JobSeeker/Edit
        [HttpPost]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> EditAsync(JobSeekerModel js, String OldPassword, String NewPassword, String ConfirmNewPassword)
        {
            var user = await _userManager.FindByIdAsync(js.Id);
           
            if (user == null)
            {
                return NotFound();
            }
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
                JobSeekerModel existingEntity = _context.JobSeekers.AsNoTracking().FirstOrDefault(j => j.Id == js.Id);
                if (existingEntity != null)
                {
                    js.Resume = existingEntity.Resume;
                    _context.Entry(existingEntity).State = EntityState.Detached;
                }
            }
            if (OldPassword == "")
            {
                TempData["OldPasswordError"] = "Required";
            }
            if (NewPassword != ConfirmNewPassword)
            {
                TempData["ConfirmPasswordError"] = "New Password and Confirm New Password do not match.";
                return View(js);
            }
            // Verify the user's current password
            if (!await _userManager.CheckPasswordAsync(user, OldPassword))
            {
                TempData["OldPasswordError"] = "Incorrect current password.";
                return View(js);
            }
            if (!string.IsNullOrEmpty(NewPassword))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, NewPassword);
                if (!result.Succeeded)
                {
                    TempData["NewPasswordError"] = "Your New Password not valid.Include one capital letter, symbol and number.";
                    return View(js);
                }
            }
            _context.JobSeekers.Update(js);
            _context.SaveChanges();
            return RedirectToAction("Index_JobSeeker","Home");

        }

        //GET : /JobSeeker/Delete
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            // var jobSeekerDelete = DataHelper.getJokSeekers().First(x => x.Id == id);
            var jobSeekerDelete = _context.JobSeekers.Include(js => js.jobs).FirstOrDefault(js => js.Id == id);

            return View(jobSeekerDelete);
        }

        //POST : /JobSeeker/Delete
        [Authorize(Roles = "Admin")]
        [HttpPost]
       public async Task<IActionResult> DeleteAsync(JobSeekerModel qt)
            {
                // Find the user associated with the employer
                var user = await _userManager.FindByIdAsync(qt.Id);

                if (user != null)
                {
                    // Delete the user
                    var result = await _userManager.DeleteAsync(user);
                }

                _context.JobSeekers.Remove(qt);
            _context.SaveChanges();

            return RedirectToAction("List","JobSeeker");
        }

        //GET : /JobSeeker/Details
        [Authorize(Roles = "Admin,JobSeeker")]
        public IActionResult Details(string id)
        {
             var jobSeekerDetails = _context.JobSeekers.Include(js => js.jobs).FirstOrDefault(js => js.Id == id);
            ViewBag.EmployerName = @User.Identity?.Name;
            if (jobSeekerDetails != null)
                {
                    return View(jobSeekerDetails);
                }

                return NotFound();
        }

    }
}
