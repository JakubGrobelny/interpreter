using System.Text;

namespace Interpreter.Expressions
{
    public class Complex : Number
    {
        Real real;
        Real imaginary;

        public override string ToString()
        {
            if (imaginary.IsPositive())
                return real.ToString() + "+" + imaginary.ToString() + "i";
            else
                return real.ToString() + imaginary.ToString() + "i";
        }

        public Complex(Real real, Real imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }

        public Complex(string str)
        {
            var strBuilder = new StringBuilder();

            foreach (var c in str)
            {
                if (c == '+' || (c == '-' && strBuilder.Length != 0))
                {
                    this.real = new Real(strBuilder.ToString());
                    strBuilder.Clear();
                }
                else if (c == 'i')
                    break;
                else
                    strBuilder.Append(c);
            }
            
            this.imaginary = new Real(strBuilder.ToString());             
        }
    }
}