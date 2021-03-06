using System.Data.Common;
using System.Numerics;
using System.Text;

namespace Interpreter.Expressions
{
    public class Rational : Number
    {
        private BigInteger numerator;
        private BigInteger denominator;

        public BigInteger Numerator
        {
            get => numerator;
            set
            {
                numerator = value;
                Normalize();
            }

        }

        public BigInteger Denominator
        {
            get => denominator;
            set
            {
                denominator = value;
                Normalize();
            }
        }

        public override string ToString()
        {
            return denominator != 1 && numerator != 0
                ? numerator + "/" + denominator
                : numerator.ToString();
        }

        protected override double ToDouble()
        {
            return (double) numerator / (double) denominator;
        }

        private static BigInteger GCD(BigInteger a, BigInteger b)
        {
            if (b == 0)
                return a;
            return GCD(b, a % b);
        }
        
        private void Normalize()
        {
            if (denominator == 0)
                throw new DivisionByZero(numerator, denominator);
            
            var gcd = GCD(BigInteger.Abs(numerator), BigInteger.Abs(denominator));
            
            if (gcd != 0)
            {
                numerator /= gcd;
                denominator /= gcd;
            }

            if (denominator < 0)
            {
                numerator *= -1;
                denominator *= -1;
            }
        }

        public static bool AreEqual(Rational a, Rational b)
            => a.Denominator == b.Denominator && a.Numerator == b.Numerator;

        public static Rational operator-(Rational a)
        {
            return new Rational(-a.Numerator, a.Denominator);
        }
        
        public static Rational Add(Rational a, Rational b)
        {
            var lcm = (a.Denominator * b.Denominator) / GCD(a.Denominator, b.Denominator);
            var newNum1 = a.Numerator / (lcm / a.Denominator);
            var newNum2 = b.Numerator / (lcm / b.Denominator);
            return new Rational(newNum1 + newNum2, lcm);
        }
        
        public static Rational Substract(Rational a, Rational b) => Add(a, -b);

        public static Rational Multiply(Rational a, Rational b)
        {
            return new Rational(a.Numerator * b.Numerator,
                                a.Denominator * b.Denominator);
        }

        public static Rational Divide(Rational a, Rational b)
        {
            if (b.Numerator == 0)
                throw new DivisionByZero(b.Numerator, b.Denominator);
            return new Rational(a.Numerator * b.Denominator,
                                a.Denominator * b.Numerator);

        }
        
        public Rational(BigInteger numerator, BigInteger denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
            Normalize();
        }

        public Rational(int val)
        {
            this.numerator = val;
            this.denominator = 1;
        }

        public Rational(string str)
        {
            var strBuilder = new StringBuilder();
            var fraction = false;

            foreach (var c in str)
            {
                if (c == '/')
                {
                    fraction = true;
                    this.numerator = BigInteger.Parse(strBuilder.ToString());
                    strBuilder.Clear();
                }
                else
                    strBuilder.Append(c);
            }


            if (fraction)
            {
                var den = strBuilder.ToString();
                this.denominator = den.Length > 0 ? BigInteger.Parse(den) : 1;
            }
            else
            {
                var num = strBuilder.ToString();
                this.numerator = BigInteger.Parse(num);
                this.denominator = 1;
            }
            Normalize();
        }

        public override object Clone() => new Rational(numerator, denominator);
    }
}