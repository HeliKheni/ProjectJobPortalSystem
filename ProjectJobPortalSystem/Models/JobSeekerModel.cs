using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace ProjectJobPortalSystem.Models
{
    public class JobSeekerModel
    {
        public JobSeekerModel() { }

        public JobSeekerModel(String id, string firstName, string lastName, string email, BigInteger contactNo, string skills, string resume)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ContactNo = contactNo;
            Skills = skills;
            Resume = resume;
        }
        [Key]
        public String Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "First Name should only contain alphabetic characters")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Last Name should only contain alphabetic characters")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact No is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact No must be a 10-digit number")]
        [Display(Name = "Contact Number")]
        [NotMapped]
        public BigInteger ContactNo { get; set; }

        [Required(ErrorMessage = "Contact No is required")]
        [Column("ContactNo", TypeName = "nvarchar(10)")] 
        public string ContactNoString
        {
            get { return ContactNo.ToString(); }
            set { ContactNo = BigInteger.Parse(value); }
        }


        [Required(ErrorMessage = "Skills No is required")]
        [Display(Name = "Skill set")]
        [StringLength(80)]
        public string Skills { get; set; }
        [NotMapped]
        [BindProperty(Name = "Resume")]
        public IFormFile ResumeFile { get; set; }

        public string Resume { get; set; }
        public List<JobsModel> jobs { get; } = new ();
    }
}
