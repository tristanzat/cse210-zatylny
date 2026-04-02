public class BattleManager
{
    // keep track of those in battle
    private Trainer _user;
    private Trainer _opponent;

    // keep track of which Pokemon opponent has out for switching after a faint
    private int _cpuOut;
    private Random random = new();

    public BattleManager(Trainer user, Trainer opponent)
    {
        _user = user;
        _opponent = opponent;
        _cpuOut = 0;
    }

    public void StartBattle()
    {
        Console.WriteLine($"Welcome to battle.\nUser's {_user.Active.Name} vs opponent's {_opponent.Active.Name}.");
    }

    public void ExecuteTurn()
    {
        // if a Pokemon fainted, prompt a switch for the next one
        while (_user.Active.IsFainted())
        {
            Party();
        }
        if (_opponent.Active.IsFainted())
        {
            _cpuOut ++;
            if (_cpuOut < 6)
            {
                _opponent.Switch(_cpuOut);
            }
        }
        bool validChoice = false;
        int input = -1;

        while (!validChoice)
        {
            Console.WriteLine($"{_opponent.Active.DisplayStats()}\n{_user.Active.DisplayStats()}");
            Console.Write("Choose an action:\n" + 
            "1. Fight\t2. Bag\n" +
            "3. Pokemon\t 4. Run\n> ");
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
                Bag();
                break;
            case 3:
                Party();
                break;
            case 4:
                Run();
                break;
        }

        // Tick post-turn status effects
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
        userMove.CurrentPP--;
        Move cpuMove = _opponent.Fight(random.Next(4));
        cpuMove.CurrentPP--;

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

    private void Bag()
    {
        
    }

    private void Party()
    {
        // Get user's switch-in choice
        Console.WriteLine("Choose a Pokemon to switch in:");
        bool validChoice = false;
        bool swap = true;
        int choice = 0;

        while (!validChoice)
        {
            _user.Party();
            Console.Write("7. Cancel\n> ");
            choice = int.Parse(Console.ReadLine());
            choice--;

            // Handle choosing active pokemon or fainted pokemon
            if (choice >= 6)
            {
                swap = false;
            }
            else if (_user.Team[choice] == _user.Active)
            {
                Console.WriteLine("This Pokemon is already active.");
            }
            else if (_user.Team[choice].IsFainted())
            {
                Console.WriteLine("This Pokemon is fainted.");
            }
            else
            {
                validChoice = true;
            }
        }

        if (swap)
        {
            _user.Switch(choice);
        }
    }

    private static void Run()
    {
        Console.WriteLine("You can't run from a trainer battle!");
    }

    private static void UseMoves(Pokemon user, Pokemon target, Move userMove, Move targetMove)
    {
        // User can't use move if frozen, asleep, or paralyzed
        bool useMove = ResolveStatuses(user, 0);
        if (useMove)
        {
            userMove.Use(user, target);
        }
        Thread.Sleep(500);

        // Check if that killed opponent; if so, opponent can't move
        if (target.IsFainted())
        {
            useMove = false;
        }
        // Handle any statuses that would prevent a move
        else
        {
            useMove = ResolveStatuses(target, 0);
        }

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
        return _user.Defeated() || _opponent.Defeated();
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