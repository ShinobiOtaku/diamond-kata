using System;

namespace diamond_kata
{
    internal class Program
    {
        static bool IsValidInput(string input)
        {
            if(input.Length != 1)
                return false;

            if (input[0] < 'A' || input[0] > 'Z')
                return false;

            return true;
        }

        static void Main(string[] args)
        {
            var validation = IsValidInput(args[0]);
            if (!validation)
            {
                Console.WriteLine("Invalid input.");
            }
            else
            {
                var diamond = Diamond.Generate(args[0][0]);
                var stringified = string.Join(Environment.NewLine, diamond);
                Console.WriteLine(stringified);
            }
        }
    }
}
