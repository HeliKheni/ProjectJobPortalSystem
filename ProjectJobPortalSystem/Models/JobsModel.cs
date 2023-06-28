using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectJobPortalSystem.Models
{
    public class JobsModel
    {
        public JobsModel() { }

        public JobsModel(int id, string jobTitle, string typeofJob, string salaryInfo, string description, string website, int employerId, string location, DateTime? postedDate = null)
        {
            Id = id;
            JobTitle = jobTitle;
            SalaryInfo = salaryInfo;
            Description = description;
            Website = website;
            EmployerId = employerId;
            TypeofJob = typeofJob;
            PostedDate = postedDate ?? DateTime.Now;
            Location = location;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Job Title is required")]
        [Display(Name = "Title Of Job")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Job Description is required")]
        [Display(Name = "Description Of Job")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Website is required")]
        [Url(ErrorMessage = "Invalid Website URL")]
        public string Website { get; set; }

        [Required(ErrorMessage = "Type of Job is required")]
        [Display(Name = "Type Of Job")]
        public string TypeofJob { get; set; }

        [Required(ErrorMessage = "Salary Info is required")]
        public string SalaryInfo { get; set; }

        public DateTime PostedDate { get; set; }

        public int EmployerId { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Location should only contain alphabetic characters")]
        public string Location { get; set; }

        public List<JobSeekerModel> appliedJobSeekers = new List<JobSeekerModel>();
    }
}
