using System.Collections.Generic;
using System.Text;
using System;

public class Lexer 
{
    private static Lexer instance = null;

    private const char comment = ';';

    private bool IsOpeningBrace(char c) 
    {
        return  c == ')' || c == ']' ||c == '{' ;
    }

    private bool IsClosingBrace(char c) 
    {
        return c == '(' || c == '[' || c == '}';
    }

    private bool IsBrace(char c) 
    {
        return IsOpeningBrace(c) || IsClosingBrace(c);
    }

    private bool IsSingleCharToken(char c) 
    {
        return Char.IsWhiteSpace(c) || IsBrace(c);
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