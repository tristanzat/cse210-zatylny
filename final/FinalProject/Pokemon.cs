// Nullable type 2 for dual type pokemon
#nullable enable

public class Pokemon
{
    // Member variables
    
    // Name and type(s)
    /// <summary>
    /// Pokemon's name
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// Base type
    /// </summary>
    public PokemonType?[] Types { get; set; } = new PokemonType?[2];
    /// <summary>
    /// Current level
    /// </summary>
    public int Level { get; private set; }
    
    // 6 base stats - HP, attack, defense, special attack, special defense, speed
    /// <summary>
    /// Maximum HP
    /// </summary>
    public int MaxHp { get; private set; }
    /// <summary>
    /// Current HP
    /// </summary>
    private int _curHp;
    /// <summary>
    /// Physical attack bonus
    /// </summary>
    private int _atk;
    /// <summary>
    /// Physical defense bonus
    /// </summary>
    private int _def;
    /// <summary>
    /// Special attack bonus
    /// </summary>
    private int _spAtk;
    /// <summary>
    /// Special defense bonus
    /// </summary>
    private int _spDef;
    /// <summary>
    /// Speed (determines move order)
    /// </summary>
    private int _speed;

    // IVs & EVs
    private readonly int[] _ivs;
    private readonly int[] _evs;

    // Nature
    private PokemonNature _nature;

    /// <summary>
    /// In-battle stage modifiers <br/>
    /// 0 - atk, 1 - def, 2 - spAtk, 3 - spDef, 4 - speed, 5 - accuracy, 6 - evasiveness <br/>
    /// Each can go from -6 to  6
    /// </summary>
    public int[] StageMods { get; set; } = new int[7];
    
    /// <summary>
    /// Move list
    /// </summary>
    private List<Move> _moves;

    public List<Status> Statuses { get; set; }

    /// <summary>
    /// Creates a new instance of a Pokemon
    /// </summary>
    /// <param name="name">Pokemon's name</param>
    /// <param name="level">Pokemon's level</param>
    /// <param name="type1">Base type</param>
    /// <param name="type2">Secondary type. Can be null.</param>
    /// <param name="hp">Base HP</param>
    /// <param name="atk">Base attack</param>
    /// <param name="def">Base defense</param>
    /// <param name="spAtk">Base special attack</param>
    /// <param name="spDef">Base special defense</param>
    /// <param name="speed">Base speed</param>
    /// <param name="moves">List of moves</param>
    public Pokemon(string name, int level, PokemonType type1, PokemonType? type2, int hp, int atk, int def, int spAtk, int spDef, int speed, List<Move> moves)
    {
        Random random = new();
        Name = name;
        Level = level;
        Types[0] = type1;
        Types[1] = type2;
        _moves = moves;
        _ivs = SetIVs();
        _evs = SetEVs();
        _nature = (PokemonNature) random.Next(25);
        for (int i = 0; i < 6; i++)
        {
            switch (i)
            {
                case 0:
                    hp = CalculateStat(hp, i);
                    break;
                case 1:
                    atk = CalculateStat(atk, i);
                    break;
                case 2:
                    def = CalculateStat(def, i);
                    break;
                case 3:
                    spAtk = CalculateStat(spAtk, i);
                    break;
                case 4:
                    spDef = CalculateStat(spDef, i);
                    break;
                case 5:
                    speed = CalculateStat(speed, i);
                    break;
            }
        }
        StageMods = [0, 0, 0, 0, 0, 0, 0];
        MaxHp = hp;
        _curHp = hp;
        _atk = atk;
        _def = def;
        _spAtk = spAtk;
        _spDef = spDef;
        _speed = speed;
        Statuses = [];
    }

    /// <summary>
    /// Handles taking damage
    /// </summary>
    /// <param name="damage">Amount of damage</param>
    public void TakeDamage(int damage)
    {
        _curHp -= damage;
    }

    /// <summary>
    /// Handles healing
    /// </summary>
    /// <param name="heal">Amount of healing</param>
    public void Heal(int heal)
    {
        _curHp += heal;
    }

    /// <summary>
    /// Choose a move from the move list to use
    /// </summary>
    /// <param name="moveIndex">Index of move to use</param>
    /// <returns>The chosen move</returns>
    public Move ChooseMove(int moveIndex)
    {
        return _moves[moveIndex];
    }

