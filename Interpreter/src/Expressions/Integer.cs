using System.Numerics;

namespace Interpreter.Expressions
{
    public class Integer : Number
    {
        private BigInteger value;

        public override string ToString()
        {
            return value.ToString();
        }

        public Integer(BigInteger value)
        {
            this.value = value;
        }

        private static BigInteger GCD(BigInteger a, BigInteger b)
        {
            if (b == 0)
                return a;
            return GCD(b, a % b);
        }
        
        public static Integer GCD(Integer a, Integer b)
        {
            return new Integer(GCD(a.value, b.value));
        }

        public static bool operator==(Integer a, BigInteger b) =>
            a.value == b;

        public static bool operator!=(Integer a, BigInteger b) =>
            !(a == b);

        public static bool operator==(Integer a, Integer b) =>
            a.value == b.value;

        public static bool operator!=(Integer a, Integer b) =>
            !(a == b);

        public static Integer operator/(Integer a, Integer b)
        {
            if (b == 0)
                throw new DivisionByZero(a.value, b.value);
            return new Integer(a.value / b.value);
        }
        
        public Integer(string str)
        {
            value = BigInteger.Parse(str);
        }
    }
}