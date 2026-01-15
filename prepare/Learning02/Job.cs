/// <summary>
/// Keeps track of the company, job title, start year, and end year.<br />
/// Member variables: string _company, string _jobTitle, int _startYear, int _endYear
/// </summary>
public class Job
{
    // Member variables
    public string _company;
    public string _jobTitle;
    public int _startYear;
    public int _endYear;

    // Base constructor
    public Job() {}

    /// <summary>
    /// Constructor taking in inputs to assign to member variables.<br />
    /// </summary>
    /// <param name="c">Company name</param>
    /// <param name="j">Job title</param>
    /// <param name="s">Start year</param>
    /// <param name="e">End year</param>
    public Job(string c, string j, int s, int e)
    {
        _company = c;
        _jobTitle = j;
        _startYear = s;
        _endYear = e;
    }

    // Method to display member vairables in the format "JobTitle (Company) StartYear-EndYear"
    public void Display()
    {
        Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
    }
}