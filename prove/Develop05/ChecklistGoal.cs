public class ChecklistGoal : Goal
{
    // Member vairables
    private int _numCompleted;
    private readonly int _target;


    // Normal constructor
    public ChecklistGoal(string n, string d, int p, int t) : base(n, d, p)
    {
        _target = t;
        _numCompleted = 0;
    }

    // Constructor for loaded goal
    public ChecklistGoal(string n, string d, int p, int t, int c) : base(n, d, p)
    {
        _target = t;
        _numCompleted = c;
    }

    // Give points earned so far
    public override int GetPoints()
    {
        if (IsComplete())
        {
            return _points * 2;
        }
        else
        {
            return _points/_target * _numCompleted;
        }
    }

    // Increment counter
    public override void RecordEvent()
    {
        if (!IsComplete())
        {
            _numCompleted ++;
        }
        else
        {
            Console.WriteLine("Goal already completed");
        }
        
    }

    // Returns whether or not goal is complete
    public override bool IsComplete()
    {
        return _numCompleted == _target;
    }

    // String format for printing name
    public override string GetDetails()
    {
        return $"[{_numCompleted}/{_target}] {_name}";
    }

    // String format for printing full details
    public override string GetFullDetails()
    {
        if (IsComplete())
        {
            return $"[{_numCompleted}/{_target}] {_name} ({_desc}) | {_points} pts | {_points*2}/{_points*2} earned | bonus {_points} awarded";
        }
        return $"[{_numCompleted}/{_target}] {_name} ({_desc}) | {_points} pts | {_points / _target * _numCompleted}/{_points} earned | no bonus earned";
    }

    // String format for file saving
    public override string ToString()
    {
        return $"ChecklistGoal:{_name},{_desc},{_points},{_target},{_numCompleted}";
    }
}