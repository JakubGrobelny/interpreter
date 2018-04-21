using System.Collections.Generic;
using Environment = Dictionary<Symbol, Expression>;

public class VariadicClosure : Closure {

    public Expression Call(List<Expression> arguments, Environment env) {
        
        // There must an argument for every non-variadic parameter.
        if (arguments.Length < parameters.Length - 1) {
            throw ArityMismatch(ToString(), parameters.Length, arguments.Length);
        }

        var extendedEnvironment = new Environment(env);

        // Adding local environment from closure to the environment.
        foreach (var entry in localEnvironment) {
            extendedEnvironment[entry.Key] = entry.Value;
        }

        // Adding passed arguments to the environments.
        for (int i = 0; i < parameters.Length - 1; i++) {
            extendedEnvironment[parameters[i]] = arguments[i].Evaluate(env);
        }

        Expression argList;

        // If there were no arguments for variadic parameter, then it is set to Null.
        if (parameters.Length == arguments.Length - 1) {
            argList = Null.Instance;
        }
        else {
            // Creating a list of arguments.
            List<Expression> argTail = arguments.Skip(parameters.Length - 1);
            argList = Pair.CreateList(argTail);
        }

        // Adding variadic argument to the environment,
        extendedEnvironment[parameters[parameters.Length - 1]] = argList;

        return expression.Evaluate(extendedEnvironment);

    }

    public VariadicClosure(string name, List <Symbol> parameters,
                   Expression expression, Environment localEnvironment)
                   : base(name, parameters, expression, localEnvironment) {};
}