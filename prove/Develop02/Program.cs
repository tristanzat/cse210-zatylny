// How requirements were exceeded:
// File is automatically saved as a csv. Double quotes
// and commas within data are appropriately handled.
// Resulting csv file can be loaded in Excel without issue.
using System;

class Program
{
    static void Main(string[] args)
    {
        // Initialize journal
        Journal journal = new();

        // Run journal until user quits
        while (!journal._done)
        {
            journal.DisplayMenu();
        }
    }
}