using System.Numerics;

public class Integer : Number
{
    private BigInteger value;

    public string ToString()
	{
        return value.ToString();
    }

    public Integer(BigInteger value)
	{
        this.value = value;
    }
}