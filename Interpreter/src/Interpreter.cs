using System;

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
                
                foreach (var expr in list)
                    Console.WriteLine(expr);
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