    public string DisplayMoves()
    {
        string display = "";
        for (int i = 0; i < 4; i++)
        {
            display += $"{i+1}. ";
            display += ChooseMove(i).DisplayInfo();
            display += "\n";
        }
        return display;
    }

    /// <summary>
    /// Returns attack of a Pokemon affected by stage modifiers
    /// </summary>
    /// <param name="category">Whether to return special attack (1) or physical attack (0)</param>
    /// <returns>The corresponding attack value.</returns>
    public double GetAttack(int category)
    {
        // Return special attack
        if (category == 1)
        {
            return _spAtk * StatModifier(StageMods[2]);
        }

        // Return physical attack
        else
        {
            return _atk * StatModifier(StageMods[0]);
        }
    }

    /// <summary>
    /// Returns attack of a Pokemon
    /// </summary>
    /// <param name="category">Whether to return special defense (1) or physical defense (0)</param>
    /// <returns>The corresponding attack value.</returns>
    public double GetDefense(int category)
    {
        // Return special defense
        if (category == 1)
        {
            return _spDef * StatModifier(StageMods[3]);
        }

        // Return physical defense
        else
        {
            return _def * StatModifier(StageMods[1]);
        }
    }

    public int GetSpeed()
    {
        double speed = _speed * StatModifier(StageMods[4]);
        if (Statuses.Any(status => status is Paralysis))
        {
            speed *= 0.5;
        }
        return (int)speed;
    }

    /// <summary>
    /// Gets a multiplier for a stat based on the in-battle stage modifiers.
    /// </summary>
    /// <param name="statMod">The modifier number for the stat</param>
    /// <returns>A multiplier based on the stat's stage</returns>
    public static double StatModifier(int statMod)
    {
        return statMod switch
        {
            -6 => 2 / 8,
            -5 => 2 / 7,
            -4 => 2 / 6,
            -3 => 2 / 5,
            -2 => 2 / 4,
            -1 => 2 / 3,
            0 => 1,
            1 => 3 / 2,
            2 => 4 / 2,
            3 => 5 / 2,
            4 => 6 / 2,
            5 => 7 / 2,
            6 => 8 / 2,
            _ => 1,
        };
    }

    /// <summary>
    /// Gets a multiplier for a move hitting based on a combined total of user's accuracy and opponent's evasion
    /// </summary>
    /// <param name="totalModifier">The total of user's accuracy and opponent's evasion<br/>Minimum of -6, maximum of 6</param>
    /// <returns>A double to multiply move accuracy by</returns>
    public static double HitModifier(int totalModifier)
    {
        return totalModifier switch
        {
            -6 => 3 / 9,
            -5 => 3 / 8,
            -4 => 3 / 7,
            -3 => 3 / 6,
            -2 => 3 / 5,
            -1 => 3 / 4,
            0 => 1,
            1 =>  4 / 3,
            2 =>  5 / 3,
            3 =>  6 / 3,
            4 =>  7 / 3,
            5 =>  8 / 3,
            6 =>  9 / 3,
            _ => 1
        };
    }

    /// <summary>
    /// Get a string displaying a Pokemon's name, statuses, and HP
    /// </summary>
    /// <returns>A string displaying a Pokemon's name, statuses, and HP</returns>
    public string DisplayStats()
    {
        string display = $"{Name} ";

        if (Statuses.Count > 0)
        {
            foreach (Status status in Statuses)
            {
                display += $"({status.GetInfo()}) ";
            }
        }
        
        display += $"| HP: {_curHp}/{MaxHp}";
        
        return display;
    }

    public bool IsFainted()
    {
        return _curHp <= 0;
    }

    public List<Move> GetMoves()
    {
        return _moves;
    }

    private static int[] SetIVs()
    {
        Random random = new();
        int[] ivs = new int[6];
        // IVs can be any number between 0 and 31
        for (int i = 0; i < 6; i++)
        {
            ivs[i] = random.Next(32);
        }
        return ivs;
    }

    private static int[] SetEVs()
    {
        Random random = new();
        int[] evs = new int[6];
        // EVs can be any number between 0 and 252; the total of EVs can only be 510
        int total = 510;
        for (int i = 0; i < 6; i++)
        {
            // Get a number for a stat
            bool allowedNum = false;
            int statNum = 0;
            while (!allowedNum)
            {
                statNum = random.Next(Math.Min(253, total));
                if (!((total - statNum) < 0))
                {
                    allowedNum = true;
                }
            }
            evs[i] = statNum;
        }
        return evs;

    }

