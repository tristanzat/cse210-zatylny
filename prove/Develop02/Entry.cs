/// <summary>
/// Handles storing and displaying of journal entries.
/// </summary>
public class Entry
{
    // Initialize member variables
    public string _date;
    public string _prompt;
    public string _entry;

    /// <summary>
    /// Initializes a new instance of the Entry class that is empty.
    /// </summary>
    public Entry()
    {        
    }

    /// <summary>
    /// Initializes a new instance of the Entry class that sets member vairables.
    /// </summary>
    /// <param name="d">Date of entry.</param>
    /// <param name="p">Prompt given to the user.</param>
    /// <param name="e">User's text entry.</param>
    public Entry(string d, string p, string e)
    {
        // Use inputs to set local variables
        _date = d;
        _prompt = p;
        _entry = e;
    }

    /// <summary>
    /// Displays member variables in the format:
    /// Date: {_date} — Prompt: {_prompt}
    /// {_entry}
    /// </summary>
    public void Display()
    {
        Console.WriteLine($"Date: {_date} — Prompt: {_prompt}\n{_entry}");
    }
}