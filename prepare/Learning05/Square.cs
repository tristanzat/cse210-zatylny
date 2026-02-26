public class Square : Shape
{
    private double _side;

    public Square(double s, string c) : base(c)
    {
        _side = s;
    }

    public override double GetArea()
    {
        return _side * _side;
    }
}