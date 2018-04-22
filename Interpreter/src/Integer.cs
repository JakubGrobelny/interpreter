using System.Numerics;

namespace Interpreter
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
    }
}