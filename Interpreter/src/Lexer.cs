using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters;
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

        private bool IsClosingBrace(char c) => c == ')' || c == ']' ||c == '{';

        private bool IsOpeningBrace(char c) => c == '(' || c == '[' || c == '}';

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

        public LinkedList<string> SplitIntoTokens(string text)
        {
            CheckBraces(text);
            
            var tokens = new LinkedList<string>();
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
                            tokens.AddLast(token.ToString());
                            token.Clear();
                        }
                        else
                            readingString = true;
                    }
                    else if (IsBrace(c) && !readingString)
                    {
                        if (token.Length != 0)
                        {
                            tokens.AddLast(token.ToString());
                            token.Clear();
                        }

                        tokens.AddLast(IsOpeningBrace(c) ? "(" : ")");
                    }
                    else if (IsWhiteSpace(c) && !readingString)
                    {
                        if (token.Length == 0) 
                            continue;
                        
                        tokens.AddLast(token.ToString());
                        token.Clear();
                    }
                    else
                        token.Append(c);    
                }
            }

            if (token.Length != 0)
                tokens.AddLast(token.ToString());
            
            return tokens;
        }

        private TokenTree Tokenize(LinkedList<string> tokens)
        {
            if (tokens.Count == 1)
                return new Token(tokens.First.Value);
            
            if (tokens.First.Value != "(")
            {
                var list = new TokenNode();
                var nestCount = 0;
                var listForRecursion = new LinkedList<string>();
                
                foreach (var token in tokens)
                {
                    if (token == "(")
                    {
                        nestCount++;
                        if (nestCount != 1)
                            listForRecursion.Append(token);
                    }

                    if (token == ")")
                    {
                        nestCount--;

                        if (nestCount == 0 && listForRecursion.Count != 0)
                        {
                            var node = new TokenNode();
                            list.children.Add(Tokenize(listForRecursion));
                            listForRecursion.Clear();
                        }
                    }
                    else
                        list.children.Add(new Token(token));
                }

                return list;
            }

            tokens.RemoveFirst();
            tokens.RemoveLast();
            return Tokenize(tokens);
        }
        
        public TokenTree Tokenize(string text)
        {
            var initiallySplit = SplitIntoTokens(text);
            return Tokenize(initiallySplit);
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