using System;
using System.Runtime.Intrinsics.X86;

class Program
{
    static void Main(string[] args)
    {
        // Starting print
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        // Instantiate variables
        List<int> num_list = new List<int>();
        int user_num;

        // Start user input before looping
        Console.Write("Enter number: ");
        user_num = int.Parse(Console.ReadLine());

        // Start user input
        while (user_num != 0)
        {
            // Append user number to list
            num_list.Add(user_num);

            // Get more input
            Console.Write("Enter number: ");
            user_num = int.Parse(Console.ReadLine());
        }

        // Output sum, average, and highst number
        Console.WriteLine($"The sum is: {num_list.Sum()}");
        Console.WriteLine($"The average is: {num_list.Average()}");
        Console.WriteLine($"The largest number is: {num_list.Max()}");

        // New list without negatives to find smallest positive
        List<int> pos_list = new List<int>(num_list);
        
        // Loop through, setting negative numbers as the maximum number
        for (int i = 0; i < pos_list.Count(); i++)
        {
            if (pos_list[i] < 0) { pos_list[i] = pos_list.Max(); }
        }

        // Output smallest positive
        Console.WriteLine($"The smallest positive number is: {pos_list.Min()}");

        // Sort original list
        num_list.Sort();

        Console.WriteLine("The sorted list is:");
        // For loop for output
        for (int i = 0; i < num_list.Count(); i++)
        {
            Console.WriteLine(num_list[i]);
        }
    }
}