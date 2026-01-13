using System;

class Program
{
    static void Main(string[] args)
    {
        // Instantiate random for number generation
        Random rand = new Random();

        // Instantiate variables for the game
        int magic_number;
        int user_guess;
        int guess_count;
        bool done;

        // Game start info
        Console.WriteLine("Welcome to the magic number game. You will " +
            "guess a number between 1 and 100, and you will be told whether your " +
            "guess is too high or too low. Keep guessing until you guess the correct number.");
        
        // Do-while loop to allow user to play again
        do {
            // Set game variables to defaults
            magic_number = rand.Next(100);
            done = false;
            guess_count = 0;

            // Newline to add a little more space between intro/games
            Console.WriteLine();

            // Game loop
            while (!done)
            {
                // Get user input
                Console.Write("What is your guess? ");
                user_guess = int.Parse(Console.ReadLine());

                // Compare input to real number
                // Guess was too low
                if (user_guess < magic_number)
                {
                    Console.WriteLine("Higher");
                    guess_count ++;
                }
                // Guess was too high
                else if (user_guess > magic_number)
                {
                    Console.WriteLine("Lower");
                    guess_count ++;
                }
                // Guess was correct
                else
                {
                    Console.WriteLine("You guessed it!");
                    guess_count ++;
                    done = true;
                }
            }
            // Game over
            Console.WriteLine($"You got the magic number in {guess_count} guesses.");

            // Ask to play again
            Console.Write("Would you like to play again (y/n)? ");
        } while (Console.ReadLine().ToLower().Equals('y'));
    }
}