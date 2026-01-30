using System;
using System.Collections.Generic;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();

        // Basic split into words (good enough for stub + builds)
        string[] parts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (string p in parts)
        {
            _words.Add(new Word(p));
        }
    }

    public void HideRandomWords(int numberToHide)
    {
        // Stub: leave empty for now (still builds)
        // Later you’ll randomly pick visible words and call Hide() on them.
    }

    public string GetDisplayText()
    {
        // Stub return that builds and is usable
        List<string> rendered = new List<string>();
        foreach (Word w in _words)
        {
            rendered.Add(w.GetDisplayText());
        }

        return $"{_reference.GetDisplayText()} - {string.Join(" ", rendered)}";
    }

    public bool IsCompletelyHidden()
    {
        // Stub (valid logic)
        foreach (Word w in _words)
        {
            if (!w.IsHidden()) return false;
        }
        return true;
    }
}
