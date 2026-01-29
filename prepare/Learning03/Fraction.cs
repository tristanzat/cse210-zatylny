/// <summary>
/// Fraction class that holds a top and bottom number of a fraction
/// </summary>
public class Fraction
{
    private int _top;
    private int _bottom;

    /// <summary>
    /// Default constructor that initializes the fraction to 1/1
    /// </summary>
    public Fraction()
    {
        SetTop(1);
        SetBottom(1);
    }

    public Fraction(int t)
    {
        SetTop(t);
        SetBottom(1);
    }

    public Fraction(int t, int b)
    {
        SetTop(t);
        SetBottom(b);
    }

    public void SetTop(int t)
    {
        _top = t;
    }

    public int GetTop()
    {
        return _top;
    }

    public void SetBottom(int b)
    {
        _bottom = b;
    }

    public int GetBottom()
    {
        return _bottom;
    }

    public string GetFractionString()
    {
        return GetTop().ToString() + "/" + GetBottom().ToString();
    }

    public double GetDecimalValue()
    {
        // Cast top number to double so it returns a number
        double top = (double)GetTop();
        return top/GetBottom();
    }
}