    /// <summary>
    /// Calculate stat based on IVs and EVs and stuff
    /// </summary>
    /// <param name="statNum">the stat number</param>
    /// <param name="statType">the stat location type thing<br>HP = 0, atk = 1, def = 2, spatk = 3, spdef = 4, speed = 5</param>
    /// <returns></returns>
    private int CalculateStat(int statNum, int statType)
    {
        // Stat calculation: https://bulbapedia.bulbagarden.net/wiki/Stat#Generation_III_onward
        // HP: floor(((2 * base + IV + floor(EV/4))*level)/100) + level + 10
        // Others: floor((floor(((2 * base + IV + floor(EV/4))*level)/100) + 5) * nature)
        int stat = 0;

        // HP
        if (statType == 0)
        {
            stat = (int)Math.Floor(((2 * statNum + _ivs[0] + Math.Floor((double)(_evs[0]/4)))*Level/100) + Level + 10);
        }
        // Everything else
        else
        {
            stat = (int)Math.Floor((Math.Floor(((2 * statNum + _ivs[statType] + Math.Floor((double)(_evs[0]/4)))*Level)/100) + 5) * NatureValue(statType));
        }

        return stat;
    }

    private double NatureValue(int statType)
    {
        switch (statType)
        {
            // Attack boosted by lonely, brave, adamant, naughty; attack drained by bold, timid, modest, calm
            case 1:
                if(_nature == PokemonNature.Lonely || _nature == PokemonNature.Brave || _nature == PokemonNature.Adamant || _nature == PokemonNature.Naughty)
                {
                    return 1.1;
                }
                else if(_nature == PokemonNature.Bold || _nature == PokemonNature.Timid || _nature == PokemonNature.Modest || _nature == PokemonNature.Calm)
                {
                    return 0.9;
                }
                else
                {
                    return 1;
                }
            // Defense boosted by bold, relaxed, impish, lax; drained by lonely, hasty, mild, gentle
            case 2:
                if(_nature == PokemonNature.Bold || _nature == PokemonNature.Relaxed || _nature == PokemonNature.Impish || _nature == PokemonNature.Lax)
                {
                    return 1.1;
                }
                else if(_nature == PokemonNature.Lonely || _nature == PokemonNature.Hasty || _nature == PokemonNature.Mild || _nature == PokemonNature.Gentle)
                {
                    return 0.9;
                }
                else
                {
                    return 1;
                }
            // SpAtk boosted by modest, mild, quiet, rash; drained by adamant, impish, jolly, careful
            case 3:
            if(_nature == PokemonNature.Modest || _nature == PokemonNature.Mild || _nature == PokemonNature.Quiet || _nature == PokemonNature.Rash)
                {
                    return 1.1;
                }
                else if(_nature == PokemonNature.Adamant || _nature == PokemonNature.Impish || _nature == PokemonNature.Jolly || _nature == PokemonNature.Careful)
                {
                    return 0.9;
                }
                else
                {
                    return 1;
                }
            // SpDef boosted by calm, gentle, sassy, careful; drained by naughty, lax, naive, rash
            case 4:
                if(_nature == PokemonNature.Calm || _nature == PokemonNature.Gentle || _nature == PokemonNature.Sassy || _nature == PokemonNature.Careful)
                {
                    return 1.1;
                }
                else if(_nature == PokemonNature.Naughty || _nature == PokemonNature.Lax || _nature == PokemonNature.Naive || _nature == PokemonNature.Rash)
                {
                    return 0.9;
                }
                else
                {
                    return 1;
                }
            // Speed boosted by timid, hasty, jolly, naive; drained by brave, relaxed, quiet, sassy
            case 5:
                if(_nature == PokemonNature.Timid || _nature == PokemonNature.Hasty || _nature == PokemonNature.Jolly || _nature == PokemonNature.Naive)
                {
                    return 1.1;
                }
                else if(_nature == PokemonNature.Brave || _nature == PokemonNature.Relaxed || _nature == PokemonNature.Quiet || _nature == PokemonNature.Sassy)
                {
                    return 0.9;
                }
                else
                {
                    return 1;
                }
            default:
                return 1;
        }
    }

}