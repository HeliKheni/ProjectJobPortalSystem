namespace ProjectJobPortalSystem.Models
{
    public static class DataHelper
    {
        static List<JobSeekerModel> jobSeeker;
     //   public static List<string> Category = new List<string> { "Software Testing", "Algorithms ", " Databases ", " Artificial Intelligence and Machine Learning " };

        public static List<JobSeekerModel> getJokSeekers()
        {
            if (jobSeeker == null)
            {
                jobSeeker = new List<JobSeekerModel>();
                jobSeeker.Add(new JobSeekerModel(1, "Hardi", "Majmundar","hardi@gmail.com",647-9899894,"Coding","Resume"));
                jobSeeker.Add(new JobSeekerModel(2, "Heli", "Kheni", "heliKheni@gmail.com", 647 - 909-9213, "Coding", "Resume"));

                jobSeeker.ElementAt(0).jobs.Add(new JobsModel(1,"first job","zhdfui","bgdsy","hdfiu","dbsj","dhiu",1,"sadho",DateTime.Now));
                jobSeeker.ElementAt(1).jobs.Add(new JobsModel(2, "second job", "zhdfui", "bgdsy", "hdfiu", "dbsj", "dhiu", 1, "sadho", DateTime.Now));
                jobSeeker.ElementAt(0).jobs.Add(new JobsModel(3, "third job", "zhdfui", "bgdsy", "hdfiu", "dbsj", "dhiu", 1, "sadho", DateTime.Now));
                jobSeeker.ElementAt(1).jobs.Add(new JobsModel(4, "fourth job", "zhdfui", "bgdsy", "hdfiu", "dbsj", "dhiu", 1, "sadho", DateTime.Now));
            }
            return jobSeeker;
        }
        public static List<JobsModel> GetJobs()
        {
            List<JobsModel> jobs = new List<JobsModel>();

            foreach (var jobSeeker in getJokSeekers())
            {
                jobs.AddRange(jobSeeker.jobs);
            }

            return jobs;
        }
    }
}
