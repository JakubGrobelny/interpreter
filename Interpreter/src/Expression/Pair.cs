using System.Collections.Generic;

namespace Interpreter.Expression
{
    public class Pair : Value
    {
    
        private Expression first;
        private Expression second;

        public Expression First
        {
            get => first;
            set => first = value;
        }

        public Expression Second
        {
            get => second;
            set => second = value;
        }

        public override string ToString()
        {
            if (second is Pair)
                return "(" + first.ToString() + " " + second.ToString() + ")";
            else if (second is Null)
                return "(" + first.ToString() + ")";
            else
                return "(" + first.ToString() + " . " + second.ToString() + ")";
        }

        public static Value CreateList(List<Expression> elements)
        {
            if (elements.Count == 0)
                return Null.Instance;
            else
            {
                Pair list = new Pair(elements[0], Null.Instance);
                Pair ptr = list;
            
                for (int i = 1; i < elements.Count; i++)
                {
                    ptr.second = new Pair(elements[i], Null.Instance);
                    ptr = ptr.second as Pair;
                }

                return list;
            }
        }

        public Pair(Expression first, Expression second)
        {
            this.first = first;
            this.second = second;
        }
    }
}