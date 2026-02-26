using System;

class Program
{
    static void Main(string[] args)
    {
        List<Shape> shapes =[];
        shapes.Add(new Square(4.0, "blue"));
        shapes.Add(new Rectangle(4.0, 6.0, "red"));
        shapes.Add(new Circle(3.0, "green"));

        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Shape - color: {shape.GetColor()}, area: {shape.GetArea()}");
        }
    }
}