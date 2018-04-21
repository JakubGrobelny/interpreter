using System.Collections.Generic;
using Environment = Dictionary<Symbol, Expression>;

public class Closure : Function {

    private List<Symbol> parameters;
    private Expression expression;
    private Environment localEnvironment;

    public Expression Call(List<Expression> arguments, Environment env) {

        if (arguments.Length != parameters.Length) {
            throw ArityMismatch(ToString(), parameters.Length, arguments.Length);
        }

        var extendedEnvironment = new Environment(env);
        
        foreach (var entry in localEnvironment) {
            extendedEnvironment[entry.Key] = entry.Value;
        }

        for (int i = 0; i < parameters.Length; i++) {
            extendedEnvironment[parameters[i]] = arguments[i].Evaluate(env);
        }

        return expression.Evaluate(extendedEnvironment);
    }

    public string ToString() {
        return "#<procedure:" + name + ">";
    }

    public Closure(string name, List <Symbol> parameters,
                   Expression expression, Environment localEnvironment) 
                   : base(name) 
    {
        this.parameters = parameters;
        this.expression = expression;
        this.localEnvironment = localEnvironment;
        this.arity = parameters.Length;
    }
}