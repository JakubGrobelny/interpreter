﻿using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public class Definition : SpecialForm
    {
        private Symbol symbol;
        private Expression expression;

        public override string ToString()
        {
            return "(def " 
                       + symbol.ToString() 
                       + " " 
                       + expression.ToString() + ")";
        }
        
        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            //env.Add(symbol, expression.Evaluate(env));
            env[symbol] = expression.Evaluate(env);
            return Void.Instance;
        }

        public Definition(Symbol symbol, Expression expression)
        {
            this.symbol = symbol;
            this.expression = expression;
        }
    }
}