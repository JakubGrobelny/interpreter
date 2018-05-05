using System.Text;

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
            {
                var strBuilder = new StringBuilder();
                var str = value.Substring(1, value.Length - 2);

                bool backslash = false;
                foreach (var c in str)
                {
                    if (c == '\\')
                    {
                        if (backslash)
                        {
                            strBuilder.Append(c);
                            backslash = false;
                        }
                        else
                            backslash = true;

                        continue;
                    }

                    if (backslash)
                    {
                        backslash = false;
                        switch (c)
                        {
                            case 'n':
                                strBuilder.Append('\n');
                                break;
                            case 'b':
                                strBuilder.Append('\b');
                                break;
                            case 'r':
                                strBuilder.Append('\r');
                                break;
                            case 't':
                                strBuilder.Append('\t');
                                break;
                            case '"':
                                strBuilder.Append('"');
                                break;
                            default:
                                throw new InvalidStringCharacter(value, c);
                        }
                    }
                    else
                        strBuilder.Append(c);
                }

                this.value = strBuilder.ToString();
            }
        }

        public override object Clone() => new StringLiteral(value);
    }
}