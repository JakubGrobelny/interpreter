using System.Collections.Generic;
using Errors;

public class Application : Combination {

    private Expression procedure;
    private List<Expression> arguments;

    public Expression Evaluate(Environment env) {
        Closure closure = procedure.Evaluate(env);

        if (!(closure is Closure))
            throw ApplicationNotAProcedure(ToString());

        List<Symbol> formalParameters = closure.GetParameters();

        if (formalParameters.Length != arguments.Length)
            throw ArityMismatch(closure.ToString(),
                                formalParameters.Length,
                                arguments.Length);

        var extendedEnv = new Dictionary<Symbol, Expression>(env);
        var localEnv = closure.GetEnv();

        foreach(var entry in localEnv) {
            extendedEnv[entry.Key] = entry.Value;
        }

        for (int i = 0; i < formalParameters.Length; i++) {
            extendedEnv[formalParameters[i]] = arguments[i].Evaluate(env);
        }

        return closure.GetExpression().Evaluate(extendedEnv);
    }

    public string ToString() {
        string result = "(" + procedure.ToString();
        
        for (int i = 0; i < arguments.Length - 1; i++)
            result = result + arguments[i].ToString() + " ";
        

        return result + arguments[arguments.Length - 1] + ")";
    }

    public Application(Expression procedure, List<Expression> arguments, int line) {
        this.procedure = procedure;
        this.arguments = arguments;
        this.line = line;
    }
}