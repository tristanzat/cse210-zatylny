using Microsoft.VisualBasic.FileIO;

/// <summary>
/// Class that handles storing scriptures and selecting them at random
/// </summary>
public class ScriptureLibrary
{
    // Local variables
    private readonly List<Scripture> _scriptures;

    /// <summary>
    /// Initializes a new instance of the ScriptureLibrary class
    /// </summary>
    public ScriptureLibrary()
    {
        _scriptures = ParseScriptures();
    }

    /// <summary>
    /// Parses the scripture csv as a List of Scripture classes
    /// </summary>
    /// <returns>A list of Scripture classes</returns>
    private static List<Scripture> ParseScriptures()
    {
        List<Scripture> scriptures = [];
        Scripture scripture;
        string filePath = "lds-scriptures.csv";

        // Create csv parser
        using TextFieldParser parser = new(filePath);

        // Make parser built for csv
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        parser.HasFieldsEnclosedInQuotes = true;

        while (!parser.EndOfData)
        {
            try
            {
                // Skip header line
                if (!(parser.LineNumber == 1))
                {
                    // Read all fields on the current line
                    string[] fields = parser.ReadFields();

                    // Important fields: 5 - book, 14 - chapter, 15 - verse number, 16 - verse text
                    string book = fields[5];
                    int chapter = int.Parse(fields[14]);
                    int verse = int.Parse(fields[15]);
                    string verseRaw = fields[16];

                    // Convert verse text to List to be passed in to scripture
                    List<string> verseText = [.. verseRaw.Split(' ')];
                
                    // Process fields by making them a scripture for the list
                    scripture = new(book, chapter, verse, verseText);
                    scriptures.Add(scripture);
                }
                else
                {
                    // Discard header line
                    parser.ReadLine();
                }
            }
            catch (MalformedLineException ex)
            {
                Console.WriteLine($"Error parsing line {ex.LineNumber}: {ex.Message}");
            }
        }

        return scriptures;
    }

    /// <summary>
    /// Generates a random scripture from the list of all scriptures
    /// </summary>
    /// <returns>A random Scripture instance</returns>
    public Scripture GetRandomScripture()
    {
        Random random = new();

        return _scriptures[random.Next(_scriptures.Count)];
    }
}