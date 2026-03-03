public abstract class Goal
{
    // member variables
    protected readonly string _name;
    protected readonly string _desc;
    protected readonly int _points;

    public Goal(string n, string d, int p)
    {
        _name = n;
        _desc = d;
        _points = p;
    }

    public virtual int GetPoints()
    {
        return _points;
    }

    public abstract void RecordEvent();

    public abstract bool IsComplete();

    public abstract string GetDetails();

    public abstract string GetFullDetails();

    public override abstract string ToString();
}