using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Interpreter.Expressions;
using Void = Interpreter.Expressions.Void;

namespace Interpreter
{
    public abstract class ExpressionFactory
    {
        private static bool IsBool(string str) => (str == "#f" || str == "#t");

        private static bool IsVoid(string str) => str == "#<void>";

        private static bool IsNull(string str) => str == "#<null>";

        private static bool IsString(string str)
        {
            if (str.Length < 2 || str[0] != str.Last() || str[0] != '"')
                return false;

            // If this string passed through the Lexer it means that
            // it does not contain any illegal characters.
            return true;
        }

        private static bool IsRational(string str)
        {
            var strBuilder = new StringBuilder();
            BigInteger temp;
            var barAppeared = false;
            
            foreach (var c in str)
            {
                if (c == '/')
                {
                    if (barAppeared)
                        return false;
                    barAppeared = true;
                    
                    if (!BigInteger.TryParse(strBuilder.ToString(), out temp))
                        return false;
                    strBuilder.Clear();
                }
                else
                    strBuilder.Append(c);
            }

            if (strBuilder.Length == 0) return false;
            return BigInteger.TryParse(strBuilder.ToString(), out temp);
        }

        // Tries to parse string into value.
        public static Expression ParseExpression(string expr)
        {
            if (expr.Length == 0)
                throw new UnhandledLexerException("ParseExpression: empty string!");
            
            // Simple types
            if (IsBool(expr))
                return new Bool(expr);
            if (IsVoid(expr))
                return Void.Instance;
            if (IsNull(expr))
                return Null.Instance;
            if (IsString(expr))
                return new StringLiteral(expr);
            // Types that are more difficult to parse.
            else
            {
                BigInteger integer;
                if (BigInteger.TryParse(expr, out integer))
                    return new Integer(integer);
                
                double real;
                if (Double.TryParse(expr, out real))
                    return new Real(real);
                
                if (IsRational(expr))
                    return new Rational(expr);
            }
            
            // Unmatched value is a symbol;
            return new Symbol(expr);
        }
    }
}