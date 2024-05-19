namespace Calculator3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Beginning Instructions 
            Console.WriteLine("Welcome to the calculator program! Press ESC to quit at any time.");

            Thread.Sleep(1000);
            Console.WriteLine("You can now start using the calculator.");

            // Variables 
            ClassTaschenrechner taschenrechner = new ClassTaschenrechner();
            bool quit = false;

            // Show 0 at beginning of loop 
            Console.WriteLine(taschenrechner.GetInputString());

            // Main Loop and Logic
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
                    taschenrechner.SetNumber(key.KeyChar);
                }
                else if (key.KeyChar == ',' || key.KeyChar == '.')
                {
                    taschenrechner.SetInput();
                    taschenrechner.SetComma();
                } // TODO Negative Number
                else if (key.KeyChar == '+' || key.KeyChar == '-' || key.KeyChar == '*' || key.KeyChar == '/')
                {
                    taschenrechner.AddOperator(key.KeyChar);
                } else if (key.Key == ConsoleKey.Enter)
                {
                    taschenrechner.EvaluateStack();
                }

                // Clear the Console at the End 
                Console.Clear();
                Console.WriteLine(taschenrechner.GetInputString());
            }
        }
    }
}
