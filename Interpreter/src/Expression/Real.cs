namespace Interpreter.Expression
{
    public class Real : Number
    {
        private double value;

        public override string ToString()
        {
            return value.ToString();
        }

        public bool IsPositive()
        {
            return value >= 0.0f;
        }

        public Real(double value)
        {
            this.value = value;
        }
    }
}