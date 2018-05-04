using System.Collections.Generic;
using System.Linq;

namespace Interpreter.Expressions
{
    public class ArrayDefinition : SpecialForm
    {
        private Symbol name;
        private Expression size;
        private List<Expression> values;
        private bool fill;

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
        
        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            var evaluatedSize = size.Evaluate(env);
            
            if (!(evaluatedSize is Rational))
                throw new InvalidArraySize(evaluatedSize.ToString());

            var rationalSize = (Rational)size;
            
            if (rationalSize.Denominator != 1 || 
                rationalSize.Numerator <= 0   || 
                rationalSize.Numerator > int.MaxValue)
                throw new InvalidArraySize(size.ToString());

            int intSize = (int)rationalSize.Numerator;
            
            if (fill)
            {
                var val = values[0].Evaluate(env);
                for (int i = 1; i < intSize; i++)
                    values.Add(val);
            }
            // Filling the rest of array with void if neccessary
            else
            {
                for (int i = 0; i < values.Count; i++)
                    values[i] = values[i].Evaluate(env);

                while (values.Count != intSize)
                    values.Add(Void.Instance);
            }

            return new Array(values.ToArray());
        }

        public ArrayDefinition(Symbol name, Rational size, List<Expression> values)
        {
            this.name = name;
            this.size = size;
            this.values = values;
            this.fill = false;
        }
        
        public ArrayDefinition(Symbol name, Rational size, Expression defaultVal)
        {
            this.name = name;
            this.size = size;
            this.fill = true;
            values = new List<Expression> {defaultVal};
        }

        public ArrayDefinition(Symbol name, Rational size)
            : this(name, size, Void.Instance) {}
    }
}