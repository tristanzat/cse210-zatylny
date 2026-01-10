using System;
using System.Net;

class Program
{
    static void Main(string[] args)
    {
        // User input
        Console.Write("What is your grade percentage? ");
        int number_grade = int.Parse(Console.ReadLine());

        // Initialize letter grade
        string letter_grade = "";

        // Conditionals to assign letter grades
        if (number_grade >= 90)
        {
            letter_grade = "A";
        }
        else if (number_grade >= 80)
        {
            letter_grade = "B";
        }
        else if (number_grade >= 70)
        {
            letter_grade = "C";
        }
        else if (number_grade >= 60)
        {
            letter_grade = "D";
        }
        else
        {
            letter_grade = "F";
        }

        // Conditionals for + or - after grades
        int last_number = number_grade % 10;
        // Remove A+ and F+/F-
        if (number_grade < 95 && number_grade >= 60)
        {
            // - grade
            if (last_number <= 3)
            {
                letter_grade += "-";
            }
            // + grade
            else if (last_number >= 7)
            {
                letter_grade += "+";
            }
            else {} // doesn't need to be changed
        }

        Console.WriteLine(letter_grade);

        if (number_grade >= 70)
        {
            Console.WriteLine("You passed the class. Congratulations!");
        }
        else
        {
            Console.WriteLine("You failed the class. Growth comes through failure; keep going!");
        }
    }
}