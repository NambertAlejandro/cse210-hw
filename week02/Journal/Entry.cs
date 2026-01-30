using System;

public class Entry
{
    private string _date;
    private int _energyLevel;      // Creativity: 1–5
    private string _promptText;
    private string _entryText;

    public Entry(string date, int energyLevel, string promptText, string entryText)
    {
        _date = date;
        _energyLevel = energyLevel;
        _promptText = promptText;
        _entryText = entryText;
    }

    public string GetDate() => _date;
    public int GetEnergyLevel() => _energyLevel;
    public string GetPromptText() => _promptText;
    public string GetEntryText() => _entryText;

    public void Display()
    {
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Energy Level: {_energyLevel}/5");
        Console.WriteLine($"Prompt: {_promptText}");
        Console.WriteLine(_entryText);
        Console.WriteLine();
    }
}
