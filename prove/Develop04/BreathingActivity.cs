public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.") {}

    public void Run()
    {
        _startTime = DateTime.Now;
        _endTime = _startTime.AddSeconds(_duration);

        // Starting message
        StartActivity();

        // Start the breathing activity
        while (DateTime.Now < _endTime)
        {
            // Breathe in for 4 seconds
            Console.Write("\nBreathe in...  ");
            DisplayCountdown(4);

            // Breathe out for 4 seconds
            Console.Write("\nBreathe out...  ");
            DisplayCountdown(4);
            
            // Newline for space
            Console.WriteLine();
        }

        // Ending message
        EndActivity();
    }
}