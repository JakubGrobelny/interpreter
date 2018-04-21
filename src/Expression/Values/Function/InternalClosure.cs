using System.Collections.Generic;
using Environment = Dictionary<Symbol, Expression>;

delegate Expression InternalFunction(List<Expression> arguments, Environment env);

public class InternalClosure : Function
{

    InternalFunction procedure;

    public Expression Call(List<Expression> arguments, Environment env)
    {
        return procedure(arguments, env);
    }

    public InternalClosure(string name, InternalFunction function)
                           : base(name), procedure(function) {};
}