/* How this program exceeds requirements:
   The scripture to memorize is chosen at random
   from a library of every scripture found in the
   standard works. This is done by parsing a csv.
*/

class Program
{
    static void Main(string[] args)
    {
        // Initialize scripture library
        ScriptureLibrary scriptureLibrary = new();

        // Make a new scripture
        Scripture scripture = scriptureLibrary.GetRandomScripture();
        
        // Variables for looping and ensuring correct display
        bool done = false;
        bool firstPass = true;

        //Main memorization loop
        while(!done)
        {
            // Clear console for readability
            Console.Clear();

            // Don't start hiding words until the second pass
            if (!firstPass) { scripture.HideWords(); }
            else { firstPass = false; }

            // Display scripture and get user's input
            scripture.Display();
            Console.WriteLine("\n\nPress enter to continue or type 'quit' to finish:");
            string choice = Console.ReadLine();

            // Stop the program when user either types quit or the scripture is fully hidden
            if (choice.Equals("quit") || scripture.FullyHidden())
            {
                done = true;
            }
        }
    }
}