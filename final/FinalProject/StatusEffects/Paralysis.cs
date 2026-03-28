public class Paralysis : Status
{
    private bool _didParalyze;
    public Paralysis() : base("paralyzed", true) {}

    // Paralysis decreases speed by 50% and has a 25% chance of making a move fail
    public override void Tick(Pokemon target)
    {
        Random random = new();
        _didParalyze = random.Next(4) == 0;
        if (DidParalyze())
        {
            Console.WriteLine($"{target.Name} couldn't move because it's paralyzed!");
        }
        else
        {
            Console.WriteLine($"{target.Name} is paralyzed, so it may be unable to move!");
        }
    }

    public bool DidParalyze()
    {
        return _didParalyze;
    }    
}