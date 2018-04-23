using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public class Symbol : Value
    {
        private string symbol;
        
        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            try
            {
                return env[this];
            }
            catch
            {
                throw new UnboundVariable(ToString());
            }
        }
        
        public override string ToString()
        {
            return symbol;
        }

        public Symbol(string symbol)
        {
            this.symbol = symbol;
        }
    }
}