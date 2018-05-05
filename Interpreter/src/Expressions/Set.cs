using System.Collections.Generic;
using System.Xml.Schema;

namespace Interpreter.Expressions
{
    public class Set : SpecialForm
    {
        private Symbol variable;
        private Expression value;

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
        
        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            return variable.Set(env, value.Evaluate(env));
        }

        public Set(Symbol variable, Expression value)
        {
            this.variable = variable;
            this.value = value;
        }
    }
}