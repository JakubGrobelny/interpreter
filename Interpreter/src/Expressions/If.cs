using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public class If : SpecialForm
    {
        private Expression condition;
        private Expression ifTrue;
        private Expression ifFalse;
        
        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
        
        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            if (condition.Evaluate(env))
                return ifTrue.Evaluate(env);
            return ifFalse.Evaluate(env);
        }

        public If(Expression condition, Expression ifTrue, Expression ifFalse)
        {
            this.condition = condition;
            this.ifTrue = ifTrue;
            this.ifFalse = ifFalse;
        }
    }
}