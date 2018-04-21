using System.Collections.Generic;
using Environment = Dictionary<Symbol, Expression>;

using Errors;

public class Application : Combination {

    private Expression procedure;
    private List<Expression> arguments;

    public Expression Evaluate(Environment env) {

        Expression proc = procedure.Evaluate(env);

        try {
            return proc.Call(arguments, env);
        }
        catch (ApplicationNotAProcedure) {
            throw ApplicationNotAProcedure(ToString());
        }
    }

    public string ToString() {
        string result = "(" + procedure.ToString();
        
        for (int i = 0; i < arguments.Length - 1; i++) {
            result = result + arguments[i].ToString() + " ";
        }

        return result + arguments[arguments.Length - 1] + ")";
    }

    public Application(Expression procedure, List<Expression> arguments, int line) {
        this.procedure = procedure;
        this.arguments = arguments;
        this.line = line;
    }
}