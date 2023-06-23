using System.Numerics;

namespace ProjectJobPortalSystem.Models
{
    public class JobSeekerModel
    {
        public JobSeekerModel() { }

        public JobSeekerModel(int id, string name, string email, BigInteger contactno, string skills, string resume )
        {
            Id = id;
            Name = name;
            Email = email;
            ContactNo = contactno;
            Skills = skills;
            Resume = resume;
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public BigInteger ContactNo { get; set; }

        public string Skills { get; set; }
        public string Resume { get; set; }

        public List<JobsModel> jobs = new List<JobsModel> (){ };


    }
}
