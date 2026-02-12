using System;

class Program
{
    static void Main(string[] args)
    {
        MathAssignment mathAssignment = new("Roberto Rodriguez", "Fractions", "7.3", "8-19");
        WritingAssignment writingAssignment = new("Mary Waters", "European History", "The Causes of World War II");

        Console.WriteLine(mathAssignment.GetSummary() + "\n" + mathAssignment.GetHomeworkList());
        Console.WriteLine(writingAssignment.GetSummary() + "\n" + writingAssignment.GetWritingInformation());
    }
}