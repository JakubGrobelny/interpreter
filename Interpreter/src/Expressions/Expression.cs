using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public abstract class Expression
    { //TODO: implement Cloneable?
        public abstract Expression Evaluate(Dictionary<Symbol, Expression> env);
        public abstract string ToString();
    }

    public abstract class Value : Expression
    {
        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            return this;
        }
    }

    public abstract class Combination : Expression {}

    public abstract class Number : Value {}
}