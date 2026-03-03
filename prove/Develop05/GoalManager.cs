using Microsoft.VisualBasic.FileIO;

public class GoalManager
{
    // member variables
    private List<Goal> _goals;
    private int _level;
    private int _score;
    private int _scoreToLevel;
    private string _userName;

    // base constructor
    public GoalManager()
    {
        _goals = [];
        _score = 0;
        CheckLevel(1);
        _userName = "";
    }

    public void SetName(string n)
    {
        _userName = n;
    }

    // menu loop
    public void Start()
    {
        Console.Write($"Welcome, {_userName}!");

        bool done = false;
        
        while (!done)
        {
            // display menu
            Console.Write("Choose an option:\n" +
                "\t1. Display goals\n" +
                "\t2. Display your information\n" +
                "\t3. Record progress toward a goal\n" +
                "\t4. Create a new goal\n" +
                "\t5. Save game\n" +
                "\t6. Quit\n" + 
                "> ");
            
            int choice = int.Parse(Console.ReadLine());

            Console.Clear();

            switch (choice)
            {
                // display goals
                case 1:
                    Console.Write(ListGoals());
                    break;
                
                // diplay user info
                case 2:
                    Console.WriteLine(GetPlayerInfo());
                    break;
                
                // record progress
                case 3:
                    if (_goals.Count > 0)
                    {
                        Console.WriteLine("Which goal would you like to record progress toward?");
                        for (int i = 0; i < _goals.Count; i++)
                        {
                            Console.Write($"{i+1}. {_goals[i].GetDetails()}");
                        }
                        Console.Write("> ");

                        int goalChoice = int.Parse(Console.ReadLine());
                        goalChoice --;

                        _goals[goalChoice].RecordEvent();
                        Console.WriteLine(_goals[goalChoice].GetFullDetails());
                        CheckLevel(_level);
                    }
                    else
                    {
                        Console.Write(ListGoals());
                    }
                    break;
                
                // create goal
                case 4:
                    CreateGoal();
                    Console.WriteLine("Goal created!");
                    break;
                
                // save
                case 5:
                    if (_goals.Count == 0)
                    {
                        Console.WriteLine("No data to save!");
                    }
                    else
                    {
                        Console.Write("Name your save (no file ending).\n> ");
                        SaveFile(Console.ReadLine());
                        Console.WriteLine("Game saved successfully!");
                    }
                    break;
                
                // quit
                default:
                    done = true;
                    break;
            }

            Console.Clear();
        }
    }

    // player info
    public string GetPlayerInfo()
    {
        return $"{_userName} | Level {_level} | Lifetime points: {_score} | Points to next level: {_scoreToLevel}";
    }

    // handle levels
    private void CheckLevel(int level)
    {
        // Set level and the score to next level
        _level = (_score % 1000) + 1;
        _scoreToLevel = _level * 1000;

        // Make score to next level take into account current points
        int subtractScore = _score;
        for (int i = 0; i < _level; i++)
        {
            subtractScore -= i * 1000;
        }

        _scoreToLevel -= subtractScore;

        // Display level up message
        if (_level > level)
        {
            Console.WriteLine($"Level up! {level} -> {_level}");
            Console.WriteLine($"Points until next level: {_scoreToLevel}");
        }
    }

    // list goals
    public string ListGoals()
    {
        string output = "";
        if (_goals.Count == 0)
        {
            output = "No goals found. Create a goal to see it here.\n";
        }
        else
        {
            foreach (Goal goal in _goals)
            {
                output += goal.GetFullDetails() + "\n";
            }
        }

        return output;
    }

