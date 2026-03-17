public abstract class Move
{
    // Member variables
    // Move name and type
    protected string _name;
    protected PokemonType _type;
    // PP, power, accuracy
    protected int _curPP;
    protected int _maxPP;
    protected int _power;
    protected int _accuracy;
    /// <summary>
    /// Category - 0 = physical, 1 = special, 2 = status
    /// </summary>
    protected int _category;
    protected int _priority;

    protected static Random random = new();

    public Move(string name, PokemonType type, int pp, int power, int accuracy, int category)
    {
        _name = name;
        _type = type;
        _maxPP = pp;
        _curPP = pp;
        _power = power;
        _accuracy = accuracy;  
        _category = category; 
        _priority = 0;
    }

    public Move(string name, PokemonType type, int pp, int power, int accuracy, int category, int priority)
    {
        _name = name;
        _type = type;
        _maxPP = pp;
        _curPP = pp;
        _power = power;
        _accuracy = accuracy;  
        _category = category; 
        _priority = priority;
    }

    public virtual int GetPriority() { return _priority; }

    public abstract void Use(Pokemon user, Pokemon target);

    public abstract string DisplayInfo();

}