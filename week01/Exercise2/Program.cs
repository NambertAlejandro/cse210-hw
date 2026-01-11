using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please enter the percentage score: ");
        int score = int.Parse(Console.ReadLine());

        string letter = "";
        string sign = "";

        if (score >= 90)
        {
            letter = "A";
            if (score >= 94)
            {
                sign = "";
            }
            else
            {
                sign = "-";
            }
        }
        else if (score >=80)
        {
            letter = "B";
             if (score >= 87)
            {
                sign = "+";
            }
            else if (score >= 84)
            {
                sign = "";
            }
            else
            {
                sign = "-";
            }
            
        }
        else if (score >= 70)
        {
            letter = "C";
             if (score >= 77)
            {
                sign = "+";
            }
            else if (score >= 74)
            {
                sign = "";
            }
            else
            {
                sign = "-";
            }
        }
        else if (score >= 60)
        {
            letter = "D";
             if (score >= 67)
            {
                sign = "+";
            }
            else if (score >= 64)
            {
                sign = "";
            }
            else
            {
                sign = "-";
            }
        }
        else
        {
            letter = "F";
        }
        Console.WriteLine($"Grade: {letter}{sign}");

        if (score >=70)
        {
            Console.WriteLine("You passed!");
        }
        else
        {
            Console.WriteLine("You failed, but you can do better next time!");
        }
    }
}