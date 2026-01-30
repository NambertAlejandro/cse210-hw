using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> _entries;

    public Journal()
    {
        _entries = new List<Entry>();
    }

    public void AddEntry(Entry newEntry)
    {
        _entries.Add(newEntry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries yet.\n");
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string file)
    {
        // Format: date|energy|prompt|text
        // Using '|' as separator (per course simplification suggestion)
        using (StreamWriter outputFile = new StreamWriter(file))
        {
            foreach (Entry entry in _entries)
            {
                string safeDate = entry.GetDate().Replace("|", "/");
                string safePrompt = entry.GetPromptText().Replace("|", "/");
                string safeText = entry.GetEntryText().Replace("|", "/");
                int energy = entry.GetEnergyLevel();

                outputFile.WriteLine($"{safeDate}|{energy}|{safePrompt}|{safeText}");
            }
        }

        Console.WriteLine($"Journal saved to: {file}\n");
    }

    public void LoadFromFile(string file)
    {
        if (!File.Exists(file))
        {
            Console.WriteLine("File not found.\n");
            return;
        }

        string[] lines = File.ReadAllLines(file);

        _entries = new List<Entry>(); // replaces current entries (requirement)

        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length < 4) continue;

            string date = parts[0];

            int energy = 3;
            int.TryParse(parts[1], out energy);

            string prompt = parts[2];

            // In case text contains extra separators, join remaining parts
            string text = string.Join("|", parts, 3, parts.Length - 3);

            _entries.Add(new Entry(date, energy, prompt, text));
        }

        Console.WriteLine($"Journal loaded from: {file}\n");
    }
}
