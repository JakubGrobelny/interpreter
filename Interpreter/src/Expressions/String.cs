namespace Interpreter.Expressions
{
    public class StringLiteral : Value
    {
        private string value;

        public override string ToString()
        {
            return "\"" + value + "\"";
        }

        public StringLiteral(string value)
        {
            if (value.Length == 2)
                this.value = "";
            else
                this.value = value.Substring(1, value.Length - 2);
        }
    }
}