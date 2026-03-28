using System.ComponentModel.DataAnnotations;

public class DamageMove : Move
{
    private int _stage;
    protected bool _didHit;
    
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
        double effectiveness = TypeChart.GetMultiplier(_type, target.Types);
        // STAB - same-type attack bonus. 1.5 if move type matches pokemon type
        double stab = 1;
        if(user.Types.Any(type => type == _type))
        {
            stab = 1.5;
        }

        // If move is physical and pokemon is burned, burn is 0.5
        double burn = 1;
        if (user.Statuses.Any(status => status is Burn) && _category == 0)
        {
            burn = 0.5;
        }
        int damage = Round(((((2 * user.Level / 5) + 2) * _power * user.GetAttack(_category) / target.GetDefense(_category) / 50) + 2) * crit * (random.Next(85, 101) / 100.0) * stab * effectiveness * burn);

        Console.WriteLine($"{user.Name} used {_name}!");
        Thread.Sleep(500);

        _didHit = DidHit(user.StageMods[5], target.StageMods[6]);

        // Determine whether or not the move hits
        if(_didHit)
        {
            // Messages for effectiveness and crit
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

        else
        {
            Console.WriteLine("The attack missed!");
        }
    }

    private static double Crit(int stage)
    {
        switch (stage)
        {
            // 1 in 24
            case 0:
                if (random.Next(24) == 0) { return 1.5; }
                return 1;
            // 1 in 8
            case 1:
                if (random.Next(8) == 0) { return 1.5; }
                return 1;
            // 1 in 2
            case 2:
                if (random.Next(2) == 0) { return 1.5; }
                return 1;
            // Always
            default:
                return 1.5;
                
        }
    }

    private bool DidHit(int uAccuracy, int tEvasion)
    {
        // Determining wheter or not a move hits
        // https://bulbapedia.bulbagarden.net/wiki/Accuracy#Generation_V_onward
        // AccMod = AccMove * Modifier * AdjustedStages * MicleBerry - Affection
        // if randomNum ≤ AccMod, move hits
        int randomNum = random.Next(1, 101);

        // Get adjusted stages. Min -6, max 6
        int stageMod = uAccuracy - tEvasion;
        stageMod = Math.Max(stageMod, -6);
        stageMod = Math.Min(stageMod, 6);

        // Modified accuracy
        int accMod = Round(_accuracy * Pokemon.HitModifier(stageMod));
        if (randomNum <= accMod)
        {
            return true;
        }
        return false;
    }

    public override string DisplayInfo()
    {
        return $"{_name} | Type: {_type} | PP: {CurrentPP}/{_maxPP}";
    }
}