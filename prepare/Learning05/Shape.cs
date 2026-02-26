public abstract class Shape
{
    // member variable
    private string _color;

    public Shape(string c)
    {
        _color = c;
    }

    // methods
    public string GetColor() { return _color; }
    public void SetColor(string color) { _color = color; }

    // abstract method to be overwritten
    public abstract double GetArea();
}