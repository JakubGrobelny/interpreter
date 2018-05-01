using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public class CompoundSymbol : Value
    {
        private List<Expression> components;

        public override string ToString()
        {
            var result = "";
            for (int i = 0; i < components.Count - 1; i++)
                result += components[i].ToString() + ".";
            return result + components[components.Count - 1];
        }
        
        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            var finalExpr = components[0].Evaluate(env);
            
            for (int i = 1; i < components.Count; i++)
            {
                var symbol = components[i].Evaluate(env);
                
                if (finalExpr is Array && symbol is Rational index)
                    finalExpr = ((Array)finalExpr)[index].Evaluate(env);
                else if (finalExpr is ClassInstance && symbol is Symbol sym)
                    finalExpr = ((ClassInstance)finalExpr).GetMember(sym).Evaluate(env);
                else
                    throw new InvalidCompoundSymbolElement(symbol.ToString());
            }

            return finalExpr;
        }

        public CompoundSymbol(List<Expression> components)
        {
            this.components = components;
        }
    }
}