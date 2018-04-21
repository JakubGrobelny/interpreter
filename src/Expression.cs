using System.Collections.Generic;

public abstract class Expression {
    public abstract Expression Evaluate(Environment env);
    public abstract String     ToString();
}

public abstract class Combination : Expression {}

public abstract class Value : Expression {
    
    public abstract Expression Evaluate(Environment env) {
        return this;
    }
}

public abstract class Number : Value {}