using System;
using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public class Lambda : SpecialForm
    {        
        private List<Symbol> parameters;
        private List<Expression> expression;

        public override Expression Evaluate(Dictionary<Symbol, Expression> env) =>
            new Closure("lambda", parameters, expression, env);

        public override string ToString()
        {
            throw new NotImplementedException();
        }
        
        //TODO: support for variadic functions
        public Lambda(List<Symbol> parameters, List<Expression> expression)
        {
            this.parameters = parameters;
            this.expression = expression;
        }
    }
}