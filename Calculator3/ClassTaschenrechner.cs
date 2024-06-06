namespace CalculatorEngine
{
    public class ClassCalculator
    {
        // Attributes
        string inputString = "0"; // input number, as a string
        public bool isNumber = true; // input a number or Operator?
        public decimal buffer = 0; // Subtotal

        // Stacks
        public Stack<decimal> operandsStack = new Stack<decimal>();
        public Stack<char> operatorsStack = new Stack<char>();

        // History
        public List<string> history = new List<string>();

        // Methods
        public ClassCalculator()
        {
            operandsStack.Push(0);
            operatorsStack.Push('+');
        }

        // Returns inputString else top value of operandsStack
        public string GetInputString()
        {
            if (isNumber)
                return inputString;
            return operandsStack.Peek().ToString();
        }


        // isNumber to true, indicates that number is being entered
        public void SetInput()
        {
            if (!isNumber)
                isNumber = true;
        }

        public void SetComma()
        {
            // , and . -> comma, but no double commas
            if (!inputString.Contains(','))
                inputString += ",";
        }

        // inputString Builder
        public void SetNumber(string number)
        {
            if (number.Length > 1)
            {
                inputString = number;
            }
            else
            {
                if (inputString == "0" && number == "0") return;
                if (inputString == "0") inputString = "";
                inputString += number;
            }
        }
        public void EvaluateStack()
        {
            // using the top operator from operatorStack to perform calculations
            while (operatorsStack.Count > 0 && operandsStack.Count >= 2)
            {
                char oldOp = operatorsStack.Pop();
                if (oldOp == '(')
                {
                    operatorsStack.Push(oldOp);
                    break;
                }

                decimal operand2 = operandsStack.Pop();
                decimal operand1 = operandsStack.Pop();
                decimal result = PerformOperation(operand1, operand2, oldOp);

                buffer = result;
                operandsStack.Push(buffer); // push Intermediate result to stack
                history.Add($"{operand1} {oldOp} {operand2} = {result}");

                // If values are still in the operand Stack, perform the calculation again
                if (operatorsStack.Count > 0 && operandsStack.Count >= 2)
                {
                    EvaluateStack();
                }
            }
        }

        // Performs arithmetic operations
        private decimal PerformOperation(decimal operand1, decimal operand2, char operation)
        {
            return operation switch
            {
                '+' => operand1 + operand2,
                '-' => operand1 - operand2,
                '*' => operand1 * operand2,
                '/' => operand2 == 0 ? throw new DivideByZeroException("Cannot divide by zero.") : operand1 / operand2,
                _ => throw new InvalidOperationException($"Invalid operator: {operation}")
            };
        }

        public void AddOperator(char op)
        {
            isNumber = false;

            // Prevent pushing zero to the stack unless it is necessary
            if (inputString != "0" || op == '(' || op == ')')
            {
                decimal lastInputNumber = Convert.ToDecimal(inputString);
                // Push only if it's not the start of a parenthesis block with zero
                if (!(lastInputNumber == 0 && op == '('))
                {
                    operandsStack.Push(lastInputNumber);
                }
            }

            // Parantheses Evaluation
            if (op == '(')
            {
                operatorsStack.Push(op);
            }
            else if (op == ')')
            {
                // Continue Evaluating stack until the Parantheses are solved
                while (operatorsStack.Count > 0 && operatorsStack.Peek() != '(')
                {
                    EvaluateStack();
                }

                if (operatorsStack.Count == 0 || operatorsStack.Peek() != '(')
                {
                    throw new InvalidOperationException("Mismatched parentheses.");
                }

                operatorsStack.Pop(); // Popping opening parantheses

                // After popping '(', push the result of the evaluated expression back onto the operand stack
                if (operandsStack.Count > 0)
                {
                    buffer = operandsStack.Pop();
                    operandsStack.Push(buffer); // Push result after evaluating parenthesis
                }
            }

            // Perform normal calculations
            else if ((op == '*' || op == '/') && (operatorsStack.Peek() == '+' || operatorsStack.Peek() == '-'))
            {
                operatorsStack.Push(op);
            }
            else
            {
                EvaluateStack();
                operatorsStack.Push(op);
            }

            history.Add($"Operator: {op}");

            inputString = "0";
        }


        // Output History 
        public List<string> GetHistory()
        {
            return history;
        }

        // Trigonometry Methods
        public string CalculateSine(string input)
        {
            decimal lastInputNumber = Convert.ToDecimal(input);
            double radians = (double)lastInputNumber * (Math.PI / 180);
            buffer = (decimal)Math.Sin(radians);
            inputString = buffer.ToString();
            return inputString;
        }

        public string CalculateCosine(string input)
        {
            decimal lastInputNumber = Convert.ToDecimal(input);
            double radians = (double)lastInputNumber * (Math.PI / 180);
            buffer = (decimal)Math.Cos(radians);
            inputString = buffer.ToString();
            return inputString;
        }

        public string CalculateTangent(string input)
        {
            decimal lastInputNumber = Convert.ToDecimal(input);
            double radians = (double)lastInputNumber * (Math.PI / 180);
            if ((radians / Math.PI) % 0.5 == 0)
            {
                throw new InvalidOperationException("Tangent is undefined at this angle.");
            }
            buffer = (decimal)Math.Tan(radians);
            inputString = buffer.ToString();
            return inputString;
        }
    }
}
