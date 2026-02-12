public class WritingAssignment : Assignment
{
    private string _title;

    public WritingAssignment(string n, string t, string w) : base(n, t)
    {
        _title = w;
    }

    public string GetWritingInformation()
    {
        return $"{_title} by {base._studentName}";
    }
}