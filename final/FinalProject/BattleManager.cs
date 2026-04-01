using System.Formats.Asn1;

public class BattleManager
{
    // keep track of those in battle
    private Trainer _user;
    private Trainer _opponent;
    private Random random = new();

    public BattleManager(Trainer user, Trainer opponent)
    {
        _user = user;
        _opponent = opponent;
    }

    // methods
    public void StartBattle()
    {
        Console.WriteLine($"Welcome to battle.\nUser's {_user.Active.Name} vs opponent's {_opponent.Active.Name}.");
    }

    public void ExecuteTurn()
    {
        bool validChoice = false;
        int input = -1;

        while (!validChoice)
        {
            Console.WriteLine($"{_opponent.Active.DisplayStats()}\n{_user.Active.DisplayStats()}");
            Console.Write("Choose an action:\n" + 
            "1. Fight\t2. Stats\n" +
            "3. Pokemon\t 4. Run\n>");
            input = int.Parse(Console.ReadLine());
            
            if(input > 0 && input <= 4)
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
                Stats();
                break;
            case 3:
                Party();
                break;
            case 4:
                Run();
                break;
        }

        ResolveStatuses(_user.Active, 1);
        if (_user.Active.IsFainted())
        {
            Console.WriteLine($"{_user.Active.Name} fainted!");
        }
        else
        {
            ResolveStatuses(_opponent.Active, 1);
            if (_opponent.Active.IsFainted())
            {
                Console.WriteLine($"{_opponent.Active.Name} fainted!");
            }
        }
    }

    private void Fight()
    {
        Console.WriteLine($"{_opponent.Active.DisplayStats()}\n{_user.Active.DisplayStats()}");
        Move userMove = _user.Fight();
        Move cpuMove = _opponent.Fight(random.Next(4));

        // Choose who goes first
        int uPriority = userMove.Priority;
        int oPriority = cpuMove.Priority;

        // Higher priority goes first
        if (uPriority > oPriority)
        {
            UseMoves(_user.Active, _opponent.Active, userMove, cpuMove);
        }
        else if (oPriority > uPriority)
        {
            UseMoves(_opponent.Active, _user.Active, cpuMove, userMove);
        }
        // Same priority - speed based
        // Same speed - random
        else
        {
            int uSpd = _user.Active.GetSpeed();
            int oSpd = _opponent.Active.GetSpeed();
            if (uSpd == oSpd)
            {
                // Randomly break tie
                if(random.Next(2) == 0) { uSpd +=  1; }
                else { oSpd += 1; }
            }
            if (uSpd > oSpd)
            {
                UseMoves(_user.Active, _opponent.Active, userMove, cpuMove);
            }
            else
            {
                UseMoves(_opponent.Active, _user.Active, cpuMove, userMove);
            }
        }
    }

    private void Stats()
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
        bool useMove = true;
        // User can't use move if frozen, asleep, or paralyzed
        useMove = ResolveStatuses(user, 0);
        if (useMove)
        {
            userMove.Use(user, target);
        }
        Thread.Sleep(500);
        useMove = true;
        // Check if that killed opponent; if so, opponent can't move
        if (target.IsFainted())
        {
            useMove = false;
        }
        useMove = ResolveStatuses(target, 0);
        if (useMove)
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
        return _user.Active.IsFainted() || _opponent.Active.IsFainted();
    }

    // Handles status ticking either during move or after moves
    public static bool ResolveStatuses(Pokemon target, int tickOrder)
    {
        // Tick order 0 - during move
        if (tickOrder == 0)
        {
            bool useMove = true;
            foreach (Status status in target.Statuses.ToList())
            {
                if (status.TickOrder == tickOrder)
                {
                    bool isFrozen = status is Freeze;
                    bool isParalyzedThisTurn = status is Paralysis paralysis && paralysis.DidParalyze();
                    bool isSleeping = status is Sleep;

                    if (isFrozen || isParalyzedThisTurn || isSleeping)
                    {
                        // Tick status, don't use move
                        status.Tick(target);
                        useMove = false;
                        // If sleep ran out, use move
                        if (status is Sleep sleep && sleep.IsAwake())
                        {
                            useMove = true;
                        }
                    }
                }
            }
            return useMove;
        }
        // Tick order 1 - after moves
        else
        {
            foreach (Status status in target.Statuses.ToList())
            {
                if (status.TickOrder == tickOrder)
                {
                    status.Tick(target);
                }
            }
            return true;
        }
    }
}