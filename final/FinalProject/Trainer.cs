public class Trainer
{
    // Personal team
    public List<Pokemon> Team { get; private set; }
    // Active pokemon
    public Pokemon Active { get; private set; }
    // Number of fainted pokemon
    private int _numFainted;
    // User's bag
    public List<int> Bag { get; private set; }

    public Trainer(List<Pokemon> pokemon)
    {
        Team = pokemon;
        Active = Team[0];
        _numFainted = 0;
        Bag = [0, 0, 0, 1];
    }

    public void Switch(int teamSpot)
    {
        Active = Team[teamSpot];
    }

    /// <summary>
    /// Chooses a move for a user
    /// </summary>
    /// <returns>The user's chosen move</returns>
    public Move Fight()
    {
        bool validChoice = false;
        int input = -1;

        while (!validChoice)
        {
            // Choose moves
            Console.Write($"Choose a move:\n{Active.DisplayMoves()}> ");
            input = int.Parse(Console.ReadLine());
            input--;

            // Make sure move can be chosen (i.e. has PP)
            if (Active.ChooseMove(input).CurrentPP > 0)
            {
                validChoice = true;
            }
            else
            {
                Console.WriteLine("Not enough PP to use this move.");
            }

            Console.Clear();
        }

        return Active.ChooseMove(input);
    }

    /// <summary>
    /// Chooses a move for a cpu
    /// </summary>
    /// <returns>The cpu's chosen move</returns>
    public Move Fight(int choice)
    {
        bool validChoice = false;
        Random random = new();

        while (!validChoice)
        {
            // Make sure move can be chosen (i.e. has PP)
            if (Active.ChooseMove(choice).CurrentPP > 0)
            {
                validChoice = true;
            }
            else
            {
                choice = random.Next(4);
            }
        }

        return Active.ChooseMove(choice);
    }

    public void Party()
    {
        int choiceNum = 1;
        foreach (Pokemon pokemon in Team)
        {
            Console.Write($"{choiceNum}. {pokemon.Name} ");
            foreach(Status status in pokemon.Statuses)
            {
                Console.Write($"({status.GetInfo()})");
            }
            if(pokemon == Active)
            {
                Console.Write("(active)");
            }
            if(pokemon.IsFainted())
            {
                Console.Write("(fainted)");
            }
            Console.WriteLine();
            choiceNum ++;
        }
    }

    private void CheckFainted()
    {
        _numFainted = 0;
        foreach (Pokemon pokemon in Team)
        {
            if (pokemon.IsFainted())
            {
                _numFainted ++;
            }
        }
    }

    public bool Defeated()
    {
        CheckFainted();
        return _numFainted >= Team.Count;
    }
}