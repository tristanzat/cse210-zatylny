using System.Data;
using System.Runtime.InteropServices;

public class Scripture
{
    // Variables
    private Reference _reference;
    private List<Word> _scripture;
    private List<int> _hiddenWords;

    public Scripture(Reference r, List<Word> words)
    {
        _reference = r;
        _scripture = words;
        _hiddenWords = [];
    }

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

    public void Display()
    {
        _reference.Display();

        foreach (Word word in _scripture)
        {
            Console.Write(" ");
            word.Display();
        }
    }

    public void HideWords()
    {
        Random random = new();
        int numWordsToHide = (_scripture.Count / 10) + 1;
        bool continueFunction = true;

        for (int i = 0; i < numWordsToHide; i++)
        {
            if (continueFunction)
            {
                int j = 0;
                int hideIndex = random.Next(_scripture.Count);
                bool continueLoop = _hiddenWords.Contains(hideIndex);
                while (continueLoop)
                {
                    hideIndex = random.Next(_scripture.Count);
                    continueLoop = _hiddenWords.Contains(hideIndex);
                    j++;
                    if (j > 100)
                    {
                        continueLoop = false;
                    }
                }

                _scripture[hideIndex].HideWord();
                _hiddenWords.Add(hideIndex);
            }
            
            if (_scripture.Count == _hiddenWords.Count)
            {
                i = numWordsToHide;
                continueFunction = false;
            }
        }

    }
}