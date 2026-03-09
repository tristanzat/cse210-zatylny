public class EternalGoal : Goal
{
    private int _timesMarked;
    
    // Normal constructor
    public EternalGoal(string n, string d, int p) : base(n, d, p)
    {
        _timesMarked = 0;
    }

    // Constructor for loaded goal
    public EternalGoal(string n, string d, int p, int m) : base(n, d, p)
    {
        _timesMarked = m;
    }

    // Give points earned so far
    public override int GetPoints()
    {
        return base.GetPoints() * _timesMarked;
    }

    // Add to counter
    public override void RecordEvent()
    {
        _timesMarked ++;
    }

    // Eternal goal is never complete
    public override bool IsComplete() { return false; }

    // String format for printing name
    public override string GetDetails()
    {
        return $"[{_timesMarked}] {_name}";
    }

    // String format for printing full details
    public override string GetFullDetails()
    {
        return $"[{_timesMarked}] {_name} ({_desc}) | {base.GetPoints()} pts | {base.GetPoints() * _timesMarked} pts earned";
    }

    // String format for file saving
    public override string ToString()
    {
        return $"EternalGoal:{_name}|{_desc}|{base.GetPoints()}|{_timesMarked}";
    }
}