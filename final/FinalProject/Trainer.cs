public class Trainer
{
    // Personal team
    private List<Pokemon> _team;
    // Active pokemon
    public Pokemon Active { get; private set; }

    public Trainer(List<Pokemon> pokemon)
    {
        _team = pokemon;
        Active = _team[0];
    }

    public void Switch(int teamSpot)
    {
        Active = _team[teamSpot];
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

    public void Bag()
    {
        
    }

    public void Party()
    {
        
    }

    public void Run()
    {
        
    }
}