using System.Collections.Generic;
using Environment = Dictionary<Symbol, Expression>;

public abstract class Expression { //TODO: implement Cloneable?
    public abstract Expression Evaluate(Environment env);
    public abstract Expression Call(List<Expression> arguments, Environment env) {
        throw ApplicationNotAProcedure(""); 
    }
    public abstract String ToString();
}

public abstract class Combination : Expression {}

public abstract class Value : Expression {
    
    public abstract Expression Evaluate(Environment env) {
        return this;
    }
}

public abstract class Number : Value {}