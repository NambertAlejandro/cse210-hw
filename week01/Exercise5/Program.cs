using System;

class Program
{
    static void Main(string[] args)
    {
        //Display Welcome
        static void DisplayWelcome()
        {
            Console.WriteLine("Welcome to the Program!");
        }

        //Prompt User Name
        static string PromptUserName()
        {
            Console.Write("Please enter your name: ");
            return Console.ReadLine();
        }

        // Prompt User Number
        static int PromptUserNumber()
        {
            Console.WriteLine("Please enter your favorite number:");
            int number = int.Parse(Console.ReadLine());
            return number;
        }

        //Square Number
        static int SquareNumber(int number)
        {
            return number * number;
        }

        //Display Result
        static void DisplayResult(string name, int squaredNumber)
        {
            Console.WriteLine($"Brother {name}, the square of your favorite number is {squaredNumber}.");
        }

        //Main Program
        DisplayWelcome();
        string name = PromptUserName();
        int favoriteNumber = PromptUserNumber();
        int squaredNumber = SquareNumber(favoriteNumber);
        DisplayResult(name, squaredNumber);
    }
}