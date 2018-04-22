namespace Interpreter.Expression
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
            this.value = value;
        }
    }
}