public class Word
{
    // Local variables
    private readonly string _word;
    private readonly string _hiddenWord;
    private bool _hidden;

    /// <summary>
    /// Initializes a new instance of the Word class given a string
    /// </summary>
    /// <param name="s">Word to store</param>
    public Word(string s)
    {
        _word = s;
        _hiddenWord = "";
        
        // Generate hidden word by making an underscore for each letter of the word
        for (int i = 0; i < s.Length; i++)
        {
            _hiddenWord += "_";
        }

        _hidden = false;
    }

    /// <summary>
    /// Sets condition that the word is hidden
    /// </summary>
    public void HideWord()
    {
        _hidden = true;
    }

    /// <summary>
    /// Displays the word either in hidden or not hidden form
    /// </summary>
    public void Display()
    {
        if (_hidden)
        {
            Console.Write(_hiddenWord);
        }
        else
        {
            Console.Write(_word);
        }
    }
}