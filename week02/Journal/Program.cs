using System;

/*
CREATIVITY / EXCEEDS REQUIREMENTS:
In addition to the core requirements, this program records the user's daily
energy level (1–5) for each journal entry. This helps reduce the pressure
to write long entries and allows users to reflect on patterns of energy
over time, addressing a common barrier to consistent journaling.
*/

class Program
{
    static void Main()
    {
        Journal journal = new Journal();
        PromptGenerator generator = new PromptGenerator();

        while (true)
        {
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Save");
            Console.WriteLine("4. Load");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1")
            {
                string prompt = generator.GetRandomPrompt();
                Console.WriteLine(prompt);
                Console.Write("> ");
                string response = Console.ReadLine();

                int energyLevel = ReadEnergyLevel();

                string dateText = DateTime.Now.ToShortDateString();

                Entry entry = new Entry(dateText, energyLevel, prompt, response);
                journal.AddEntry(entry);

                Console.WriteLine("Entry added.\n");
            }
            else if (choice == "2")
            {
                journal.DisplayAll();
            }
            else if (choice == "3")
            {
                Console.Write("Enter filename to save (example: journal.txt): ");
                string filename = Console.ReadLine();
                Console.WriteLine();

                journal.SaveToFile(filename);
            }
            else if (choice == "4")
            {
                Console.Write("Enter filename to load: ");
                string filename = Console.ReadLine();
                Console.WriteLine();

                journal.LoadFromFile(filename);
            }
            else if (choice == "5")
            {
                Console.WriteLine("Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Try again.\n");
            }
        }
    }

    private static int ReadEnergyLevel()
    {
        while (true)
        {
            Console.Write("Energy level today (1-5): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int level) && level >= 1 && level <= 5)
            {
                Console.WriteLine();
                return level;
            }

            Console.WriteLine("Please enter a number from 1 to 5.\n");
        }
    }
}
