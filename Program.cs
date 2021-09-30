using System;
using System.Collections.Generic;
using System.Linq;

namespace cslisp
{
    class Program
    {
        static void Main(string[] args)
        {
            Loop();
        }
        static void Loop()
        {
            Console.Write(">");
            string val;
            val = Console.ReadLine();
            object returned = Parse(val);
            Console.WriteLine(returned);
            Loop();
        }
        static int Eval(string[] input)
        {
            var result = 0;
            string op = input[0];
            string[] args = input.Skip(1).ToArray();
            List<int> argsInt = new List<int>();
            foreach (var item in args)
                argsInt.Add(Int32.Parse(item));
            switch (op)
            {
                case "+":
                    result = Add(argsInt);
                    break;
                case "-":
                    result = Minus(argsInt);
                    break;
                case "*":
                    result = Multiply(argsInt);
                    break;
            }
            return result;
        }
        static int Add(List<int> args)
        {
            int result = 0;
            foreach (var arg in args)
            {
                result += arg;
            }
            return result;
        }

        static int Minus(List<int> args)
        {
            int result = args[0];
            foreach (var arg in args.Skip(1).ToArray())
            {
                result -= arg;
            }
            return result;
        }
        static int Multiply(List<int> args)
        {
            int result = 1;
            foreach (var arg in args)
            {
                result *= arg;
            }
            return result;
        }

        static object Parse(string input)
        {
            var openParen = input.IndexOf("(") + 1;
            var closeParen = input.LastIndexOf(")") - 1;
            var result = input.Substring(openParen, closeParen);
            var next = result.IndexOf("(");
            if (next > 0)
            {
                var thisForm = result.Substring(0, next - 1);
                var nextForm = result.Substring(next);
                int toReturn = 0;
                object nextEvaluated = Parse(nextForm); //cannot implicitly converty type 'object' to 'int' An explicit conversion exists (are you missing a cast?)
                if (nextEvaluated is int)
                {
                    toReturn = Eval(thisForm.Split(" ")) + (int)nextEvaluated;
                }
                return toReturn;
            }
            else
            {
                var strArr = result.Split(" ");
                return Eval(strArr);
            }
        }
    }
}
