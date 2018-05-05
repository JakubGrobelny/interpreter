using System.Collections.Generic;
using System.Linq;

namespace Interpreter.Expressions
{
    public class CompoundSymbol : Symbol
    {
        private List<Expression> components;

        public override string ToString()
        {
            var result = "";
            for (int i = 0; i < components.Count - 1; i++)
                result += components[i].ToString() + ".";
            return result + components[components.Count - 1];
        }

        public new Void Set(Dictionary<Symbol, Expression> env, Expression value)
        {
            var finalExpr = components[0].Evaluate(env);
            
            for (int i = 1; i < components.Count - 1; i++)
            {
                var symbol = components[i].Evaluate(env);
                
                if (finalExpr is Array && symbol is Rational index)
                    finalExpr = ((Array)finalExpr)[index].Evaluate(env);
                else if (finalExpr is ClassInstance && symbol is Symbol sym)
                    finalExpr = ((ClassInstance)finalExpr).GetMember(sym, env);
                else
                    throw new InvalidCompoundSymbolElement(symbol.ToString());
            }

            var last = components[components.Count - 1].Evaluate(env);
            
            if (finalExpr is Array aExpr)
            {
                if (!(last is Rational index))
                    throw new InvalidArrayIndex(last.ToString());
                aExpr[index] = value;
            }
            else if (finalExpr is ClassInstance cExpr)
            {
                if (!(last is Symbol symbol))
                    throw new InvalidMember(finalExpr.ToString(), last.ToString());
                
                cExpr.SetMember(symbol, value);
            }
            else
                throw new InvalidCompoundSymbolElement(finalExpr.ToString());
            
            return Void.Instance;
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
                    finalExpr = ((ClassInstance)finalExpr).GetMember(sym, env);
                else
                    throw new InvalidCompoundSymbolElement(symbol.ToString());
            }

            return finalExpr;
        }

        public override object Clone()
        {
            var clones = new List<Expression>();

            foreach (var component in components)
                clones.Append((Expression)component.Clone());

            return new CompoundSymbol(clones);
        }

        public CompoundSymbol(List<Expression> components)
        {
            this.components = components;
        }
    }
}