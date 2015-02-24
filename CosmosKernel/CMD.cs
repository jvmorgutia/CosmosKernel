using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    public class CMD
    {
        List<char> operations;
        Variable variables;
        bool stringFound;
        string[] vars;

        public CMD()
        {
            stringFound = false;
        }

        public string[] EvaluateCommand(string input, Variable existingVariables)
        {
            variables = existingVariables;
            vars = new string[2];
            operations = new List<char>();
            string varName = input.Split(' ')[1].Trim();
            string expression = input.Split('=')[1].Trim();

            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '+') operations.Add('+');
                else if (expression[i] == '-') operations.Add('-');
                else if (expression[i] == '*') operations.Add('*');
                else if (expression[i] == '/') operations.Add('/');
                else if (expression[i] == '"') stringFound = true;
            }
            vars[0] = varName;

            if (operations.Count > 0 && !stringFound) IntExpression(expression);
            else if (operations.Count > 0 && stringFound) StringExpression(expression);
            else if (IsVariable(expression)) CopyVariable(expression);
            else if (!stringFound) SetInt(expression);
            else if (stringFound) SetString(expression);

            stringFound = false;
            return vars;
        }

        public void CopyVariable(string expression)
        {
            vars[1] = variables.GetVarValue(expression);
        }

        public void SetString(string expression)
        {
            string stringVal = "";
            int strStart = -1;
            int strEnd = -1;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[0] == '"')
                {
                    if (strStart == -1) strStart = i;
                    else strEnd = i;
                }
            }

            for (int i = strStart + 1; i < strEnd; i++)
            {
                stringVal += expression[i];
            }

            vars[1] = stringVal;
        }

        public void SetInt(string expression)
        {
            vars[1] = expression;
        }

        public void StringExpression(string expression)
        {
            if (operations[0] == '+')
            {
                string stringVal;
                string leftStr = expression.Split(operations[0])[0].Trim();
                string rightStr = expression.Split(operations[0])[1].Trim();
                string leftStringVal = "";
                string rightStringVal = "";

                int leftStringStart = -1;
                int leftStringEnd = -1;
                int rightStringStart = -1;
                int rightStringEnd = -1;
                for (int i = 0; i < leftStr.Length; i++)
                {
                    if (leftStr[i] == '"')
                    {
                        if (leftStringStart == -1) leftStringStart = i;
                        else leftStringEnd = i;
                    }
                }

                for (int i = 0; i < rightStr.Length; i++)
                {
                    if (rightStr[i] == '"')
                    {
                        if (rightStringStart == -1) rightStringStart = i;
                        else rightStringEnd = i;
                    }
                }
                if (leftStringStart == -1)
                {
                    leftStringVal = (string)variables.GetVarValue(leftStr);
                }
                else
                {
                    for (int i = leftStringStart + 1; i < leftStringEnd; i++)
                    {
                        leftStringVal += leftStr[i];
                    }
                }

                if (rightStringStart == -1)
                {
                    rightStringVal = (string)variables.GetVarValue(rightStr);
                }
                else
                {
                    for (int i = rightStringStart + 1; i < rightStringEnd; i++)
                    {
                        rightStringVal += rightStr[i];
                    }
                }

                stringVal = leftStringVal + rightStringVal;
                vars[1] = stringVal;
            }
        }

        public void IntExpression(string expression)
        {
            string leftArg = expression.Split(operations[0])[0].Trim();
            string rightArg = expression.Split(operations[0])[1].Trim();
            string leftStrVal = "";
            string rightStrVal = "";
            int leftValue = 0;
            int rightValue = 0;



            int finalValue = 0;

            if (IsVariable(leftArg))
            {
                leftStrVal = (string)variables.GetVarValue(leftArg);
                leftValue = Int32.Parse((string)variables.GetVarValue(leftArg));
            }
            else
            {
                leftValue = Int32.Parse(leftArg);
            }

            if (IsVariable(rightArg))
            {
                rightStrVal = (string)variables.GetVarValue(rightArg);
                rightValue = Int32.Parse(rightStrVal);
            }
            else
            {
                rightValue = Int32.Parse(rightArg);
            }

            if (operations[0] == '+') finalValue = leftValue + rightValue;
            else if (operations[0] == '-') finalValue = leftValue - rightValue;
            else if (operations[0] == '*') finalValue = leftValue * rightValue;
            else if (operations[0] == '/')
            {
                if ((int)rightValue == 0)
                    Console.WriteLine("Can not divide by 0");
                else
                    finalValue = (int)leftValue / (int)rightValue;
            }
            //Console.WriteLine("Final Value: " + finalValue);
            string fv = "" + finalValue;
            vars[1] = fv;
        }

        private bool IsVariable(string expression)
        {
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '"') return false;
                if (expression[0] >= '0' && expression[0] <= '9') return false;
                if ((expression[i] < 'A' || expression[i] > 'Z') && (expression[i] < 'a' || expression[i] > 'z'))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
