public class PromptActivity : Activity
{
    private readonly List<string> _prompts;
    private static readonly List<int> _chosenPrompts = [];
    // Boolean is true if list activity, false if reflection activity
    private readonly bool _listingMode;

    public PromptActivity(string n, string d, List<string> p, bool l) : base(n, d)
    {
        _prompts = p;
        _listingMode = l;
    }

    public void Run()
    {
        Random random = new();
        int promptNumber = random.Next(_prompts.Count);
        bool duplicate = true;
        
        while (duplicate)
        {
            // See if it isn't a duplicate
            if (!_chosenPrompts.Contains(promptNumber))
            {
                _chosenPrompts.Add(promptNumber);
                duplicate = false;
            }
            // There is a duplicate, see if every prompt has been used already & clear list if so
            else if (_chosenPrompts.Count == _prompts.Count)
            {
                _chosenPrompts.Clear();
                _chosenPrompts.Add(promptNumber);
                duplicate = false;
            }
            // Regenerate number and try again
            else
            {
                promptNumber = random.Next(_prompts.Count);
            }
        }

        string prompt = _prompts[promptNumber];

        StartActivity();
        if (_listingMode) // Listing activity
        {
            List<string> responses = [];

            Console.Write("List as many responses as you can to the following prompt:\n" +
            $" --- {prompt} ---\n" +
            "You may begin in:  ");
            DisplayCountdown(5);

            // Space for user input time
            Console.WriteLine();

            _startTime = DateTime.Now;
            _endTime = _startTime.AddSeconds(_duration);

            while (DateTime.Now < _endTime)
            {
                Console.Write("> ");
                responses.Add(Console.ReadLine());
            }

            Console.WriteLine($"You listed {responses.Count} items!");
        }
        else // Reflection activity
        {
            List<string> reflectQuestions = ["Why was this experience meaningful to you?", "Have you ever done anything like this before?", "How did you get started?", "How did you feel when it was complete?", "What made this time different than other times when you were not as successful?", "What is your favorite thing about this experience?", "What could you learn from this experience that applies to other situations?", "What did you learn about yourself through this experience?", "How can you keep this experience in mind in the future?"];
            List<int> chosenQuestions = [];
            int questionNumber = random.Next(reflectQuestions.Count);

            Console.WriteLine("Consider the following prompt:\n\n" +
            $" --- {prompt} ---\n\n" +
            "When you have something in mind, press enter to continue.");
            
            // Wait for user to press enter
            Console.ReadLine();

            Console.Write("Now ponder on each of the following questions as they related to this experience.\n" +
            "You may begin in:  ");
            DisplayCountdown(5);

            Console.Clear();

            _startTime = DateTime.Now;
            _endTime = _startTime.AddSeconds(_duration);

            while(DateTime.Now < _endTime)
            {
                // Choose a prompt, but make sure it's not a duplicate
                duplicate = true;
                while (duplicate)
                {
                    // See if it isn't a duplicate
                    if (!chosenQuestions.Contains(questionNumber))
                    {
                        chosenQuestions.Add(questionNumber);
                        duplicate = false;
                    }
                    // There is a duplicate, see if every question has been asked already & clear list if so
                    else if (chosenQuestions.Count == reflectQuestions.Count)
                    {
                        chosenQuestions.Clear();
                        chosenQuestions.Add(questionNumber);
                        duplicate = false;
                    }
                    // Regenerate number and try again
                    else
                    {
                        questionNumber = random.Next(reflectQuestions.Count);
                    }
                }

                // Display question and spinner
                Console.Write($"> {reflectQuestions[questionNumber]} ");
                Pause(10);
                Console.WriteLine();
            }
        }
        
        EndActivity();
    }
}