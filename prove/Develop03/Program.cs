using System;
using System.Xml.Serialization;

class Program
{
    static void Main(string[] args)
    {
        Scripture scripture = new("Moses", 1, 39, ["For", "behold", "this", "is", "my", "work", "and", "my", "glory—","to","bring","to","pass","the","immortality","and","eternal","life","of","man."]);
        bool done = false;
        while(!done)
        {
            scripture.Display();
            Console.Write("\n\nEnter or quit: ");
            string choice = Console.ReadLine();

            scripture.HideWords();

            if (choice.Equals("quit"))
            {
                done = true;
            }
        }
    }
}