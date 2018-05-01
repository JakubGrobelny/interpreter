namespace Interpreter.Expressions
{
    public class Bool : Value
    {
        private bool value;
        
        public bool GetValue()
        {
            return value;
        }

        public override string ToString()
        {
            return value ? "#t" : "#f";
        }

        public Bool(string str)
        {
            this.value = str == "#t";
        }
        
        public Bool(bool value)
        {
            this.value = value;
        }

        public override object Clone() => new Bool(value);
    }
}