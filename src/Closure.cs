using System.Collections.Generic;

public class Closure : Value {

    private string name;
    private List<Symbol> parameters;
    private Expression expression;
    private Environment localEnvironment;

    public List<Symbol> GetParameters() {
        return parameters;
    }

    public Environment GetEnv() {
        return localEnvironment;
    }

    public Expression GetExpression() {
        return expression;
    }

    public string ToString() {
        return "#<procedure:" + name + ">";
    }

    public Closure(string name, List <Symbol> parameters,
                   Expression expression, Environment localEnvironment) {
        this.name = name;
        this.parameters = parameters;
        this.expression = expression;
        this.localEnvironment = localEnvironment;
    }
}