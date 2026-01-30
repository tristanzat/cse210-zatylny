public class Word
{
    // Variables
    private readonly string _word;
    private readonly string _hiddenWord;
    private bool _hidden;

    public Word(string s)
    {
        _word = s;
        _hiddenWord = "";
        
        for(int i = 0; i < s.Length; i++)
        {
            _hiddenWord += "_";
        }

        _hidden = false;
    }

    public void HideWord()
    {
        _hidden = true;
    }

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