using System;

namespace TestTask0
{
    public class TaskZero
    {
        public static void Main()
        {
            Console.WriteLine("Enter the first number:");
            int number1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the second number:");
            int number2 = int.Parse(Console.ReadLine());

            int sum = AddNumbers(number1, number2);
            Console.WriteLine("The sum of {0} and {1} is: {2}", number1, number2, sum);
        }

        public static int AddNumbers(int number1, int number2)
        {
            return number1 + number2;
        }
    }
}