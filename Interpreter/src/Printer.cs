using System;
using Interpreter.Expressions;
using Void = Interpreter.Expressions.Void;

namespace Interpreter
{
    public class Printer
    {
        private static Printer instance;

        public void Print(Expression expr)
        {
            if (!(expr is Void))
            {
                if (Pair.IsList(expr) || expr is Symbol || expr is Pair || expr is CompoundSymbol)
                    Console.WriteLine("\'" + expr);
                else
                    Console.WriteLine(expr.ToString());
            }
        }

        public static Printer Instance
        {
            get
            {
                if (instance == null)
                    instance = new Printer();
                return instance;
            }
        }

        private Printer() {}
    }
}