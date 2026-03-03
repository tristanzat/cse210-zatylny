public class SimpleGoal : Goal
{
    private bool _complete;
    
    // Normal constructor
    public SimpleGoal(string n, string d, int p) : base(n, d, p)
    {
        _complete = false;
    }

    // Constructor for loaded goal
    public SimpleGoal(string n, string d, int p, bool c) : base(n, d, p)
    {
        _complete = c;
    }

    // Return points
    public override int GetPoints()
    {
        if (IsComplete())
        {
            return base.GetPoints();
        }
        else
        {
            return 0;
        }
    }

    // Record event happens once
    public override void RecordEvent()
    {
        if (!IsComplete())
        {
            _complete = true;
        }
        else
        {
            Console.WriteLine("Goal already marked complete");
        }
        
    }

    // Returns whether or not goal is complete
    public override bool IsComplete()
    {
        return _complete;
    }

    // String format for printing name
    public override string GetDetails()
    {
        if (IsComplete())
        {
            return $"[X] {_name}";
        }
        return $"[ ] {_name}";
    }

    // String format for printing full details
    public override string GetFullDetails()
    {
        if (IsComplete())
        {
            return $"[X] {_name} ({_desc}) | {_points} pts";
        }
        return $"[ ] {_name} ({_desc}) | {_points} pts";
    }


    // String format for file saving
    public override string ToString()
    {
        return $"SimpleGoal:{_name},{_desc},{_points},{_complete}";
    }
}