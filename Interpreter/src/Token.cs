using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Interpreter
{
    public abstract class TokenTree {}

    public class Token : TokenTree
    {
        public string token;

        public override String ToString()
        {
            return token;
        }
        
        public Token(string token)
        {
            this.token = token;
        }

        public static bool operator==(Token token, string str)
        {
            return token.token == str;
        }

        public static bool operator!=(Token token, string str)
        {
            return !(token == str);
        }
    }

    public class TokenNode : TokenTree
    {
        public List<TokenTree> children;

        public override string ToString()
        {
            string result = "(";

            foreach (var token in children)
                result = result + token + " ";

            return result + ")";
        }
        
        public TokenNode()
        {
            this.children = new List<TokenTree>();
        }
        
        public TokenNode(List<TokenTree> children)
        {
            this.children = children;
        }
    }
}