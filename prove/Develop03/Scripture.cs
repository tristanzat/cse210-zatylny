/// <summary>
/// Represents a scripture to memorize, containing a reference and the words of the scirpture.
/// </summary>
public class Scripture
{
    // Instantiate local variables
    private readonly Reference _reference;
    private readonly List<Word> _scripture;
    private readonly List<int> _hiddenWords;

    /// <summary>
    /// Creates a new instance of the Scripture class referencing John 11:35.
    /// </summary>
    public Scripture()
    {
        _reference = new("John", 11, 35);
        _scripture = [new Word("Jesus"), new Word("wept.")];
        _hiddenWords = [];
    }

    /// <summary>
    /// Create a new instance of the Scripture class with a Reference and a List of Words.
    /// </summary>
    /// <param name="r">Reference class containing scripture reference</param>
    /// <param name="words">List containing Word instances</param>
    public Scripture(Reference r, List<Word> words)
    {
        _reference = r;
        _scripture = words;
        _hiddenWords = [];
    }

    /// <summary>
    /// Create a new instance of the Scripture class given book, chapter, verse, and a List of Words.
    /// </summary>
    /// <param name="b">Book of scripture</param>
    /// <param name="c">Chapter of verse</param>
    /// <param name="v">Verse of scripture</param>
    /// <param name="words">List containing Word instances</param>
    public Scripture(string b, int c, int v, List<Word> words)
    {
        _reference = new(b, c, v);
        _scripture = words;
        _hiddenWords = [];
    }

    /// <summary>
    /// Create a new instance of the Scripture class given book, chapter, verse, and List of strings for the words.
    /// </summary>
    /// <param name="b">Book of scripture</param>
    /// <param name="c">Chapter of verse</param>
    /// <param name="v">Verse of scripture</param>
    /// <param name="strings">Words of scripture as a List</param>
    public Scripture(string b, int c, int v, List<string> strings)
    {
        _reference = new(b, c, v);
        _scripture = [];
        
        foreach (string s in strings)
        {
            Word word = new(s);
            _scripture.Add(word);
        }

        _hiddenWords = [];
    }

    /// <summary>
    /// Create a new instance of the Scripture class given book, chapter, verses, and List of strings for the words.
    /// </summary>
    /// <param name="b">Book of scripture</param>
    /// <param name="c">Chapter of verse</param>
    /// <param name="ints">Verses of scripture as a List</param>
    /// <param name="strings">Words of scripture as a List</param>
    public Scripture(string b, int c, List<int> ints, List<string> strings)
    {
        _reference = new(b, c, ints);
        _scripture = [];
        
        foreach (string s in strings)
        {
            Word word = new(s);
            _scripture.Add(word);
        }

        _hiddenWords = [];
    }

    /// <summary>
    /// Displays the scripture in the format {reference} {words}.
    /// </summary>
    public void Display()
    {
        _reference.Display();

        foreach (Word word in _scripture)
        {
            Console.Write(" ");
            word.Display();
        }
    }

    /// <summary>
    /// Hides a random number of words of the scripture based on verse length.
    /// </summary>
    public void HideWords()
    {
        // Variables to use in loop
        Random random = new();
        int numWordsToHide = (_scripture.Count / 10) + 1;

        // Loop for hiding words
        for (int i = 0; i < numWordsToHide; i++)
        {
            // Check to see if the scripture is already fully hidden. If so, end the for loop
            if (FullyHidden())
            {
                i = numWordsToHide;
            }

            // Scripture isn't fully hidden, hide words
            else
            {
                // Get an index of a random word to hide
                int hideIndex = random.Next(_scripture.Count);

                // See if that word has already been hidden. If so, loop until word hasn't been hidden
                bool continueLoop = _hiddenWords.Contains(hideIndex);
                while (continueLoop)
                {
                    hideIndex = random.Next(_scripture.Count);
                    continueLoop = _hiddenWords.Contains(hideIndex);
                }

                // Hide word at chosen random index
                _scripture[hideIndex].HideWord();

                // Add index to list to ensure word can't be hidden more than once
                _hiddenWords.Add(hideIndex);
            }
        }

    }

    /// <summary>
    /// Check if the scripture is fully hidden.
    /// </summary>
    /// <returns>Boolean containing whether or not the scirpture is fully hidden</returns>
    public bool FullyHidden()
    {
        return _hiddenWords.Count == _scripture.Count;
    }
}