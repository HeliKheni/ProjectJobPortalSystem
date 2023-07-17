using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectJobPortalSystem.Data;
using ProjectJobPortalSystem.Models;

namespace ProjectJobPortalSystem.Controllers
{
    public class EmployerController : Controller
    {
        public readonly ApplicationDbContext _context;

        public EmployerController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        //GET : /Employer/List
        public IActionResult List()
        {
            //return View(DataHelper.GetEmployers());
            return View(_context.Employers.ToList());
        }

        //GET : //Employer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Employer/Create
        [HttpPost]
        public IActionResult Create(EmployerModel em)
        {
           /* var jobSeekerList = DataHelper.GetEmployers();
            if (jobSeekerList.Count == 0)
            {
                em.Id = 1;
            }
            else
            {
                em.Id = jobSeekerList.Max(x => x.Id) + 1;
            }*/
            if (ModelState.IsValid)
            {
               // jobSeekerList.Add(em);
               _context.Employers.Add(em);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return View(em);
        }


        //GET : /Employer/Edit
        public IActionResult Edit(int id)
        {
            // var employeerEdit = DataHelper.GetEmployers().First(x => x.Id == id);
            var employeerEdit = _context.Employers.Find(id);
            return View(employeerEdit);
        }
        //POST : /Employer/Edit
        [HttpPost]
        public IActionResult Edit(EmployerModel em)
        {
            if (ModelState.IsValid)
            {
                //DataHelper.GetEmployers()[em.Id - 1] = em;
                _context.Employers.Update(em);
                _context.SaveChanges();

                return RedirectToAction("List");
            }
            else
            {
                 return View(em);
            }
        }


        //GET: /Employer/Details
        public IActionResult Details(int id)
        {
            /* var employerDetails = DataHelper.GetEmployers().FirstOrDefault(x => x.Id == id);
             if (employerDetails != null)
             {
                 employerDetails.Jobslist = DataHelper.GetJobs().Where(j => j.EmployerId == id).ToList();
             }
             return View(employerDetails);*/

            var employerDetails = _context.Employers.Include(t => t.Jobslist).FirstOrDefault(m => m.Id == id);
            return View(employerDetails);
        }


        //GET: /Employer/Delete
        public IActionResult Delete(int id)
        {
            /*var empDelete = DataHelper.GetEmployers().FirstOrDefault(x => x.Id == id);
            if (empDelete == null)
            {
                return RedirectToAction("List");
            }
            empDelete.Jobslist = DataHelper.GetJobs().Where(j => j.EmployerId == id).ToList();
            return View(empDelete);*/

            var empDelete = _context.Employers.Find(id);
            if (empDelete == null)
            {
                return RedirectToAction("List");
            }
            empDelete.Jobslist = _context.Jobs.Where(j => j.EmployerId == id).ToList();

            return View(empDelete);
        }


        // POST: /Employer/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(EmployerModel em)
        {
            /* var employertoremove = DataHelper.GetEmployers().FirstOrDefault(a => a.Id == em.Id);
             if (employertoremove != null)
             {
                 DataHelper.GetEmployers().Remove(employertoremove);

             }
             return RedirectToAction("List");*/
            _context.Employers.Remove(em);
            _context.SaveChanges();
            return RedirectToAction("List");

        }


    }
}
