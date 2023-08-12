using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectJobPortalSystem.Models
{
    public class JobsModel
    {
        public JobsModel() { }
        public JobsModel(int id, string jobTitle, string typeofJob, string salaryInfo, string description, string website, string employerId, string location, DateTime? postedDate = null)
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
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Job Title is required")]
        [Display(Name = "Title Of Job")]
        [StringLength(80)]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Job Description is required")]
        [Display(Name = "Description Of Job")]
        [StringLength(200)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Website is required")]
        [Url(ErrorMessage = "Invalid Website URL")]
        [StringLength(50)]
        public string Website { get; set; }

        [Required(ErrorMessage = "Type of Job is required")]
        [Display(Name = "Type Of Job")]
        [StringLength(30)]
        public string TypeofJob { get; set; }

        [Required(ErrorMessage = "Salary Info is required")]
        [StringLength(30)]
        public string SalaryInfo { get; set; }

        public DateTime PostedDate { get; set; }

        public string EmployerId { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(50)]
        public string Location { get; set; }

        public List<JobSeekerModel> appliedJobSeekers { get; } = new();

        public EmployerModel JobsEmployer { get; set; }
    }
}
