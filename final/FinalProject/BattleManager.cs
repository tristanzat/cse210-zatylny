using System.Reflection.Metadata;

public class BattleManager
{
    // keep track of those in battle
    private Pokemon _uActive;
    private Pokemon _oActive;
    // keep track of each party
    private List<Pokemon> _uParty;
    private List<Pokemon> _oParty;
    private Random random = new();

    public BattleManager(Pokemon user, Pokemon opponent)
    {
        _uActive = user;
        _oActive = opponent;
    }

    // methods
    public void StartBattle()
    {
        Console.WriteLine("Welcome to battle. user vs cpu");
    }

    public void ExecuteTurn()
    {
        // Choose moves
        Console.WriteLine($"{_uActive.DisplayStats()}\n{_oActive.DisplayStats()}");
        Console.Write($"Choose a move:\n{_uActive.DisplayMoves()}> ");
        int input = int.Parse(Console.ReadLine());
        Console.Clear();

        input--;
        Move userMove = _uActive.ChooseMove(input);

        // Opponent move
        Move cpuMove = _oActive.ChooseMove(random.Next(4));

        // Choose who goes first
        int uPriority = userMove.GetPriority();
        int oPriority = cpuMove.GetPriority();

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

    private void UseMoves(Pokemon user, Pokemon target, Move userMove, Move targetMove)
    {
        userMove.Use(user, target);
        Thread.Sleep(500);
        if (!target.IsFainted())
        {
            targetMove.Use(target, user);
        }
        else
        {
            Console.WriteLine($"{target.GetName()} fainted!");
        }
        Thread.Sleep(500);
    }

    public bool IsOver()
    {
        return _uActive.IsFainted() || _oActive.IsFainted();
    }
}