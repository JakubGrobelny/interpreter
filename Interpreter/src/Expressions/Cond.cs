using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public class Cond : SpecialForm
    {
        private List<List<Expression>> clauses;
        
        public override string Keyword => "cond";

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
        
        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            foreach (var clause in clauses)
                if ((bool)clause[0].Evaluate(env))
                    return clause[1].Evaluate(env);
            
            return Void.Instance;
        }

        public Cond(List<List<Expression>> clauses)
        {
            this.clauses = clauses;
        }
    }
}