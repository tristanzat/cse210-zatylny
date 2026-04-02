public class StatusMove : DamageMove
{
    private List<Status> _statuses;
    private List<int> _applicationChances;

    /// <summary>
    /// Creates a new instance of a DamageMove
    /// </summary>
    /// <param name="name">Move name</param>
    /// <param name="type">Move type</param>
    /// <param name="pp">Move max PP</param>
    /// <param name="power">Move power</param>
    /// <param name="accuracy">Move accuracy</param>
    /// <param name="category">Move category; 0 = physical, 1 = special, 2 = status</param>
    /// <param name="effects">A list of status effects to apply</param>
    /// <param name="applicationChances">A list of the chances for respective statuses to apply, out of 100</param>
    public StatusMove(string name, PokemonType type, int pp, int power, int accuracy, int category, List<Status> effects, List<int> applicationChances) : base(name, type, pp, power, accuracy, category)
    {
        _statuses = effects;
        _applicationChances = applicationChances;
    }

    /// <summary>
    /// Creates a new instance of a DamageMove
    /// </summary>
    /// <param name="name">Move name</param>
    /// <param name="type">Move type</param>
    /// <param name="pp">Move max PP</param>
    /// <param name="power">Move power</param>
    /// <param name="accuracy">Move accuracy</param>
    /// <param name="category">Move category; 0 = physical, 1 = special, 2 = status</param>
    /// <param name="priority">Move priority</param>
    /// <param name="effects">A list of status effects to apply</param>
    /// <param name="applicationChances">A list of the chances for respective statuses to apply</param>
    public StatusMove(string name, PokemonType type, int pp, int power, int accuracy, int category, int priority, List<Status> effects, List<int> applicationChances) : base(name, type, pp, power, accuracy, category, priority)
    {
        _statuses = effects;
        _applicationChances = applicationChances;
    }

    public override void Use(Pokemon user, Pokemon target)
    {
        Random random = new();
        base.Use(user, target);
        if(_didHit)
        {
            for (int i = 0; i < _statuses.Count; i++)
            {
                // Get status
                Status status = _statuses[i];

                // See if chance to apply succeeds
                if (random.Next(100) < _applicationChances[i])
                {
                    // If status is non-volatile and a non-volatile status is already applied, don't apply
                    if (status.IsNonVolatile())
                    {
                        // No non-volatile statuses applied
                        if(!target.Statuses.Any(status => status.IsNonVolatile()))
                        {
                            status.Apply(target);
                        }
                    }
                    else
                    {
                        status.Apply(target);
                    }
                }
            }
        }
    }

    
}