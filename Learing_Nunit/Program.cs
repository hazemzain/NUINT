using ExcelDataReader;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Learing_Nunit
{
    public class Program
    {
        public static int MainGuiCalculator(string input)
        {

            var calculator = new Calculator();
            int result = 0;

            try
            {
                result = EvaluateExpression(input, calculator);


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return result;
        }
        static void Main(string[] args)
        {
            bool exit = false;
            Console.WriteLine("Welcome to  Calculator!");
            Console.WriteLine("============================================");
            while (!exit)
            {
                Console.WriteLine("\nEnter a mathematical expression (e.g., 5 + 3 - 2 * 4 / 2) or type 'exit' to quit:");
                string? input = Console.ReadLine();
                if (input != null && input.Trim().ToLower() == "exit")
                {
                    exit = true;
                    Console.WriteLine("Goodbye!");
                    break;
                }
                int result=MainGuiCalculator(input);
                Console.WriteLine($"Result: {result}");
            }
                
        }

        public static int EvaluateExpression(string expression, Calculator calculator)
        {
            int result = 0;
            int flag = 0;
            string value1 = "";
            string operation = "";
            string value2 = "";

            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new ArgumentException("Expression cannot be empty.");
            }

           
            var tokens = Regex.Split(expression, @"(\+|\-|\*|\/)")
                              .Where(t => !string.IsNullOrWhiteSpace(t))
                              .ToArray();

            List<string> tempTokens = new List<string>();

            for (int i = 0; i < tokens.Length; i++)
            {
                if (tempTokens.Count > 0 && (tokens[i] == "*" || tokens[i] == "/"))
                {
                    
                    int val1 = int.Parse(tempTokens.Last());
                    int val2 = int.Parse(tokens[++i].Trim());
                    string op = tokens[i - 1].Trim();
                    val1 = PerformOperation(val1, val2, op, calculator);
                    tempTokens[tempTokens.Count - 1] = val1.ToString();
                }
                else
                {
                    tempTokens.Add(tokens[i].Trim());
                }
            }

            result = int.Parse(tempTokens[0]);
            for (int i = 1; i < tempTokens.Count; i += 2)
            {
                operation = tempTokens[i];
                value2 = tempTokens[i + 1];
                result = PerformOperation(result, int.Parse(value2), operation, calculator);
            }

            return result;
        }




        //public static int EvaluateExpression(string expression,Calculator calculator)
        //{
        //    int result = 0;
        //    int flag = 0;  
        //    string value1 = "";
        //    string operation = "";
        //    string value2 = "";

        //    if (string.IsNullOrWhiteSpace(expression))
        //    {
        //        throw new ArgumentException("Expression cannot be empty.");
        //    }


        //    var tokens = Regex.Split(expression, @"(\+|\-|\*|\/)")
        //                      .Where(t => !string.IsNullOrWhiteSpace(t))  
        //                      .ToArray();

        //    for (int i = 0; i < tokens.Length; i++)
        //    {
        //        if (!"+-/*".Contains(tokens[i].Trim()) && tokens[i].Trim().All(char.IsDigit) && flag == 0)
        //        {
        //            value1 = tokens[i].Trim(); 
        //            result = int.Parse(value1); 
        //            flag = 1;  
        //        }
        //        else if ("+-/*".Contains(tokens[i].Trim())) 
        //        {
        //            operation = tokens[i].Trim(); 
        //            flag = 2;  
        //        }
        //        else if (!"+-/*".Contains(tokens[i].Trim()) && tokens[i].Trim().All(char.IsDigit) && flag == 2)
        //        {
        //            value2 = tokens[i].Trim(); 
        //            result = PerformOperation(result, int.Parse(value2), operation, calculator); 
        //            flag = 1;
        //        }
        //    }

        //    return result;
        //}


        public static int PerformOperation(int value1, int value2, string operation, Calculator calculator)
        {
            return operation switch
            {
                "+" => calculator.Add_Number(value1, value2),
                "-" => calculator.Sub_number(value1, value2),
                "*" => calculator.Multiply_Number(value1, value2),
                "/" => calculator.Divide_Number(value1, value2),
                _ => throw new InvalidOperationException("Unsupported operator: " + operation),
            };
        }
    }
}
