/// <summary>
/// Represents a reference to a scripture.
/// </summary>
public class Reference
{
    // Local variables
    private readonly string _book;
    private readonly int _chapter;
    private readonly List<int> _verses;

    /// <summary>
    /// Initializes a new instance of the Reference class with a book, chapter, and single verse
    /// </summary>
    /// <param name="b">Book of scripture</param>
    /// <param name="c">Chapter of book</param>
    /// <param name="v">Verse of scripture</param>
    public Reference(string b, int c, int v)
    {
        _book = b;
        _chapter = c;
        _verses = [v];
    }

    /// <summary>
    /// Initializes a new instance of the Reference class with a book, chapter, and multiple verses
    /// </summary>
    /// <param name="b">Book of scripture</param>
    /// <param name="c">Chapter of book</param>
    /// <param name="ints">List of verse numbers</param>
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

    /// <summary>
    /// Displays the reference in the format {book} {chapter}:{verse}(-{verse} if more than 1 verse)
    /// </summary>
    public void Display()
    {
        // Display in multiple verse format if there are many verses
        if (_verses.Count > 1)
        {
            Console.Write($"{_book} {_chapter}:{_verses[0]}–{_verses[^1]}");
        }

        // Display in single verse format if there is one verse
        else
        {
            Console.Write($"{_book} {_chapter}:{_verses[0]}");
        }
    }
}