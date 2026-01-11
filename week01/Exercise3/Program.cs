using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Give me a Magic Number:");
        int magicNumber = int.Parse(Console.ReadLine());

        Console.WriteLine("Give me a number to guess the Magic Number:");
        int userGuess = int.Parse(Console.ReadLine());
        int attempts = 0;


        while (userGuess != magicNumber)
        {
            attempts++;
            if (userGuess < magicNumber)
            {
                Console.WriteLine("Higher!");
            }
            else
            {
                Console.WriteLine("Lower!");
            }

            Console.WriteLine("Guess again:");
            userGuess = int.Parse(Console.ReadLine());
            
            {
                Console.WriteLine();
            }
        }

        Console.WriteLine($"You Guessed! You tried {attempts} times");

    }
}