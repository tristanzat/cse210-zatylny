public class Freeze : Status
{
    public Freeze() : base("frozen", true, 0) {}

    // Freeze makes a pokemon unable to use a move. 20% cance of being thawed
    public override void Tick(Pokemon target)
    {
        Console.WriteLine($"{target.Name} is frozen solid!");
        Random random = new();
        if (random.Next(10) < 2)
        {
            Clear(target);
        }
    }
}