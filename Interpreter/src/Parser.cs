using System.Runtime.CompilerServices;
using Interpreter.Expressions;
using System.Collections.Generic;

namespace Interpreter
{
    public class Parser
    {
        private static Parser instance = null;
        
        public static Parser Instance
        {
            get
            {
                if (instance == null)
                    instance = new Parser();
                return instance;
            }
        }

        private Parser() {}

        public Expression ParseExpression(TokenTree expression)
        {
            if (expression is Token)
                return ExpressionFactory.ParseValue(expression.ToString());
            else
            {
                TokenNode expr = expression as TokenNode;
                var list = expr.children;

                if (expr.children.Count == 0)
                    throw new InvalidEmptyExpression();


            }

            return null;
        }
    }
}