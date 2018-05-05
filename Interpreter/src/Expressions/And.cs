using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Interpreter.Expressions
{
    public class And : SpecialForm
    {
        private List<Expression> expressions;
        
        public override string Keyword => "and";

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
        
        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            foreach (var expr in expressions)
                if (!expr.Evaluate(env))
                    return new Bool(false);

            return new Bool(true);
        }

        public And(List<Expression> expressions)
        {
            this.expressions = expressions;
        }
    }
}