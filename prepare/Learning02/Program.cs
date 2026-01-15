using System;

class Program
{
    static void Main(string[] args)
    {
        // Make job "Software Engineer (Microsoft) 2019-2022" by directly assigning member variables
        Job job1 = new Job();
        job1._jobTitle = "Software Engineer";
        job1._company = "Microsoft";
        job1._startYear = 2019;
        job1._endYear = 2022;

        // Make job "Manager (Apple) 2022-2023" using the other constructor
        Job job2 = new Job("Manager", "Apple", 2022, 2023);

        // Make resume with the name Allison Rose and the 2 jobs already made
        // First make a Job List for putting in the Resume
        List<Job> jobs = new List<Job> {job1, job2};
        Resume resume = new Resume("Allison Rose", jobs);

        // Display the resume
        resume.Display();
    }
}
