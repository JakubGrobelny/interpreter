using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public class Quote : Value
    {
        private Expression expr;

        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            return expr;
        }

        public override string ToString()
        {
            return "'" + expr.ToString();
        }

        public Quote(Expression expr)
        {
            this.expr = expr;
        }
    }
}