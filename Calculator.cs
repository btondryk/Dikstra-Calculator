using QueueLibrary;
using StackLibrary;


// dir:  C:\Users\bryce\CSC212\DikstraCalc\Calculator\bin\Debug\net6.0>
class Program
{
    static void Main(string[] args)
    {
        // this would be if there is no readLine. For example in this case I would type in the path (dir from above) 
        // and if I do dir.EXE "( ( form + ula ) * here )", the program would print the result without prompting the user
        if (args.Length > 0) 
        {
            Calculator calculator = new Calculator();
            
            bool isBalanced = calculator.AreParenthesisBalanced(args[0]);

            if (!isBalanced)
            {
                Console.WriteLine("The expression has unbalanced parenthesis. Please check to make sure that you have the same amount of opening and closing parenthesis. ");
                Console.WriteLine("A common problem is not putting parenthesis around the equation. Remember we are parsing strings!");
            }
            else
            {
                //since there is no readline we just check if parenthesis are balanced and then we immediately parse the equation
                try
                {
                    MyQueue<string> equationTokens = calculator.ParseEquation(args[0]);
                    double result = calculator.Compute(equationTokens);
                    Console.WriteLine("Result: " + result);
                }
                // catches exception
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

        }
        else
        {
            //NOW IF THERE IS A READLINE

            //will keep promting until user enters exit or quit
            while (true) 
            {
                //Prompt User to Enter a formula with equal spaces and only parenthesis
                Console.WriteLine("Please enter a mathematical expression in the form of ( ( a + b ) * ( c / d ) ) . Valid operations include " +
                    "+, - , * , /, **, sqrt, sin, cos, tan. Ensure proper spacing like above.");
                //this readline will be checked if balanced and if the operators equal parenthesis * 0.5
                string inputExpression = Console.ReadLine();
                if (inputExpression.ToLower() == "exit" || inputExpression.ToLower() == "quit")
                {
                    break;
                }

                //utilize the calculator class to have access to calculator methods
                Calculator calculator = new Calculator();

                bool isBalanced = calculator.AreParenthesisBalanced(inputExpression);

                if (!isBalanced)
                {
                    Console.WriteLine("The expression has unbalanced parenthesis. Please check to make sure that you have the same amount of opening and closing parenthesis.");
                }
                else
                {
                    try
                    {
                        MyQueue<string> equationTokens = calculator.ParseEquation(inputExpression);
                        double result = calculator.Compute(equationTokens);
                        Console.WriteLine("Result: " + result);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }
        }
    }


    class Calculator
    {
        /// <summary>
        /// This method evaluates whether the parenthesis are balanced or not
        /// </summary>
        /// <param name="equation">the user input or args[0]</param>
        /// <returns>true if parenthesis are balanced; false if they are not balanced </returns>
        public bool AreParenthesisBalanced(string equation)
        {
            int count = 0;
            foreach (char c in equation)
            {
                if (c == '(')
                { count++; }
                else if (c == ')')
                { count--; }
                if (count < 0)
                {
                    return false;
                }

            }
            if (count != 0)
            {
                return false;
            }
            return count == 0;
        }
        /// <summary>
        /// This method parses the users input. It first stores the input as a string and then splits this string
        /// by the spaces in between each token. Then each token is evaluated. If the token is a parenthesis, the 
        /// parenthesis count is increased. If the token is an operator the operator count is increased. 
        /// The program then enqueues each token. If the operator count * 2 != the paranthesis count, an exception is
        /// thrown. 
        /// </summary>
        /// 
        /// <param name="equation"></param>
        /// <returns> The QUEUE with each token </returns>
        /// <exception cref="InvalidOperationException">Exception is thrown if there are unbalanced parathesis and
        /// operators</exception>
        public MyQueue<string> ParseEquation(string equation)
        {
            MyQueue<string> expr = new MyQueue<string>();
            string[] tokens = equation.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            int countP = 0;
            int countO = 0;
            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i] == "(" || tokens[i] == ")")
                {
                    countP++;
                }
                else if (tokens[i] == "+" || tokens[i] == "-" || tokens[i] == "*" || tokens[i] == "/" || tokens[i] == "**" || tokens[i] == "sqrt" || tokens[i] == "sin" || tokens[i] == "cos" || tokens[i] == "tan")
                {
                    countO++;
                }
                //pushes all the tokens in the queue
                expr.Enqueue(tokens[i]);
            }
            //another check for if the parenthesis and operators are placed correctly
            if (countP != countO * 2) 
            {
                throw new InvalidOperationException("Error: Parenthesis and operators are not adding up. Please make sure there is a space between each element and you have the correct number of operators and parenthesis");
            }
            else
            {
                return expr;
            }


        }
        /// <summary>
        /// In this method, each token is dequeued and either pushed onto the operator stack or value stack. When 
        /// dequeue"ing" the queue, if a closing parenthesis is encountered, both the stacks are popped to return a value.
        /// Depending on the operator, two values might need to be popped - like for addition. Then this program pushes that 
        /// value back onto the stack until there is just one value remaining and no operators. To get the result, 
        /// this last value is popped.
        /// </summary>
        /// <param name="equationTokens"></param>
        /// <returns>This method returns the last value on the stack when the operator stack is empty</returns>
        /// <exception cref="InvalidOperationException">One exception is thrown if there is a negative sqrt. The other 
        /// exception is thrown when the user inputs non numeric values.</exception>
        public double Compute(MyQueue<string> equationTokens)
        {
            MyStack<string> ops = new MyStack<string>();
            MyStack<double> vals = new MyStack<double>();

            while (!equationTokens.IsEmpty())
            {
                //DeQueues all the tokens
                //Basicallly we enqueued all the tokens and then dequeued them to show the queue works :)

                string tokens = equationTokens.DeQueue();
                //ignore opening parenthesis
                if (tokens == "(")
                {

                }
                //push all the operators on stack
                else if (tokens == "+")
                {
                    ops.Push(tokens);

                }
                else if (tokens == "-")
                {
                    ops.Push(tokens);

                }
                else if (tokens == "*")
                {
                    ops.Push(tokens);

                }
                else if (tokens == "/")
                {
                    ops.Push(tokens);

                }
                else if (tokens == "**")
                {
                    ops.Push(tokens);

                }
                else if (tokens == "sqrt")
                {
                    ops.Push(tokens);

                }
                else if (tokens == "sin")
                {
                    ops.Push(tokens);

                }
                else if (tokens == "cos")
                {
                    ops.Push(tokens);

                }
                else if (tokens == "tan")
                {
                    ops.Push(tokens);

                }
                else if (tokens == ")")
                {
                    //pop an operator when a closing parenthesis is detected                
                    string opperator = ops.Pop();
                    double valueA = vals.Pop();

                    //this operator needs two elements so we take the 2 topmost values off the stack
                    if (opperator == "+")
                    {
                        valueA = vals.Pop() + valueA;
                    }
                    //2 elements. Subtraction is not commutative so we pay attention to ordering
                    else if (opperator == "-")
                    {
                        valueA = vals.Pop() - valueA;
                    }
                    //2 elements
                    else if (opperator == "*")
                    {
                        valueA = vals.Pop() * valueA;
                    }
                    //2 elements, pay attention to ordering.
                    else if (opperator == "/")
                    {
                        valueA = vals.Pop() / valueA;
                    }
                    //2 elements, the value and the exponent
                    else if (opperator == "**")
                    {
                        valueA = Math.Pow(vals.Pop(), valueA);
                    }
                    //1 element satisfies this condition
                    else if (opperator == "sqrt")
                    {
                        if (valueA < 0)
                        {
                            throw new InvalidOperationException("Sorry the capabilities of this calculator lie in the real number set!");
                        }
                        else
                        {
                            valueA = Math.Sqrt(valueA);
                        }
                        
                    }
                    //1 element
                    else if (opperator == "sin")
                    {
                        valueA = Math.Sin(valueA);
                    }
                    //1 element
                    else if (opperator == "cos")
                    {
                        valueA = Math.Cos(valueA);
                    }
                    //1 element
                    else if (opperator == "tan")
                    {
                        valueA = Math.Tan(valueA);
                    }
                    //now push the value back onto the stack
                    vals.Push(valueA);
                }
                //uses tryparse which more safely parses the program
                //throws exception if user enters a non number
                else
                {
                    if (!double.TryParse(tokens, out double numericValue))
                    {
                        throw new InvalidOperationException($"Error: Unable to parse numeric value '{tokens}'. Please double check that no letters are in the formula. Common problem: the 0 is an O.");
                    }
                    //pushes the parsed value back on the stack
                    vals.Push(numericValue);
                }

                
            }
            //pop the value
            return (vals.Pop());
            

        }

    }
}



