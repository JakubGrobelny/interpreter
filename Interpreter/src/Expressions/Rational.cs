using System.Runtime.Serialization.Formatters;
using System.Text;

namespace Interpreter.Expressions
{
    public class Rational : Number
    {
        private Integer numerator;
        private Integer denominator;

        public override string ToString()
        {
            return numerator.ToString() + "/" + denominator.ToString();
        }

        public Rational(Integer numerator, Integer denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }

        public Rational(string str)
        {
            var strBuilder = new StringBuilder();

            foreach (var c in str)
            {
                if (c == '/')
                {
                    this.numerator = new Integer(strBuilder.ToString());
                    strBuilder.Clear();
                }
                else
                    strBuilder.Append(c);
            }
            
            this.denominator = new Integer(strBuilder.ToString());
        }
    }
}