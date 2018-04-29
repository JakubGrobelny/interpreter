using System;

namespace Interpreter.Expressions
{
    public class Real : Number
    {
        private double value;

        public double Value
        {
            get => value;
            set => this.value = value;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        protected override double ToDouble()
        {
            return value;
        }

        public bool IsPositive()
        {
            return value >= 0.0f;
        }

        public Real(double value)
        {
            this.value = value;
        }

        public Real(string str)
        {
            this.value = Double.Parse(str);
        }
    }
}