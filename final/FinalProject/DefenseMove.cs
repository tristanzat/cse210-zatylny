public class DefenseMove : Move
{
    public DefenseMove(string name, PokemonType type, int pp, int power, int accuracy, int category) : base(name, type, pp, power, accuracy, category) { }

    // User and target are the same
    public override void Use(Pokemon user, Pokemon target)
    {
        
    }

    // Display info
    public override string DisplayInfo()
    {
        return $"{_name} | Type: {_type} | PP: {_curPP}/{_maxPP}";
    }
}