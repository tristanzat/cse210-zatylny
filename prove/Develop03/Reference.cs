public class Reference
{
    // Variables
    private readonly string _book;
    private readonly int _chapter;
    private readonly List<int> _verses;

    public Reference(string b, int c, int v)
    {
        _book = b;
        _chapter = c;
        _verses = [v];
    }

    public Reference(string b, int c, List<int> ints)
    {
        _book = b;
        _chapter = c;
        _verses = [];
        foreach (int num in ints)
        {
            _verses.Add(num);
        }
    }

    public void Display()
    {
        if (_verses.Count > 1)
        {
            Console.Write($"{_book} {_chapter}:{_verses[0]}–{_verses[^1]}");
        }
        else
        {
            Console.Write($"{_book} {_chapter}:{_verses[0]}");
        }
    }
}