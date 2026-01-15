/// <summary>
/// Resume class: keeps track of the person's name and a list of their jobs.
/// </summary>
public class Resume
{
    // Member variables
    public string _name;
    public List<Job> _jobs;

    // Base constructor
    public Resume() {}

    /// <summary>
    /// Constructor to assign member variables
    /// </summary>
    /// <param name="n">Person's name</param>
    /// <param name="j">List of jobs</param>
    public Resume(string n, List<Job> j)
    {
        _name = n;
        _jobs = j;
    }

    // Method to display name and each job
    public void Display()
    {
        Console.WriteLine($"Name: {_name}\nJobs:");
        foreach (var job in _jobs)
        {
            job.Display();
        }
    }
}
