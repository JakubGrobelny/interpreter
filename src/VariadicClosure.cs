using System.Collections.Generic;
using Environment = Dictionary<Symbol, Expression>;

public class VariadicClosure : Closure {

    //TODO:
    //TODO:
    //TODO:

    public Expression Call(List<Expression> arguments, Environment env) {

    }

    public VariadicClosure(string name, List <Symbol> parameters,
                   Expression expression, Environment localEnvironment)
                   : base(name, parameters, expression, localEnvironment) {};
}