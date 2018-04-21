public abstract class Function : Value
{
    private string name;

    public abstract Expression Call(List<Expression> arguments, Environment env);

    public string ToString()
    {
        return "#<procedure:" + name + ">";
    }

    public Function(string name)
    {
        this.name = name;
    }

}