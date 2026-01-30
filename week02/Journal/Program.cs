using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Journal stubs running...");

        Journal journal = new Journal();
        PromptGenerator generator = new PromptGenerator();

        string prompt = generator.GetRandomPrompt();
        Entry entry = new Entry("2026-01-30", prompt, "Sample journal entry.");

        journal.AddEntry(entry);
        journal.DisplayAll();
    }
}
