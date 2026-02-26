public class Circle : Shape
{
    private double _radius;

    public Circle(double r, string c): base(c)
    {
        _radius = r;
    }

    public override double GetArea()
    {
        return Math.PI * _radius * _radius;
    }
}