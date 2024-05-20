using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator3
{
    // CHANGES
    // EvaluateStack: Old Version Operands where poped without considering order, wich is incorrect for subtraction and divisions (a - b correct; b - a incorrect)
    // Old Version called EvaluateStack as long as there were two operands, causing unnecessary evaluations
    // Simplified logic by recognizing current operator precedence

    public class ClassTaschenrechner
    {
        // Attributes
        string inputString = "0"; // input number, as a string
        public bool inputFlag = true; // number or an operator is being entered
        decimal buffer = 0; // Zwischensumme

        // Stacks
        Stack<decimal> operandenStack = new Stack<decimal>();
        Stack<char> operatorenStack = new Stack<char>();

        // Methods
        public ClassTaschenrechner()
        {
            operandenStack.Push(0);
            operatorenStack.Push('+');
        }

        // Returns inputString else top value of operandStack
        public string GetInputString()
        {
            if (inputFlag)
                return inputString;
            return operandenStack.Peek().ToString();
        }

        // inputString Builder
        public void SetNumber(char number)
        {
            if (inputString == "0" && number == '0')
                return;
            if (inputString == "0")
                inputString = "";
            inputString += number;
        }

        // inputFlag to true, indicates that number is being entered
        public void SetInput()
        {
            if (!inputFlag)
                inputFlag = true;
        }

        public void SetComma()
        {
            // , and . -> comma, but no double commas
            if (!inputString.Contains(','))
                inputString += ",";
        }

        public void EvaluateStack()
        {
            // if there are more than 1 operand on the stack perform calculation 
            // using the top operator from operatorStack
            if (operandenStack.Count >= 2)
            {
                char oldOp = operatorenStack.Pop();
                decimal operand2 = operandenStack.Pop();
                decimal operand1 = operandenStack.Pop();

                switch (oldOp)
                {
                    case '+':
                        buffer = operand1 + operand2;
                        break;
                    case '-':
                        buffer = operand1 - operand2;
                        break;
                    case '*':
                        buffer = operand1 * operand2;
                        break;
                    case '/':
                        buffer = operand1 / operand2;
                        break;
                }

                operandenStack.Push(buffer); // push Intermediate result to stack

                // If Values are still in the operand Stack than perform the calculation again 
                if (operatorenStack.Count > 0 && operandenStack.Count >= 2)
                {
                    EvaluateStack();
                }
            }
        }

        public void AddOperator(char op)
        {
            inputFlag = false; // Operator will be entered

            // Convert current input string to decimal & pushes it to the stack
            decimal lastInputNumber = Convert.ToDecimal(inputString);
            operandenStack.Push(lastInputNumber);

            // Current Operator is point and dash -> keep pushing to stack
            if ((op == '*' || op == '/') && (operatorenStack.Peek() == '+' || operatorenStack.Peek() == '-')) 
            {
                operatorenStack.Push(op);
            }
            else // Let all the values in the stack be evaluated
            {
                EvaluateStack();
                operatorenStack.Push(op);
            }

            // Reset current input
            inputString = "0";
        }
    }
}
