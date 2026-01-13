using System;

class Program
{
    static void Main(string[] args)
    {
        // Initialize variables for later use
        string name;
        double number;
        int year;

        // Call all of the functions
        DisplayWelcome();
        name = PromptUserName();
        number = PromptUserNumber();
        year = PromptUserBirthYear();
        DisplayResult(name, SquareNumber(number), year);
    }

    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the program!");
    }

    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        return Console.ReadLine();
    }

    static double PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        return double.Parse(Console.ReadLine());
    }

    static int PromptUserBirthYear()
    {
        Console.Write("Please enter the year you were born: ");
        return int.Parse(Console.ReadLine());   
    }

    static double SquareNumber(double number)
    {
        return Math.Pow(number, 2);
    }

    static void DisplayResult(string name, double number, int year)
    {
        Console.WriteLine($"{name}, the square of your number is {number}");
        Console.WriteLine($"{name}, you will turn {2026 - year} this year.");
    }
}