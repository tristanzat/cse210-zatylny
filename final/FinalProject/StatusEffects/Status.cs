public abstract class Status
{
    protected string _name;
    /// <summary>
    /// Non-volatile status effects remain upon switching out. Can have only one non-volatile status effect at a time.
    /// </summary>
    protected bool _isNonVolatile;
    /// <summary>
    /// Defines when the status effect goes off. 0 = before move use, 1 = after all turns
    /// </summary>
    public int TickOrder { get; private set; }

    public Status(string name, bool isNonVolatile, int tickOrder)
    {
        _name = name;
        _isNonVolatile = isNonVolatile;
        TickOrder = tickOrder;
    }

    /// <summary>
    /// What the status effect does
    /// </summary>
    /// <param name="target">The Pokemon with the status</param>
    public abstract void Tick(Pokemon target);

    /// <summary>
    /// Applies a status effect
    /// </summary>
    /// <param name="target">The Pokemon to apply the status to</param>
    public virtual void Apply(Pokemon target)
    {
        Console.WriteLine($"{target.Name} was {_name}!");
        target.Statuses.Add(this);
    }

    public virtual string GetInfo()
    {
        return _name;
    }

    public virtual void Clear(Pokemon target)
    {
        target.Statuses.Remove(this);
    }

    public virtual bool IsNonVolatile()
    {
        return _isNonVolatile;
    }

    public virtual void SwitchEffect(Pokemon target)
    {
        // Volatile status effects clear on switch
        if(!IsNonVolatile())
        {
            Clear(target);
        }
    }
}