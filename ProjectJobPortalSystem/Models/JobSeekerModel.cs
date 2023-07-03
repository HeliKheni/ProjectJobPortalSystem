using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace ProjectJobPortalSystem.Models
{
    public class JobSeekerModel
    {
        public JobSeekerModel() { }

        public JobSeekerModel(int id, string firstName, string lastName, string email, BigInteger contactNo, string skills, string resume)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ContactNo = contactNo;
            Skills = skills;
            Resume = resume;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "First Name should only contain alphabetic characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Last Name should only contain alphabetic characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact No is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact No must be a 10-digit number")]
        [Display(Name = "Contact Number")]
        public BigInteger ContactNo { get; set; }

        [Required(ErrorMessage = "Skills No is required")]
        [Display(Name = "Skill set")]
        public string Skills { get; set; }
        

        [BindProperty(Name = "Resume")]
        public IFormFile ResumeFile { get; set; }

        public string Resume { get; set; }
        public List<JobsModel> jobs { get; set; } = new List<JobsModel>();
    }
}
