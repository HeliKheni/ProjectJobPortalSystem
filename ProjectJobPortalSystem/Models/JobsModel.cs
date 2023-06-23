namespace ProjectJobPortalSystem.Models
{
    public class JobsModel
    {
        public JobsModel() { }

        public JobsModel(string jobtitle ,string typeofjob, string salaryinfo, string description, string website, string country,int employerid, string location, DateTime posteddate ) 
        {
            JobTitle = jobtitle;
            SalaryInfo = salaryinfo;
            Description = description;
            Website = website;
            EmployerId = employerid;
            TypeofJob = typeofjob;
            Country = country;
            PostedDate = DateTime.Now;
            Location = location;
        }
        int Id { get; set; }
        public string JobTitle{ get; set; }

        public string SalaryInfo { get;set; }
        public string Description { get; set; }

        public string Website { get; set; }

        public string TypeofJob { get; set; }

        public DateTime PostedDate { get; set; }
        public int EmployerId { get; set; }
        
        public string Location { get; set; }
        public string Country { get; set; }

        public List<JobSeekerModel> appliedJobSeekers = new List<JobSeekerModel>();
    }
}
