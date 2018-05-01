using System.Collections.Generic;

namespace Interpreter.Expressions
{    
    public class Application : Combination
    {
        private readonly Expression procedure;
        private readonly List<Expression> arguments;

        public override object Clone()
        {
            throw new System.NotImplementedException("Application cloning!");
        }

        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            var proc = procedure.Evaluate(env) as Function;
        
            if (proc == null)
                throw new ApplicationNotAProcedure(ToString());
            else
                return proc.Call(arguments, env);
        }

        public override string ToString()
        {
            string result = "(" + procedure.ToString();
        
            for (int i = 0; i < arguments.Count - 1; i++)
                result = result + arguments[i].ToString() + " ";

            return result + arguments[arguments.Count - 1] + ")";
        }

        public Application(Expression procedure, List<Expression> arguments)
        {
            this.procedure = procedure;
            this.arguments = arguments;
        }
    }
}