## Job Portal System
  The Job Portal System is a web-based application developed using the ASP.NET MVC framework . It is designed to serve as a platform for job seekers and employers to interact, 
  manage job listings. The system is built on top of the Identity Framework for user authentication and Entity Framework for database operations.It developed using Entity framework and identity framwork.

## Features
### User Registration and Login
  - Customized registration process for job seekers and employers and configure with Idenitity framework.
  - Seamless login functionality with user authentication and security.

### Job Seeker Module
 - Browse and view job details, including employer information.
 - Apply for specific job listings.
 - Restrict job applications to one per job listing.
 - Manage and view a list of applied jobs.
 - Update personal profile details and upload resume.
   
### Employer Module
 - Post new job listings with detailed information.
 - Edit, delete, and view job postings.
 - Access a list of applicants for each job listing.
 - Download resumes uploaded by job seekers.
 - Manage employer profile details.
   
### Database
 - Utilizes MySQL as the backend database for storing user data, job listings, and related information.
   
## Getting Started
  To get the project up and running locally on your development machine, follow these steps:
  
  Clone the repository to your local machine.
  
  git clone https://github.com/HeliKheni/ProjectJobPortalSystem.git
  
  Open the project in Visual Studio.
  Configure the MySQL database connection in the appsettings.json file.
  
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=JobPortalDB;User=root;Password=yourpassword;"
  }
  
  Build and run the project in Visual Studio.


## Technologies Used
 - ASP.NET MVC
 - Entity Framework
 - Identity Framework
 - MySQL Database
 - C#
 - HTML/CSS/BootStrap
 - JavaScript

## Project Contributors
- Heli Kheni 
- Hardi Majmundar

## Acknowledgments
- This project was developed as part of academic coursework and is intended for educational purposes.