    // create goal
    public Goal CreateGoal()
    {
        // Choosing goal type
        bool chosen = false;
        int choice = 0;

        // Goal instantiation
        string n, d;
        int p;

        while (!chosen)
        {
            // display goal creating menu
            Console.Write("Choose a goal to create.\n" +
                    "\t1. Simple goal (marked off once)\n" +
                    "\t2. Eternal goal (never complete)\n" +
                    "\t3. Checklist goal (marked many times)\n" +
                    "> ");
            
            choice = int.Parse(Console.ReadLine());
            
            // choice is a real choice
            if (choice > 0 && choice < 4)
            {
                chosen = true;
            }

            // choice isn't an option
            else
            {
                Console.WriteLine("Invalid choice. Please input a number corresponding to a menu option.");
            }
        }

        Console.Clear();
        
        // switch based on choice
        switch (choice)
        {
            // Simple goal
            case 1:
                Console.Write("Simple goal.\nEnter goal name, description, and point value.\n" +
                "Name: ");
                n = Console.ReadLine();
                Console.Write("Description: ");
                d = Console.ReadLine();
                Console.Write("Point value: ");
                p = int.Parse(Console.ReadLine());
                
                return new SimpleGoal(n, d, p);

            // Eternal goal
            case 2:
                Console.Write("Eternal goal.\nEnter goal name, description, and points per completion.\n" +
                "Name: ");
                n = Console.ReadLine();
                Console.Write("Description: ");
                d = Console.ReadLine();
                Console.Write("Points per completion: ");
                p = int.Parse(Console.ReadLine());
                
                return new EternalGoal(n, d, p);
            
            // Checklist goal
            case 3:
                Console.Write("Checklist goal.\nEnter goal name, description, point value, and target completion value.\n" +
                "Name: ");
                n = Console.ReadLine();
                Console.Write("Description: ");
                d = Console.ReadLine();
                Console.Write("Point value: ");
                p = int.Parse(Console.ReadLine());
                Console.Write("Marks until completion: ");
                int t = int.Parse(Console.ReadLine());

                if (t < 1)
                {
                    Console.WriteLine("Goal must have a positive, non-zero target. Setting to 1.");
                    t = 1;
                }
                
                return new ChecklistGoal(n, d, p, t);

            // Uh oh, make an error simple goal
            default:
                return new SimpleGoal("error", "error", 0);
        }

    }

    // record event
    public void RecordEvent(Goal goal)
    {
        goal.RecordEvent();
        _score += goal.GetPoints();
    }

    // save to file
    public void SaveFile(string fileName)
    {
        // Save file as txt
        fileName += ".txt";

        // Create the writer
        using StreamWriter writer = new(fileName, false);
        
        // write user info
        writer.WriteLine($"{_userName},{_level},{_score}");

        // Write the header row
        writer.WriteLine("date,prompt,entry");

        // Write goal data
        foreach (Goal goal in _goals)
        {
            // Write line to file
            writer.WriteLine(goal.ToString());
        }
    }

    // load from file
    public void LoadFile(string fileName)
    {
        // Expect a txt file
        fileName += ".txt";

        // Use a using statement to ensure the parser is disposed correctly
        using TextFieldParser parser = new(fileName);

        // File is delimited
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(","); // Delimiter is comma

        // Loop through all the lines in the file
        while (!parser.EndOfData)
        {
            try
            {
                // Read all fields on the current line
                string[] fields = parser.ReadFields();

                // Header line is user information
                if (parser.LineNumber == 1)
                {
                    _userName = fields[0];
                    _level = int.Parse(fields[1]);
                    _score = int.Parse(fields[2]);
                }

                // Goal information
                else
                {
                    // first item is type:name, so split it on :
                    string goalType = fields[0].Split(':')[0];
                    string name = fields[0].Split(':')[1];
                    
                    // second item is description, third is point value
                    string description = fields[1];
                    int points = int.Parse(fields[2]);
                    
                    // additional items depend on goal type
                    switch (goalType)
                    {
                        case "SimpleGoal":
                            // fourth item is whether or not it is complete
                            _goals.Add(new SimpleGoal(name, description, points, bool.Parse(fields[3])));
                            break;
                        case "EternalGoal":
                            // fourth item is how many times it has been marked
                            _goals.Add(new EternalGoal(name, description, points, int.Parse(fields[3])));
                            break;
                        case "ChecklistGoal":
                            // fourth item is target, fifth is number of times marked
                            _goals.Add(new ChecklistGoal(name, description, points, int.Parse(fields[3]), int.Parse(fields[4])));
                            break;
                        default:
                            break;
                    }      
                }
            }
            catch (MalformedLineException ex)
            {
                Console.WriteLine($"Error parsing line {ex.LineNumber}: {ex.Message}");
            }
        }

        // Make sure values are correct
        CheckLevel(_level);
    }

}