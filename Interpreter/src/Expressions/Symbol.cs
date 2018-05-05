using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public class Symbol : Value
    {
        private string symbol;
        
        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            if (env.ContainsKey(this))
                return env[this];
            throw new UnboundVariable(ToString());
        }

        public Void Set(Dictionary<Symbol, Expression> env, Expression value)
        {
            if (env.ContainsKey(this))
            {
                env[this] = value;
                return Void.Instance;
            }
            throw new UnboundVariable(ToString());
        }
        
        public override object Clone() => new Symbol(symbol);

        public bool IsThis() => symbol == "this";
        
        public override string ToString()
        {
            return symbol;
        }

        public Symbol()
        {
            symbol = "";
        }
        
        public Symbol(string symbol)
        {
            this.symbol = symbol;
        }
    }
}