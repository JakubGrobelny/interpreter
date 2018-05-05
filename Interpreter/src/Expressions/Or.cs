using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public class Or : SpecialForm
    {
        private List<Expression> expressions;

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
        
        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            foreach (var expr in expressions)
                if (expr.Evaluate(env))
                    return new Bool(true);

            return new Bool(false);
        }

        public Or(List<Expression> expressions)
        {
            this.expressions = expressions;
        }
    }
}