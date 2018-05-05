using System;
using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public class Lambda : SpecialForm
    {
        public override string Keyword => "lambda";
        
        private List<Symbol> parameters;
        private List<Expression> expression;

        public override Expression Evaluate(Dictionary<Symbol, Expression> env) =>
            new Closure(Keyword, parameters, expression, env);

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