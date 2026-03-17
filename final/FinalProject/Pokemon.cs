// Nullable type 2 for dual type pokemon
#nullable enable

public class Pokemon
{
    // Member variables
    
    // Name and type(s)
    /// <summary>
    /// Pokemon's name
    /// </summary>
    private string _name;
    /// <summary>
    /// Base type
    /// </summary>
    private PokemonType _type;
    /// <summary>
    /// Secondary type (if it has one)
    /// </summary>
    private PokemonType? _type2;
    /// <summary>
    /// Current level
    /// </summary>
    private int _level;
    
    // 6 base stats - HP, attack, defense, special attack, special defense, speed
    /// <summary>
    /// Maximum HP
    /// </summary>
    private int _maxHp;
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
    
    /// <summary>
    /// Move list
    /// </summary>
    private List<Move> _moves;

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
        _name = name;
        _level = level;
        _type = type1;
        _type2 = type2;
        _maxHp = hp;
        _curHp = hp;
        _atk = atk;
        _def = def;
        _spAtk = spAtk;
        _spDef = spDef;
        _speed = speed;
        _moves = moves;
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
    /// Returns attack of a Pokemon
    /// </summary>
    /// <param name="category">Whether to return special attack (1) or physical attack (0)</param>
    /// <returns>The corresponding attack value.</returns>
    public int GetAttack(int category)
    {
        // Return special attack
        if (category == 1) { return _spAtk; }

        // Return physical attack
        else { return _atk; }
    }

    /// <summary>
    /// Returns attack of a Pokemon
    /// </summary>
    /// <param name="category">Whether to return special defense (1) or physical defense (0)</param>
    /// <returns>The corresponding attack value.</returns>
    public int GetDefense(int category)
    {
        // Return special attack
        if (category == 1) { return _spDef; }

        // Return physical attack
        else { return _def; }
    }

    public int GetLevel()
    {
        return _level;
    }

    public string GetName()
    {
        return _name;
    }

    public int GetSpeed()
    {
        return _speed;
    }

    public PokemonType?[] GetTypes()
    {
        return [_type, _type2];
    }

    public string DisplayStats()
    {
        return $"{_name} | HP: {_curHp}/{_maxHp}";
    }

    public bool IsFainted()
    {
        return _curHp <= 0;
    }

}