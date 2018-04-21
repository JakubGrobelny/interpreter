public class Rational : Number
{
    private Integer numerator;
    private Integer denominator;

    public string ToString()
	{
        return numerator.ToString() + "/" + denominator.ToString();
    }

    public Rational(Integer numerator, Integer denominator)
	{
        this.numerator = numerator;
        this.denominator = denominator;
    }
}