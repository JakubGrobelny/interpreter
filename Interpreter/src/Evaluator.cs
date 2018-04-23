using System.Runtime.CompilerServices;
using Interpreter.Expressions;

namespace Interpreter
{
    public class Evaluator
    {
        private static string[] KEYWORDS =
        {
            "def", "def-func", "def-class",
            "set!", "and", "or", "if", "lambda",
            "let", "def-array", "def-macro"
        };

        private Evaluator instance = null;
        
        public Evaluator Instance
        {
            get
            {
                if (instance == null)
                    instance = new Evaluator();
                return instance;
            }
        }
        
        private Evaluator() {}
        
        public Expression EvaluateExpression(TokenTree expression)
        {
            if (expression is Token)
                return ExpressionFactory.ParseExpression(expression.ToString());
            else
            {
                TokenNode listExpression = expression as TokenNode;
                
                if (listExpression.children.Count == 0)
                    throw new InvalidEmptyExpression();

                switch (listExpression.children[0])
                {
                    default: return null;
                }
            }
        }
    }
}