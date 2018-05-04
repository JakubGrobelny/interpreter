using System.Collections.Generic;

namespace Interpreter.Expressions
{
    // TODO: make it store lists (or actually make parser convert
    // everything into a list
    // TODO: alternatively make it store TokenTrees
    public class Quote : Value
    {
        private Expression expr;

        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            //TODO: if list, then convert into token list and parse again
            return expr;
        }

        public override object Clone() => new Quote((Expression)expr.Clone());

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