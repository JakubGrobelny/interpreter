using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public delegate Expression InternalFunction(List<Expression> arguments, 
                                         Dictionary<Symbol, Expression> env);

    public class InternalClosure : Function
    {
        private InternalFunction procedure;

        public override Expression Call(List<Expression> arguments, Dictionary<Symbol, Expression> env)
        {
            return procedure(arguments, env);
        }
        
        public InternalClosure(string name, InternalFunction function)
            : base(name)
        {
            this.procedure = function;
        }
    }
}