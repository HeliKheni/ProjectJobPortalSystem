using System.Numerics;

namespace ProjectJobPortalSystem.Models
{
    public class EmployerModel
    {
        public EmployerModel() { }

        public EmployerModel(int id,string firstName,string lastName, BigInteger contactno, string email, string companyname, string companyprofile, string position)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            ContactNo = contactno;
            Email = email;
            CompanyName = companyname;
            CompanyProfile = companyprofile;
            Position = position;
        }
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public BigInteger ContactNo { get; set; }

        public string Email { get; set; }
        public string CompanyName{ get; set; }
        public string CompanyProfile { get; set; }
        public string Position { get; set; }

        public List<JobsModel> Jobslist = new List<JobsModel>();
    }
}
