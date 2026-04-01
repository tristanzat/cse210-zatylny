public class Sleep : Status
{
    private int _duration;
    private int _maxDuration;
    public Sleep() : base("asleep", true, 0)
    {
        Random random = new();
        _maxDuration = random.Next(2,5);
        _duration = _maxDuration;
    }

    // Sleep makes a pokemon unable to use a move. 2-4 turn duration. Waking up allows the move to be used.
    public override void Tick(Pokemon target)
    {
        Console.WriteLine($"{target.Name} is fast asleep.");
        _duration--;
        if (_duration <= 0)
        {
            Console.WriteLine($"{target.Name} woke up!");
            Clear(target);
        }
    }

    // Switching out resets duration
    public override void SwitchEffect(Pokemon target)
    {
        _duration = _maxDuration;
    }

    public bool IsAwake()
    {
        return _duration <= 0;
    }
}