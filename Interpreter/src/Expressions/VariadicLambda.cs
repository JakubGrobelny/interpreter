using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public class VariadicLambda : SpecialForm
    {
        private List<Symbol> parameters;
        private List<Expression> expression;
        
        public override string Keyword => "vlambda";

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
        
        public override Expression Evaluate(Dictionary<Symbol, Expression> env) =>
            new VariadicClosure(Keyword, parameters, expression, env);

        public VariadicLambda(List<Symbol> parameters, List<Expression> expression)
        {
            this.parameters = parameters;
            this.expression = expression;
        }
    }
}