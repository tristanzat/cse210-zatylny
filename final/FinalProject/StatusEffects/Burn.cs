public class Burn : Status
{
    public Burn() : base("burned", true) {}

    // Burn deals 1/16 of the pokemon's max hp as damage
    public override void Tick(Pokemon target)
    {
        target.TakeDamage(target.MaxHp/16);
        Console.WriteLine($"{target.Name} was hurt by its burn!");
    }
}