public class Poison : Status
{
    private readonly bool _badPoison;
    private int _tickCounter;

    /// <summary>
    /// Creates a new instance of the Poison status
    /// </summary>
    /// <param name="badPoison">Whether or not the poison is badly poisoned or not</param>
    public Poison(bool badPoison) : base("poisoned", true)
    {
        _badPoison = badPoison;
        _tickCounter = 1;
    }

    // Poison deals 1/8 of the pokemon's max hp as damage. If badly poisoned, 
    public override void Tick(Pokemon target)
    {
        // Normal poison
        if (!_badPoison)
        {
            target.TakeDamage(target.MaxHp/8);
        }

        // Bad poison
        else
        {
            target.TakeDamage(target.MaxHp * (_tickCounter/16));
            _tickCounter++;
        }
        Console.WriteLine($"{target.Name} was hurt by its poisoning!");
    }

    // Switching out reverts bad poison's tick counter
    public void SwitchEffect()
    {
        _tickCounter = 1;
    }
}