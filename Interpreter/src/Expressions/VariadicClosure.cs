using System.Collections.Generic;
using System.Linq;

namespace Interpreter.Expressions
{
    public class VariadicClosure : Closure
    {
        public override Expression Call(List<Expression> arguments, Dictionary<Symbol, Expression> env)
        {
            // There must an argument for every non-variadic parameter.
            if (arguments.Count < parameters.Count - 1)
                throw new ArityMismatch(ToString(), parameters.Count, arguments.Count);

            var extendedEnvironment = new Dictionary<Symbol, Expression>(env);

            // Adding local environment from closure to the environment.
            foreach (var entry in localEnvironment)
                extendedEnvironment[entry.Key] = entry.Value;

            // Adding passed arguments to the environment.
            for (var i = 0; i < parameters.Count - 1; i++)
                extendedEnvironment[parameters[i]] = arguments[i].Evaluate(env);

            Expression argList;

            // If there were no arguments for variadic parameter, then it is set to Null.
            if (parameters.Count == arguments.Count - 1)
                argList = Null.Instance;
            else
            {
                // Creating a list of arguments.
                var argTail = new List<Expression>();

                for (int i = parameters.Count - 1; i < arguments.Count; i++)
                    argTail.Add(arguments[i].Evaluate(env));
                argList = Pair.CreateList(argTail);
            }

            // Adding variadic argument to the environment,
            extendedEnvironment[parameters[parameters.Count - 1]] = argList;

            Expression val = null;
            foreach (var expr in expression)
                val = expr.Evaluate(extendedEnvironment);
            return val;
        }

        public VariadicClosure(string name, List <Symbol> parameters,
            List<Expression> expression, Dictionary<Symbol, Expression> localEnvironment)
            : base(name, parameters, expression, localEnvironment) {}
    }
}