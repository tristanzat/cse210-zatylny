/* How this program exceeds requirements:
In prompt activities, prompts that have already been used in that session are not chosen until all
have been gone through. In a reflection activity, questions cannot be repeated for the duration of
that reflection session unless needed.
*/

class Program
{
    static void Main(string[] args)
    {
        string reflectName = "Reflection";
        string reflectDesc = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
        List<string> reflectPrompts = ["Think of a time when you stood up for someone else.", "Think of a time when you did something really difficult.", "Think of a time when you helped someone in need.", "Think of a time when you did something truly selfless."];
        string listName = "Listing";
        string listDesc = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
        List<string> listPrompts = ["Who are people that you appreciate?", "What are personal strengths of yours?", "Who are people that you have helped this week?", "When have you felt the Holy Ghost this month?", "Who are some of your personal heroes?"];

        bool done = false;

        while (!done)
        {
            Console.Clear();

            Console.Write("Menu Options:\n" +
            "  1. Start breathing activity\n" +
            "  2. Start reflecting activity\n" +
            "  3. Start listing activity\n" +
            "  4. Quit\n" +
            "Select a choice from the menu: ");

            int choice = int.Parse(Console.ReadLine());
            
            Console.Clear();

            switch (choice)
            {
                case 1:
                    BreathingActivity breathingActivity = new();
                    breathingActivity.Run();
                    break;
                case 2:
                    PromptActivity reflectActivity = new(reflectName, reflectDesc, reflectPrompts, false);
                    reflectActivity.Run();
                    break;
                case 3:
                    PromptActivity listActivity = new(listName, listDesc, listPrompts, true);
                    listActivity.Run();
                    break;
                default:
                    done = true;
                    break;
            }
        }
    }

}