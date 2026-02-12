public class Assignment
{
    protected string _studentName;
    protected string _topic;

    public Assignment(string n, string t)
    {
        _studentName = n;
        _topic = t;
    }

    public string GetSummary()
    {
        return $"{_studentName} - {_topic}";
    }
}