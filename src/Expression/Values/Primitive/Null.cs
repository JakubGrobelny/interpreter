public class Null : Value
{
    private static Null instance = null;
    
    public static Null Instance
    {
        get
        {
            if (instance == null)
                instance = new Null();
            return instance;
        }
    }

    //TODO: Singleton
    public string ToString()
    {
        return "()";
    }

    private Null() {}
}