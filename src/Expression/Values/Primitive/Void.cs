public class Void : Value
{
    private static Void instance = null;

    public static Void Instance
    {
        get
        {
            if (instance == null)
                instance = new Void();
            return instance;
        }
    }

    
    public string ToString()
    {
        return "";
    }

    private Void() {}
}