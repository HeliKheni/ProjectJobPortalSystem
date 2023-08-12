using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace ProjectJobPortalSystem.Models
{
    public class RegisterViewModel
    {

        public string Role { get; set; }

        // Employer fields

        [Required(ErrorMessage = "Company Name is required")]
        [StringLength(50)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Company Profile is required")]
        [StringLength(100)]
        public string CompanyProfile { get; set; }

        [Required(ErrorMessage = "Position is required")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Position should only contain alphabetic characters")]
        [StringLength(50)]
        public string Position { get; set; }

        // Job Seeker fields


        [Required(ErrorMessage = "Skills is required")]
        [Display(Name = "Skill set")]
        [StringLength(80)]
        public string Skills { get; set; }
        [NotMapped]
        [BindProperty(Name = "Resume")]
        public IFormFile ResumeFile { get; set; }

        public string Resume { get; set; }


        // Common fields

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "First Name should only contain alphabetic characters")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Last Name should only contain alphabetic characters")]
        [StringLength(50)]
        public string LastName { get; set; }

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

        //for edit
        [NotMapped]
        [Required(ErrorMessage = "Old Password is required")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
       

    }
}
