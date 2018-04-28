using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Interpreter
{
    public abstract class TokenTree {}

    public class Token : TokenTree
    {
        protected bool Equals(Token other)
        {
            return string.Equals(token, other.token);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) 
                return false;
            if (ReferenceEquals(this, obj)) 
                return true;
            if (obj.GetType() != this.GetType()) 
                return false;
            return Equals((Token) obj);
        }

        public override int GetHashCode()
        {
            return (token != null ? token.GetHashCode() : 0);
        }

        public string token;

        public override string ToString()
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