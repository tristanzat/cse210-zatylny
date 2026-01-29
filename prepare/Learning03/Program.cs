using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new();

        for(int i = 0; i < 20; i++)
        {
            int top = random.Next(1, 10);
            int bottom = random.Next(1, 10);

            Fraction fraction = new(top, bottom);

            Console.WriteLine($"Fraction {i+1}: string: {fraction.GetFractionString()} Number: {fraction.GetDecimalValue()}");
        }
    }
}