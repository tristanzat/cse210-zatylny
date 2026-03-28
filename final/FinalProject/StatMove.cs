public class StatMove : Move
{
    /// <summary>
    /// Which stats are affected.<br/>
    /// 0 - atk, 1 - def, 2 - spAtk, 3 - spDef, 4 - speed, 5 - accuracy, 6 - evasiveness
    /// </summary>
    private readonly List<int> _statsAffected;

    /// <summary>
    /// The degree to which the stat changed
    /// </summary>
    private readonly List<int> _degreeChanged;

    /// <summary>
    /// Which Pokemon the stat change affects; true = user, false = opponent
    /// </summary>
    private readonly List<bool> _target;


    /// <summary>
    /// Creates a new instance of a StatMove
    /// </summary>
    /// <param name="name">Move name</param>
    /// <param name="type">Move type</param>
    /// <param name="pp">Move max PP</param>
    /// <param name="power">Move power</param>
    /// <param name="accuracy">Move accuracy</param>
    /// <param name="category">Move category; 0 = physical, 1 = special, 2 = status</param>
    /// <param name="statsAffected">A list of which stats are affected<br/>0 - atk, 1 - def, 2 - spAtk, 3 - spDef, 4 - speed, 5 - accuracy, 6 - evasiveness</param>
    /// <param name="degreeChanged">A list containing the degrees to which previous listed stats should change, ranging from -6 to 6</param>
    /// <param name="target">A list containing which Pokemon the stat change affects; true = user, false = opponent</param>
    public StatMove(string name, PokemonType type, int pp, int power, int accuracy, int category, List<int> statsAffected, List<int> degreeChanged, List<bool> target) : base(name, type, pp, power, accuracy, category)
    {
        _statsAffected = statsAffected;
        _degreeChanged = degreeChanged;
        _target = target;
    }

    public override void Use(Pokemon user, Pokemon target)
    {
        Console.WriteLine($"{user.Name} used {_name}!");
        Thread.Sleep(500);

        string useMessage = $"{user.Name}'s ";

        for (int i = 0; i < _statsAffected.Count; i++)
        {
            // Get the stat to change
            int stat = _statsAffected[i];

            // Message forming
            switch(stat)
            {
                case 0:
                    useMessage += "attack ";
                    break;
                case 1:
                    useMessage += "defense ";
                    break;
                case 2:
                    useMessage += "special attack ";
                    break;
                case 3:
                    useMessage += "special defense ";
                    break;
                case 4:
                    useMessage += "speed ";
                    break;
                case 5:
                    useMessage += "accuracy ";
                    break;
                case 6:
                    useMessage += "evasiveness ";
                    break;                
            }

            // Different message if a stat won't change
            bool skipChange = false;
            if (user.StageMods[stat] == 6)
            {
                useMessage += "won't go any higher!";
                skipChange = true;
            }
            else if (user.StageMods[stat] == -6)
            {
                useMessage += "won't go any lower!";
                skipChange = true;
            }

            if (!skipChange)
            {
                // Message forming
                if (_degreeChanged[i] < -2)
                {
                    useMessage += "severely fell!";
                }
                else if (_degreeChanged[i] > 2)
                {
                    useMessage += "rose drastically!";
                }
                else
                {
                    switch(_degreeChanged[i])
                    {
                        case -2:
                            useMessage += "harshly fell!";
                            break;
                        case -1:
                            useMessage += "fell!";
                            break;
                        case 0:
                            break;
                        case 1:
                            useMessage += "rose!";
                            break;
                        case 2:
                            useMessage += "rose sharply!";
                            break;
                        default:
                            break;
                    }
                }


                // Increase or decrease stat by the degree to which it changes
                user.StageMods[stat] += _degreeChanged[i];
                
                // Cap both ends
                if (user.StageMods[stat] >= 6)
                {
                    user.StageMods[stat] = 6;   
                }
                else if (user.StageMods[stat] <= -6)
                {
                    user.StageMods[stat] = -6;
                }
            }
        }
    }

    // Display info
    public override string DisplayInfo()
    {
        return $"{_name} | Type: {_type} | PP: {CurrentPP}/{_maxPP}";
    }
}