using System.Collections.Generic;

namespace Interpreter.Expression
{
    public abstract class Function : Value
    {
        protected string name;

        public abstract Expression Call(List<Expression> arguments, 
                                        Dictionary<Symbol, Expression> env);

        public override string ToString()
        {
            return "#<procedure:" + name + ">";
        }

        public Function(string name)
        {
            this.name = name;
        }

    }
}