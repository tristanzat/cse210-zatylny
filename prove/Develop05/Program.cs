/* How this exceeds requirements:
*  There is a level system that uses the user's points.
*  Levels get increasingly more difficult to achieve.
*/

class Program
{
    static void Main(string[] args)
    {
        GoalManager goalManager = new();

        // Start of program
        Console.Write("Welcome to the goal game!\n" +
                "\t1. Start a new game.\n" + 
                "\t2. Load a previous game.\n" +
                "\t3. Quit\n" +
                "> ");
        
        int choice = int.Parse(Console.ReadLine());

        Console.Clear();

        switch (choice)
        {
            // New game, get user's name
            case 1:
                Console.Write("What is your name? ");
                goalManager.SetName(Console.ReadLine());
                Console.Clear();
                goalManager.Start();
                break;
            
            // Load from a file
            case 2:
                Console.Write("What is the file name (no ending)? ");
                goalManager.LoadFile(Console.ReadLine());
                Console.Clear();
                goalManager.Start();
                break;
            
            // Quit
            default:
                break;
        }

    }
}