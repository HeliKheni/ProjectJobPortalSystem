using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace ProjectJobPortalSystem.Models
{
    public class EmployerModel
    {
        public EmployerModel() { }

        public EmployerModel(int id, string firstName, string lastName, BigInteger contactNo, string email, string companyName, string companyProfile, string position)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            ContactNo = contactNo;
            Email = email;
            CompanyName = companyName;
            CompanyProfile = companyProfile;
            Position = position;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "First Name should only contain alphabetic characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Last Name should only contain alphabetic characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Contact No is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact No must be a 10-digit number")]
        [Display(Name = "Contact Number")]
        public BigInteger ContactNo { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Company Profile is required")]
        public string CompanyProfile { get; set; }

        [Required(ErrorMessage = "Position is required")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Position should only contain alphabetic characters")]
        public string Position { get; set; }

        public List<JobsModel> Jobslist { get; set; } = new List<JobsModel>();
    }
}
