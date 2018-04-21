using System.Collections.Generic;
using Environment = Dictionary<Symbol, Expression>;

public class Symbol : Value
{

    private string symbol;

    public Expression Evaluate(Environment env)
    {
        try
        {
            return env[symbol];
        }
        catch
        {
            throw UnboundVariable(ToString());
        }
    }

    public string ToString()
    {
        return symbol;
    }

    public Symbol(string symbol)
    {
        this.symbol = symbol;
    }
}