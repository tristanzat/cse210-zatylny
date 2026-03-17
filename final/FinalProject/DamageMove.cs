public class DamageMove : Move
{
    private int _stage;
    
    public DamageMove(string name, PokemonType type, int pp, int power, int accuracy, int category) : base(name, type, pp, power, accuracy, category)
    {
        _stage = 0;
        if (name.Equals("Slash"))
        {
            _stage = 1;
        }
    }

    public DamageMove(string name, PokemonType type, int pp, int power, int accuracy, int category, int priority) : base(name, type, pp, power, accuracy, category, priority)
    {
        _stage = 0;
        if (name.Equals("Slash"))
        {
            _stage = 1;
        }
    }

    private static int Round(double value)
    {
        // Check if the fractional part is exactly 0.5
        if (value % 1.0 == 0.5 || value % 1.0 == -0.5)
        {
            // Round down for exactly 0.5 (e.g., 2.5 becomes 2)
            return (int)Math.Floor(value);
        }
        else
        {
            // For all other cases, use standard rounding
            return (int)Math.Round(value);
        }
    }


    public override void Use(Pokemon user, Pokemon target)
    {
        // Damage calculation: https://bulbapedia.bulbagarden.net/wiki/Damage#Generation_V_onward
        // (((((2 * level / 5) + 2) * power * atk/def ) / 50) + 2 ) * targets * parental bond * weather * glaive rush * critical * random * STAB * type * burn * other * ZMove * TeraShield
        double crit = Crit(_stage);
        double effectiveness = TypeChart.GetMultiplier(_type, target.GetTypes());
        double calcDamage = ((((2 * user.GetLevel() / 5) + 2) * _power * user.GetAttack(_category) / target.GetDefense(_category) / 50) + 2) * crit * (random.Next(85, 101) / 100.0) * effectiveness;
        int damage = Round(calcDamage);

        Console.WriteLine($"{user.GetName()} used {_name}!");

        if (effectiveness > 1)
        {
            Console.Write("It's super effective! ");
        }
        else if (effectiveness < 1)
        {
            Console.Write("It's not very effective... ");
        }

        if (crit > 1)
        {
            Console.Write("A critical hit! ");
        }

        Console.WriteLine($"Dealt {damage} damage.\n");

        target.TakeDamage(damage);
    }

    private static double Crit(int stage)
    {
        switch (stage)
        {
            // 1 in 24
            case 0:
                if (random.Next(1,25) == 1) { return 1.5; }
                return 1;
            // 1 in 8
            case 1:
                if (random.Next(1,9) == 1) { return 1.5; }
                return 1;
            // 1 in 2
            case 2:
                if (random.Next(1,3) == 1) { return 1.5; }
                return 1;
            // Always
            default:
                return 1.5;
                
        }
    }

    public override string DisplayInfo()
    {
        return $"{_name} | Type: {_type} | PP: {_curPP}/{_maxPP}";
    }
}