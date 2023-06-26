using System.Numerics;


namespace ProjectJobPortalSystem.Models
{
    public class JobSeekerModel
    {
        public JobSeekerModel() { }

        public JobSeekerModel(int id, string firstName,string lastName, string email, BigInteger contactno, string skills, string resume )
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ContactNo = contactno;
            Skills = skills;
            Resume = resume;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public BigInteger ContactNo { get; set; }

        public string Skills { get; set; }
        public string Resume { get; set; }

        public List<JobsModel> jobs = new List<JobsModel> (){ };

    }
}
