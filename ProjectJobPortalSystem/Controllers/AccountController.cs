using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectJobPortalSystem.Data;
using ProjectJobPortalSystem.Models;
using System.Threading.Tasks;

namespace ProjectJobPortalSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ApplicationDbContext _context;

        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            // Create the admin user if not exists
            InitializeAdminUser().Wait();
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
           
                // Save data to AspNetUsers table (Identity integration)
                var user = new IdentityUser
                {
                    Email = model.Email,
                    UserName =  model.Email
                };

            
            var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Check if the role exists, and create it if not
                    var roleExists = await _roleManager.RoleExistsAsync(model.Role);
                    if (!roleExists)
                    {
                        var newRole = new IdentityRole(model.Role);
                        await _roleManager.CreateAsync(newRole);
                    }

                // Assign the role to the user
                if (model.Role == "Employer")
                    {
                        await _userManager.AddToRoleAsync(user, model.Role);
                     // Save data to Employer table
                        var employer = new EmployerModel
                        {
                            Id = user.Id,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            ContactNo = model.ContactNo,
                            CompanyName = model.CompanyName,
                            CompanyProfile = model.CompanyProfile,
                            Position = model.Position,
                            Email = model.Email
                        };

                        _context.Employers.Add(employer);
                         await _context.SaveChangesAsync();

                    // Redirect to a success page
                   // return RedirectToAction("Index_Employer", "Home");
                }
                    else if (model.Role == "JobSeeker")
                    {
                        await _userManager.AddToRoleAsync(user, "JobSeeker");
                    // Save data to JobSeeker table

                    if (model.ResumeFile != null && model.ResumeFile.Length > 0)
                    {
                        // Save the resume file to a desired location
                        string resumeFileName = model.ResumeFile.FileName;
                        string resumeFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "resumes", resumeFileName);
                        using (var fileStream = new FileStream(resumeFilePath, FileMode.Create))
                        {
                            model.ResumeFile.CopyTo(fileStream);
                        }
                        model.Resume = resumeFileName;
                    }

                    var jobSeeker = new JobSeekerModel
                        {

                            Id = user.Id,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            ContactNo = model.ContactNo,
                            Skills = model.Skills,
                            Email = model.Email,
                            Resume = model.Resume
                        };

                        _context.JobSeekers.Add(jobSeeker);
                    await _context.SaveChangesAsync();

                    // Redirect to a success page
                    //return RedirectToAction("Index_Jobseeker", "Home");
                }

                await _context.SaveChangesAsync();

                // Redirect to a success page
                return RedirectToAction("Login", "Account");


            }
            else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
           

            return View(model);
        }

        public IActionResult RegistrationSuccess()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Creating admin user
        private async Task InitializeAdminUser()
        {
            var adminUser = await _userManager.FindByEmailAsync("admin@gmail.com");
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(adminUser, "Admin123@");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (model.Email == "admin@gmail.com" && model.Password == "Admin123@")
                {
                    return RedirectToAction("Index_Admin", "Home");
                }
                else if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        var roles = await _userManager.GetRolesAsync(user);

                        if (roles.Contains("Employer"))
                        {
                            return RedirectToAction("Index_Employer", "Home");
                        }
                        else if (roles.Contains("JobSeeker"))
                        {
                            return RedirectToAction("Index_Jobseeker", "Home");
                        }
                        
                    }

                    // Default redirect if role is not recognized
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }


    }
}
