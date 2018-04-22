using System.Collections.Generic;
using static System.Char;

namespace Interpreter
{
    public abstract class Token {}

    public class TokenElement : Token
    {
        public string Token { get; set; }
    }

    public class TokenList : Token
    {
        public List<string> Tokens { get; set; }
    }

    public class Lexer 
    {

        public static List<Token> Tokenize(string text)
        {
            return null;
            //TODO:
        }

        private static Lexer instance = null;

        private const char comment = ';';

        private static bool IsOpeningBrace(char c) 
        {
            return  c == ')' || c == ']' ||c == '{' ;
        }

        private static bool IsClosingBrace(char c) 
        {
            return c == '(' || c == '[' || c == '}';
        }

        private static bool IsBrace(char c) 
        {
            return IsOpeningBrace(c) || IsClosingBrace(c);
        }

        private bool IsSingleCharToken(char c) 
        {
            return IsWhiteSpace(c) || IsBrace(c);
        }

        public static Lexer Instance
        {
            get
            {
                if (instance == null)
                    instance = new Lexer();
                return instance;
            }
        }

        private Lexer() {}
    }
}