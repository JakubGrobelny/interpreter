using System;

namespace Interpreter
{
    public static class Interpreter
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var lexer = Lexer.Instance;

            try
            {
                var tokens = lexer.SplitIntoTokens(input);
                var list = lexer.Tokenize(input);
                
                Console.WriteLine(list);
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
