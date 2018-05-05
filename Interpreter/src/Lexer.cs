using System.Collections.Generic;
using System.Linq;
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

        private bool IsSpecialSymbol(char c) => c == quoteChar || 
                                                c == compoundSymbolChar;

        public const string quoteKeyword = "quote";

        public const char quoteChar = '\'';
        public const char compoundSymbolChar = '$';
        public const char scopeOperator = '.';
        
        private string FilterToken(string token)
        {
            if (token == quoteKeyword)
                return quoteChar.ToString();
            return token;
        }
                
        private bool IsClosingBrace(char c) => c == ')' || 
                                               c == ']' || 
                                               c == '}';

        private bool IsOpeningBrace(char c) => c == '(' || 
                                               c == '[' || 
                                               c == '{';

        private bool IsBrace(char c) => IsOpeningBrace(c) || IsClosingBrace(c);

        private void CheckBraces(string text)
        {
            var stack = new Stack<char>();
            var readingString = false;
            char prev = '\0';

            foreach (var c in text)
            {
                if (c == '"' && prev != '\\')
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

                if (prev == '\\' && c == '\\')
                    prev = '\0';
                else
                    prev = c;
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
            
            var quoteBraceCounter = 0;
            var quoting = false;
            
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
                            tokens.AddLast(FilterToken(token.ToString()));
                            token.Clear();
                        }
                        else if (!readingString && prev != '\\')
                            readingString = true;
                    }
                    else if (IsSpecialSymbol(c) && !readingString)
                    {
                        if (token.Length != 0)
                        {
                            tokens.AddLast(FilterToken(token.ToString()));
                            token.Clear();
                        }

                        tokens.AddLast("(");
                        tokens.AddLast(c.ToString());
                        quoting = true;
                    }
                    else if (IsBrace(c) && !readingString)
                    {
                        if (token.Length != 0)
                        {
                            tokens.AddLast(FilterToken(token.ToString()));
                            token.Clear();
                        }

                        tokens.AddLast(IsOpeningBrace(c) ? "(" : ")");
                        
                        if (IsOpeningBrace(c) && quoting)
                            quoteBraceCounter++;

                        if (IsClosingBrace(c) && quoting)
                        {
                            if (quoteBraceCounter == 0)
                            {
                                tokens.AddLast(")");
                                quoting = false;
                            }
                            quoteBraceCounter--;
                        }
                    }
                    else if (c == scopeOperator && quoting)
                    {
                        if (token.Length != 0)
                        {
                            tokens.AddLast(FilterToken(token.ToString()));
                            token.Clear();
                        }
                    }
                    else if (IsWhiteSpace(c) && !readingString)
                    {
                        if (token.Length == 0) 
                            continue;
                        
                        tokens.AddLast(FilterToken(token.ToString()));
                        token.Clear();

                        if (quoting && quoteBraceCounter == 0)
                        {
                            quoting = false;
                            tokens.AddLast(")");
                        }
                    }
                    else if (c != '\\' || prev == '\\') // I have no clue why this works anymore. DO NOT CHANGE.
                        token.Append(c);
                    else
                        token.Append('\\');
                }

                prev = c;
            }

            if (token.Length != 0)
                tokens.AddLast(FilterToken(token.ToString()));
            
            return tokens;
        }

        public List<TokenTree> Tokenize(string text)
        {
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
                        ((TokenNode)tempListPtr).children.Add(new TokenNode());
                        nestStack.Push(tempListPtr);
                        tempListPtr = ((TokenNode)tempListPtr).children.Last();
                    }
                    
                    nestCounter++;
                }
                else if (token == ")")
                {   
                    nestCounter--;
                    tempListPtr = nestStack.Pop();
                }
                else if (nestCounter != 0)
                    ((TokenNode)tempListPtr).children.Add(new Token(token));
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