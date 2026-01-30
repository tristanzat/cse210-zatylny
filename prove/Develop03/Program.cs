using System;

class Program
{
    static void Main(string[] args)
    {
        // Make a new scripture
        Scripture scripture = new("Moses", 1, 39, ["For", "behold,", "this", "is", "my", "work", "and", "my", "glory—","to","bring","to","pass","the","immortality","and","eternal","life","of","man."]);
        
        // Variables for looping and ensuring correct display
        bool done = false;
        bool firstPass = true;

        //Main memorization loop
        while(!done)
        {
            // Don't start hiding words until the second pass
            if (!firstPass) { scripture.HideWords(); }
            else { firstPass = false; }

            // Display scripture and get user's input
            scripture.Display();
            Console.WriteLine("\n\nPress enter to continue or type 'quit' to finish: ");
            string choice = Console.ReadLine();

            // Stop the program when user either types quit or the scripture is fully hidden
            if (choice.Equals("quit") || scripture.FullyHidden())
            {
                done = true;
            }
        }
    }
}