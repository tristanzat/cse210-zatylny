public abstract class Status
{
    protected string _name;
    /// <summary>
    /// Non-volatile status effects remain upon switching out. Can have only one non-volatile status effect at a time.
    /// </summary>
    protected bool _isVolatile;

    public Status(string name, bool isVolatile)
    {
        _name = name;
        _isVolatile = isVolatile;
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
}