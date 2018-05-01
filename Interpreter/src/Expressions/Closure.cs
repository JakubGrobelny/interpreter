using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Interpreter.Expressions
{
    public class Closure : Function
    {
        protected List<Symbol> parameters;
        protected List<Expression> expression;
        protected Dictionary<Symbol, Expression> localEnvironment;

        public override Expression Call(List<Expression> arguments, Dictionary<Symbol, Expression> env)
        {
            // Checking whether parameters match passed arguments.
            if (arguments.Count != parameters.Count)
                throw new ArityMismatch(ToString(), parameters.Count, arguments.Count);

            var extendedEnvironment = new Dictionary<Symbol, Expression>(env);

            // Adding local environment to the environment.
            foreach (var entry in localEnvironment)
                extendedEnvironment[entry.Key] = entry.Value;

            // Adding arguments to the environment.
            for (int i = 0; i < parameters.Count; i++)
                extendedEnvironment[parameters[i]] = arguments[i].Evaluate(env);

            Expression val = null;
            foreach (var expr in expression)
                val = expr.Evaluate(extendedEnvironment);

            return val;
        }

        public override string ToString()
        {
            return "#<procedure:" + name + ">";
        }

        public Closure(string name, List <Symbol> parameters,
            List<Expression> expression, Dictionary<Symbol, Expression> localEnvironment) 
            : base(name) 
        {
            this.parameters = parameters;
            this.expression = expression;
            this.localEnvironment = localEnvironment;
        }

        public override object Clone()
        {
            var exprClones = new List<Expression>();

            foreach (var expr in expression)
                exprClones.Append((Expression)expr.Clone());

            var paramClones = new List<Symbol>();

            foreach (var symbol in parameters)
                paramClones.Append((Symbol)symbol.Clone());

            var envClone = new Dictionary<Symbol, Expression>(localEnvironment);

            return new Closure(name, paramClones, exprClones, envClone);
        }
    }
}