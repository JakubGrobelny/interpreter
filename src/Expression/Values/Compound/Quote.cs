public class Quote : Value
{
    private Expression expr;

    public Expression Evaluate(Environment env)
    {
        return expr;
    }

    public string ToString()
    {
        return "'" + expr.ToString();
    }

    public Quote(Expression expr)
    {
        this.expr = expr;
    }
}