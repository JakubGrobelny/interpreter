using System.Collections.Generic;

namespace Interpreter.Expressions
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

        private string ToStringHelper()
        {
            var result = first.ToString();

            if (second is Pair p)
                result += " " + p.ToStringHelper();
            else if (!(second is Null))
                result += " " + second.ToString();

            return result;
        }

        public override string ToString()
        {
            return "(" + ToStringHelper() + ")";
        }

        public static Value CreateList(List<Expression> elements)
        {
            if (elements.Count == 0)
                return Null.Instance;
            Pair list = new Pair(elements[0], Null.Instance);
            Pair ptr = list;

            for (int i = 1; i < elements.Count; i++)
            {
                ptr.second = new Pair(elements[i], Null.Instance);
                ptr = ptr.second as Pair;
            }

            return list;
        }

        public static Bool IsList(Expression list)
        {
            if (list is Null || (list is Pair p && (bool)IsList(p.second)))
                return new Bool(true);
            return new Bool(false);
        }
        
        public Pair(Expression first, Expression second)
        {
            this.first = first;
            this.second = second;
        }

        public override object Clone() => new Pair((Expression)first.Clone(),
                                                   (Expression)second.Clone());
    }
}