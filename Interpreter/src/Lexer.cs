using System.Collections.Generic;
using System.Text;
using static System.Char;

namespace Interpreter
{
    public class Lexer 
    {
        private static Lexer instance = null;

        private const char COMMENT = ';';

        private char MatchingBrace(char c)
        {
            switch (c)
            {
                case '(':
                    return ')';
                case '[':
                    return ']';
                case '{':
                    return '}';
                case ')':
                    return '(';
                case ']':
                    return '[';
                case '}':
                    return '{';
                default:
                    throw new ParenthesisError(c);
            }
        }

        
        private bool IsClosingBrace(char c) 
        {
            return  c == ')' || c == ']' ||c == '{' ;
        }

        private bool IsOpeningBrace(char c)
        {
            return c == '(' || c == '[' || c == '}';
        }

        private bool IsBrace(char c) 
        {
            return IsOpeningBrace(c) || IsClosingBrace(c);
        }

        private void CheckBraces(string text)
        {
            var stack = new Stack<char>();
            var readingString = false;
            
            foreach (var c in text)
            {
                if (c == '"')
                {
                    if (readingString)
                    {
                        if (stack.Count == 0 || stack.Peek() != '"')
                            throw new ParenthesisError(c);

                        stack.Pop();
                        
                        readingString = false;
                    }
                    else
                    {
                        readingString = true;
                        stack.Push(c);
                    }
                }
                else if (IsOpeningBrace(c) && !readingString)
                    stack.Push(c);
                else if (IsClosingBrace(c) && !readingString)
                {
                    if (stack.Count == 0 || stack.Peek() != MatchingBrace(c))
                        throw new ParenthesisError(c);
                    stack.Pop();
                }
            }
            
            if (stack.Count != 0)
                throw new ParenthesisError(stack.Peek());
        }
                
        public List<string> splitIntoTokens(string text)
        {
            CheckBraces(text);
            
            var tokens = new List<string>();
            var token = new StringBuilder();
            var readingString = false;
            var readingComment = false;
            
            foreach (var c in text)
            {
                if (c == COMMENT)
                    readingComment = true;
                if (c == '\n')
                {
                    readingComment = false;
                    continue;
                }
                
                if (!readingComment)
                {
                    if (c == '"')
                    {
                        token.Append(c);
                        if (readingString)
                        {
                            readingString = false;
                            tokens.Add(token.ToString());
                            token.Clear();
                        }
                        else
                            readingString = true;
                    }
                    else if (IsBrace(c) && !readingString)
                    {
                        if (token.Length != 0)
                        {
                            tokens.Add(token.ToString());
                            token.Clear();
                        }

                        tokens.Add(IsOpeningBrace(c) ? "(" : ")");
                    }
                    else if (IsWhiteSpace(c) && !readingString)
                    {
                        if (token.Length == 0) 
                            continue;
                        
                        tokens.Add(token.ToString());
                        token.Clear();
                    }
                    else
                        token.Append(c);    
                }
            }

            return tokens;
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