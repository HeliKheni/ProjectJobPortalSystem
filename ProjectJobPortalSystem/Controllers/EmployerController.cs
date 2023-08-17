
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectJobPortalSystem.Data;
using ProjectJobPortalSystem.Models;

using System.Data;

namespace ProjectJobPortalSystem.Controllers
{
    public class EmployerController : Controller
    {
        
        public readonly ApplicationDbContext _context;

        private UserManager<IdentityUser> _userManager;

        
        public EmployerController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        

        [Authorize(Roles = "Admin,Employer")]
        //GET : /Employer/List
        public IActionResult List()
        {
            //return View(DataHelper.GetEmployers());
            return View(_context.Employers.ToList());
        }

        //GET : //Employer/Create

        [Authorize(Roles = "Employer")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Employer/Create

        [Authorize(Roles = "Employer")]
        [HttpPost]
        public IActionResult Create(EmployerModel em)
        {
           
            if (ModelState.IsValid)
            {
               // jobSeekerList.Add(em);
               _context.Employers.Add(em);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return View(em);
        }

        [Authorize(Roles = "Employer")]
        //GET : /Employer/Edit
        public IActionResult Edit(string id)
        {
            // var employeerEdit = DataHelper.GetEmployers().First(x => x.Id == id);
            var employeerEdit = _context.Employers.Find(id);
            return View(employeerEdit);     
        }
        //POST : /Employer/Edit
        [Authorize(Roles = "Employer")]
        [HttpPost]
        public async Task<IActionResult> EditAsync(EmployerModel em, String OldPassword, String NewPassword, String ConfirmNewPassword)
        {
            var user = await _userManager.FindByIdAsync(em.Id);

            if (user == null)
            {
                return NotFound();
            }
            if(OldPassword == "")
            {
                TempData["OldPasswordError"] = "Required";
            }
            if (NewPassword != ConfirmNewPassword)
            {
                TempData["ConfirmPasswordError"] = "New Password and Confirm New Password do not match.";
                return View(em);
            }
            // Verify the user's current password
            if (!await _userManager.CheckPasswordAsync(user, OldPassword))
            {
                TempData["OldPasswordError"] = "Incorrect current password.";
                return View(em);
            }
            if (!string.IsNullOrEmpty(NewPassword))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, NewPassword);
                if (!result.Succeeded)
                {
                    TempData["NewPasswordError"] = "Your New Password not valid.Include one capital letter, symbol and number.";
                    return View(em);
                }
            }
            //DataHelper.GetEmployers()[em.Id - 1] = em;
                _context.Employers.Update(em);
                _context.SaveChanges();

                return RedirectToAction("Index_Employer","Home");
          
        }


        //GET: /Employer/Details
        [Authorize(Roles = "Admin,Employer")]
        public IActionResult Details(string id)
        {
            var employerDetails = _context.Employers.Include(t => t.Jobslist).FirstOrDefault(m => m.Id == id);
            ViewBag.EmployerName = @User.Identity?.Name;
            return View(employerDetails);
        }


        //GET: /Employer/Delet
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            
            var empDelete = _context.Employers.Find(id);
            if (empDelete == null)
            {
                return RedirectToAction("List");
            }
            empDelete.Jobslist = _context.Jobs.Where(j => j.EmployerId == id).ToList();

            return View(empDelete);
        }


        // POST: /Employer/Delete
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(EmployerModel em)
        {
            // Find the user associated with the employer
            var user = await _userManager.FindByIdAsync(em.Id);

            if (user != null)
            {
                // Delete the user
                var result = await _userManager.DeleteAsync(user);
            }
            _context.Employers.Remove(em);
            _context.SaveChanges();
            return RedirectToAction("List");

        }


    }
}
