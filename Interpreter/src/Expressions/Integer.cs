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

        public Integer(string str)
        {
            value = BigInteger.Parse(str);
        }
    }
}