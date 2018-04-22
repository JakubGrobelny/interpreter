namespace Interpreter
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
    }
}