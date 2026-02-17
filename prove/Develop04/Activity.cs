public class Activity
{
    // Member variables
    protected string _name;
    protected string _description;
    protected int _duration;
    protected DateTime _startTime;
    protected DateTime _endTime;

    // Constructor to set variables
    public Activity(string n, string d)
    {
        _name = n;
        _description = d;
        _duration = StartMessage();
    }

    // Message to start the activity
    protected static void StartActivity()
    {
        Console.Clear();
        Console.WriteLine("Get ready...");
        Pause(5);
    }

    // Message to end the activity
    protected void EndActivity()
    {
        Console.WriteLine("\nWell done!");
        Pause(5);
        Console.WriteLine($"\nYou have completed {_duration} seconds of the {_name} Activity.");
        Pause(5);
    }

    // Display starting message, return duration
    private int StartMessage()
    {
        // Common starting message
        Console.Write($"Welcome to the {_name} Activity.\n\n" +
        $"{_description}\n\n" +
        "How long, in seconds, would you like your activity to take? ");

        // User input to return
        return int.Parse(Console.ReadLine());
    }

    // Handles the pausing animation. Takes in duration in seconds
    protected static void Pause(int duration)
    {
        // Convert duration to milliseconds
        duration *= 1000;

        // Animation characters
        char[] animChars = ['|', '/', '-', '\\'];
        int frameIndex = 0;

        // First animation frame
        Console.Write(animChars[frameIndex]);

        for (int i = 0; i < duration; i += 200)
        {
            Thread.Sleep(200);
            
            // Move to next frame
            frameIndex = (frameIndex + 1) % 4;
            
            // Clear previous character and write new one
            Console.Write("\b \b");
            Console.Write(animChars[frameIndex]);
        }
        
        // Clear the animation character when done
        Console.Write("\b \b");
    }

    // Display a countdown (used in child classes)
    protected static void DisplayCountdown(int timer)
    {
        for (int i = timer; i > 0; i--)
        {
            Console.Write("\b \b");
            Console.Write(i);
            Thread.Sleep(1000);
        }
        Console.Write("\b \b");
    }
}