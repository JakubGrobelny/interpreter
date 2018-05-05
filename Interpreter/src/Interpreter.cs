using System;
using System.Collections.Generic;
using Interpreter.Expressions;
using Void = Interpreter.Expressions.Void;

namespace Interpreter
{
    public static class Interpreter
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine();

            try
            {
                var list = Lexer.Instance.Tokenize(input);
                
                //TODO: add else to environment (def else #t)
                var globalEnv = new Dictionary<Symbol, Expression>();
                
                //TODO: replace this temporary loop with read-eval-write loop
                foreach (var expr in list)
                {
                    var expression = Parser.Instance.ParseExpression(expr);
                    var value = expression.Evaluate(globalEnv);
                    if (!(value is Void))
                        Console.WriteLine(value.ToString());
                }
                //    Console.WriteLine(Parser.Instance.ParseExpression(expr));                
            }
            catch (InternalException exc)
            {
                Console.WriteLine("Error!: " + exc.Message);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Interpreter error!\n" +
                                  "If this has happened then you should " +
                                  "report the bug.\n" + exc);                
            }
            
            Console.ReadKey();
        }
    }
}
