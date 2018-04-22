namespace Interpreter
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
    }
}