using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public class CompoundSymbol
    {
        private List<Expression> components;

        public CompoundSymbol(List<Expression> components)
        {
            foreach (var component in components)
            {
                if (!(component is Symbol) || !(component is Rational))
                    throw new InvalidCompoundSymbolElement(component.ToString());
            }
        }
    }
}