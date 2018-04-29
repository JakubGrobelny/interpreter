using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
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

        private bool IsClosingBrace(char c) => c == ')' || c == ']' ||c == '}';

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

        private LinkedList<string> SplitIntoTokens(string text)
        {
            CheckBraces(text);
            
            var tokens = new LinkedList<string>();
            var token = new StringBuilder();
            var readingString = false;
            var readingComment = false;
            var prev = '\0';
            
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
                        if (readingString && prev != '\\')
                        {
                            readingString = false;
                            tokens.AddLast(token.ToString());
                            token.Clear();
                        }
                        else if (!readingString)
                            readingString = true;
                    }
                    else if (c == '\'' && !readingString)
                    {
                        if (token.Length != 0)
                        {
                            tokens.AddLast(token.ToString());
                            token.Clear();
                        }

                        tokens.AddLast("'");
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

                prev = c;
            }

            if (token.Length != 0)
                tokens.AddLast(token.ToString());
            
            return tokens;
        }

        public List<TokenTree> Tokenize(string text)
        {
            // TODO handle quotes ( '«expr» -> (quote «expr) )
            
            var initiallySplit = SplitIntoTokens(text);
            var totalList = new List<TokenTree>();
            TokenTree tempListPtr = null;
            var nestCounter = 0;
            var nestStack = new Stack<TokenTree>();
            
            foreach (var token in initiallySplit)
            {
                if (token == "(")
                {
                    if (nestCounter == 0)
                    {
                        totalList.Add(new TokenNode());
                        tempListPtr = totalList.Last();
                        nestStack.Push(tempListPtr);
                    }
                    else if (nestCounter >= 1)
                    {
                        (tempListPtr as TokenNode).children.Add(new TokenNode());
                        nestStack.Push(tempListPtr);
                        tempListPtr = (tempListPtr as TokenNode).children.Last();
                    }
                    
                    nestCounter++;
                }
                else if (token == ")")
                {   
                    nestCounter--;
                    tempListPtr = nestStack.Pop();
                }
                else if (nestCounter != 0)
                    (tempListPtr as TokenNode).children.Add(new Token(token));
                else
                    totalList.Add(new Token(token));
            }

            return totalList;
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