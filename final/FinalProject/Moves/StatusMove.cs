public class StatusMove : DamageMove
{
    private List<Status> _statuses;
    public StatusMove(string name, PokemonType type, int pp, int power, int accuracy, int category, List<Status> effects) : base(name, type, pp, power, accuracy, category)
    {
        _statuses = effects;
    }

    public StatusMove(string name, PokemonType type, int pp, int power, int accuracy, int category, int priority, List<Status> effects) : base(name, type, pp, power, accuracy, category, priority)
    {
        _statuses = effects;
    }

    public override void Use(Pokemon user, Pokemon target)
    {
        base.Use(user, target);
        if(_didHit)
        {
            foreach (Status status in _statuses)
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