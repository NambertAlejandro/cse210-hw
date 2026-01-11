using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a list of numbers negatives and positives, type 0 when finished.");
        Console.WriteLine("Enter number:");
        int number = int.Parse(Console.ReadLine());
        List <int> numbers = new List<int>();
        int sum = 0;
        while (number != 0)
        {
            numbers.Add(number);
            Console.WriteLine("Enter number:");
            number = int.Parse(Console.ReadLine());
            sum += number;
        }
        int numberCount = numbers.Count;
        double average = (double)sum / numberCount;

        int maxNumber = numbers[0];
        foreach (int num in numbers)
        {
            if (num > maxNumber)
            {
                maxNumber = num;
            }
        }
        int smallestNumberNext0 = numbers[0];
        foreach (int num2 in numbers)
        {
            if (num2 < smallestNumberNext0 && num2 > 0)
            {
                smallestNumberNext0 = num2;
            }
        }


        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {maxNumber}");
        Console.WriteLine($"The smallest positive number is: {smallestNumberNext0}");
        Console.WriteLine($"The sorted list is:");
        numbers.Sort();
        foreach (int num3 in numbers)
        {
            Console.WriteLine(num3);
        }
    }
}