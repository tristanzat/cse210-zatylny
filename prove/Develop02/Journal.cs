using Microsoft.VisualBasic.FileIO;
/// <summary>
/// Keeps track of journal entries and handles user input and files.
/// </summary>
public class Journal
{
    // Initialize member variables
    public List<Entry> _journal = [];
    public List<string> _prompts = [];
    public bool _done = false;

    /// <summary>
    /// Initialize a new instance of the Journal class that sets every prompt.
    /// </summary>
    public Journal()
    {
        // Add prompts to the list
        _prompts.Add("How have I seen the Lord's hand in my life today?");
        _prompts.Add("What is one thing I learned about myself today?");
        _prompts.Add("What is one thing I would do differently if I could redo today?");
        _prompts.Add("What habit would make my life 1% better this week?");
        _prompts.Add("Who made my life better this week?");
        _prompts.Add("When did I feel the Holy Ghost recently?");
        _prompts.Add("What pattern am I trying to rewrite, and what progress have I noticed?");
        _prompts.Add("What am I looking forward to in the next week?");
        _prompts.Add("What kind of person am I becoming, and what choices are shaping that?");
        _prompts.Add("What gospel principle have I recently understood in a deeper or more personal way?");
    }

    /// <summary>
    /// Handles displaying the user choice menu and handling the input. Any choice ≥ 5 will quit the program.
    /// </summary>
    public void DisplayMenu()
    {
        Console.Write("Please select one of the following choices:\n" +
        "1. Write\n" +
        "2. Display\n" +
        "3. Load\n" +
        "4. Save\n" +
        "5. Quit\n" +
        "What would you like to do? ");

        // Get user's choice
        int choice = int.Parse(Console.ReadLine());


        // Switch-case for handling the user's choice
        switch (choice)
        {
            // 1. Write
            case 1:
                _journal.Add(GeneratePrompt());
                break;
            // 2. Display
            case 2:
                DisplayJournal();
                break;
            // 3. Load file
            case 3:
                Console.WriteLine("What is the file name? (don't include file ending)");
                string filepath = Console.ReadLine();
                _journal = LoadFile(filepath);
                break;
            // 4. Save
            case 4:
                Console.WriteLine("What is the file name? (saved as a csv)");
                string filename = Console.ReadLine();
                SaveFile(filename);
                break;
            // 5 (or any other number). Exit
            default:
                _done = true;
                break;
        }
        
    }

    /// <summary>
    /// Generate a prompt from the list of prompts, get user's input, and return an Entry.
    /// </summary>
    /// <returns>Entry instance with the date, prompt, and user's input.</returns>
    public Entry GeneratePrompt()
    {
        // Initial variables for generating prompt, getting user input, and getting the date
        Random rand = new();
        string prompt = _prompts[rand.Next(_prompts.Count)];
        string userInput;
        string date = DateTime.Now.ToShortDateString();
        
        // Write to console for user input
        Console.WriteLine(prompt);
        Console.Write("> ");
        userInput = Console.ReadLine();

        // Generate and return Entry
        Entry entry = new(date, prompt, userInput);
        return entry;

    }

    /// <summary>
    /// Displays each entry in the journal separated by a new line.
    /// </summary>
    public void DisplayJournal()
    {
        foreach (Entry e in _journal)
        {
            e.Display();
            Console.Write("\n");
        }
    }

    /// <summary>
    /// Loads a csv file with the header date, prompt, entry.
    /// </summary>
    /// <param name="filePath">Path to the file.</param>
    /// <returns>A List of Entry instances.</returns>
    public static List<Entry> LoadFile(string filePath)
    {
        // Initial varaibles for use
        List<Entry> entries = [];
        Entry entry;
        filePath += ".csv";

        // Use a using statement to ensure the parser is disposed correctly
        using TextFieldParser parser = new(filePath);

        // Stuff to handle the fact that the file is a csv
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(","); // Set the delimiter
        parser.HasFieldsEnclosedInQuotes = true; // Handle fields with commas and quotes

        // Loop through all the lines in the file
        while (!parser.EndOfData)
        {
            try
            {
                // Read all fields on the current line
                string[] fields = parser.ReadFields();

                // Skip header line (I don't know why it's 2)
                if (!(parser.LineNumber == 2))
                {
                    // Process fields by making them an entry for the list
                    // [0] = date, [1] = prompt, [2] = entry
                    entry = new(fields[0], fields[1], fields[2]);
                    entries.Add(entry);
                }
            }
            catch (MalformedLineException ex)
            {
                Console.WriteLine($"Error parsing line {ex.LineNumber}: {ex.Message}");
            }
        }

        return entries;
    }

    public void SaveFile(string fileName)
    {
        // Save file as csv
        fileName += ".csv";

        // Create the writer
        using StreamWriter writer = new(fileName, false);
        
        // Write the header row
        writer.WriteLine("date,prompt,entry");

        // Write data rows
        foreach (Entry entry in _journal)
        {
            // Make string out of Entry member variables
            string line = entry._date + ",";

            // See if prompt contains a comma and handle appropriately
            if (entry._prompt.Contains(','))
            {
                line = line + "\"" + entry._prompt + "\",";
            }
            else
            {
                line = line + entry._prompt + ",";
            }

            // See if entry contains quotation marks and handle appropriately
            if (entry._entry.Contains('\"'))
            {
                string newEntry = entry._entry;
                List<int> indexes = [];
                for (int i = 0; i < entry._entry.Length; i++)
                {
                    if (entry._entry[i].Equals('\"'))
                    {
                        indexes.Add(i);
                    }
                }
                
                for (int i = indexes.Count-1; i >= 0; i--)
                {
                    newEntry = newEntry.Insert(indexes[i], "\"");
                }

                line = line + "\"" + newEntry + "\",";
            }
            // See if entry contains a comma but not quotation marks and handle appropriately
            else if (entry._entry.Contains(','))
            {
                line = line + "\"" + entry._entry + "\",";
            }
            else
            {
                line = line + entry._entry + ",";
            }

            // Write line to csv
            writer.WriteLine(line);
        }
    }
}