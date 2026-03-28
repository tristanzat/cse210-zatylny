using System.Reflection.Metadata;

public class BattleManager
{
    // keep track of those in battle
    private Trainer _user;
    private Trainer _opponent;
    private Pokemon _uActive;
    private Pokemon _oActive;
    // keep track of each party
    private List<Pokemon> _uParty;
    private List<Pokemon> _oParty;
    private Random random = new();

    public BattleManager(Trainer user, Trainer opponent)
    {
        _user = user;
        _opponent = opponent;
        _uActive = user.Active;
        _oActive = opponent.Active;
    }

    // methods
    public void StartBattle()
    {
        Console.WriteLine("Welcome to battle. user vs cpu");
    }

    public void ExecuteTurn()
    {
        bool validChoice = false;
        int input = -1;

        while (!validChoice)
        {
            Console.WriteLine($"{_oActive.DisplayStats()}\n{_uActive.DisplayStats()}");
            Console.Write("Choose an action:\n" + 
            "1. Fight\t2. Bag\n" +
            "3. Pokemon\t 4. Run\n>");
            input = int.Parse(Console.ReadLine());
            
            if(input > 0 && input < 4)
            {
                validChoice = true;
            }

            else
            {
                Console.WriteLine("Invalid choice.");
            }

            Console.Clear();
        }

        switch(input)
        {
            case 1:
                Fight();
                break;
            case 2:
                Bag();
                break;
            case 3:
                Party();
                break;
            case 4:
                Run();
                break;
        }

    }

    private void Fight()
    {
        Console.WriteLine($"{_oActive.DisplayStats()}\n{_uActive.DisplayStats()}");
        Move userMove = _user.Fight();
        Move cpuMove = _opponent.Fight(random.Next(4));

        // Choose who goes first
        int uPriority = userMove.Priority;
        int oPriority = cpuMove.Priority;

        // Higher priority goes first
        if (uPriority > oPriority)
        {
            UseMoves(_uActive, _oActive, userMove, cpuMove);
        }
        else if (oPriority > uPriority)
        {
            UseMoves(_oActive, _uActive, cpuMove, userMove);
        }
        // Same priority - speed based
        // Same speed - random
        else
        {
            int uSpd = _uActive.GetSpeed();
            int oSpd = _oActive.GetSpeed();
            if (uSpd == oSpd)
            {
                // Randomly break tie
                if(random.Next(1, 3) == 1) { uSpd +=  1; }
                else { oSpd += 1; }
            }
            if (uSpd > oSpd)
            {
                UseMoves(_uActive, _oActive, userMove, cpuMove);
            }
            else
            {
                UseMoves(_oActive, _uActive, cpuMove, userMove);
            }
        }
    }

    private void Bag()
    {
        
    }

    private void Party()
    {
        
    }

    private void Run()
    {
        Console.WriteLine("You can't run from a trainer battle!");
    }

    private void UseMoves(Pokemon user, Pokemon target, Move userMove, Move targetMove)
    {
        // Use user's move
        userMove.Use(user, target);
        Thread.Sleep(500);
        // Check if that killed opponent; if not, opponent moves
        if (!target.IsFainted())
        {
            targetMove.Use(target, user);
        }
        // Check if either move killed opponent
        if (target.IsFainted())
        {
            Console.WriteLine($"{target.Name} fainted!");
        }
        else if (user.IsFainted())
        {
            Console.WriteLine($"{user.Name} fainted!");
        }
        Thread.Sleep(500);
    }

    public bool IsOver()
    {
        return _uActive.IsFainted() || _oActive.IsFainted();
    }
}