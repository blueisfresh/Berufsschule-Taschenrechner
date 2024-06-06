using CalculatorEngine;

namespace CalculatorConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Variables 
            ClassCalculator taschenrechner = new ClassCalculator();
            bool quit = false;

            // Show 0 at beginning of loop 
            Console.WriteLine(taschenrechner.GetInputString());

            // Main Loop and Logic
            try
            {
                while (!quit)
                {
                    var key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Escape)
                    {
                        quit = true;
                    }
                    else if (char.IsDigit(key.KeyChar))
                    {
                        taschenrechner.SetInput();
                        taschenrechner.SetNumber(key.KeyChar.ToString());
                    }
                    else if (key.KeyChar == ',' || key.KeyChar == '.')
                    {
                        taschenrechner.SetInput();
                        taschenrechner.SetComma();
                    }
                    else if (!taschenrechner.isNumber && key.KeyChar == '-')
                    {
                        taschenrechner.SetInput();
                        taschenrechner.SetNumber(key.KeyChar.ToString());
                    }
                    else if (key.KeyChar == '+' || key.KeyChar == '-' || key.KeyChar == '*' || key.KeyChar == '/' || key.KeyChar == '(' || key.KeyChar == ')')
                    {
                        taschenrechner.AddOperator(key.KeyChar);
                    }
                    else if (key.Key == ConsoleKey.S)
                    {
                        string currentInput = taschenrechner.GetInputString();
                        string result = taschenrechner.CalculateSine(currentInput);
                        taschenrechner.SetNumber(result);
                    }
                    else if (key.Key == ConsoleKey.C)
                    {
                        string currentInput = taschenrechner.GetInputString();
                        string result = taschenrechner.CalculateCosine(currentInput);
                        taschenrechner.SetNumber(result);
                    }
                    else if (key.Key == ConsoleKey.T)
                    {
                        string currentInput = taschenrechner.GetInputString();
                        string result = taschenrechner.CalculateTangent(currentInput);
                        taschenrechner.SetNumber(result);
                    }

                    // Clear the Console and show the current input/output
                    Console.Clear();
                    Console.WriteLine(taschenrechner.GetInputString());
                }
            }
            catch (DivideByZeroException ex)
            {
                Console.Clear();
                Console.WriteLine("Error: Cannot divide by zero.");
            }
            catch (InvalidOperationException ex)
            {
                Console.Clear();
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            // Clear the Console at the End 
            Console.Clear();
            Console.WriteLine(taschenrechner.GetInputString());
        }
    }
}
