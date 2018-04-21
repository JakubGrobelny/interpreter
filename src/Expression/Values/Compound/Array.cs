public class Array : Value
{
    private Expression[] array;

    public GetSize()
    {
        return array.Length;
    }

    public string ToString()
    {
        string result = "[";

        for (int i = 0; i < array.Length - 1; i++)
            result = result + array[i].ToString() + ", ";

        return result + array[array.Length - 1].ToString() +  "]";
    }

    public Expression this[int key]
    {
        get
        {
            if (key > array.Length)
                throw ArrayIndexOutOfBounds(key, array.Length);
            return array[key];
        }
        set
        {
            array[key] = value;
        }
    }

    public Array (int size, Expression val)
    {
        array = new Expression[size];
        
        for (int i = 0; i < size; i++)
            array[i] = val;
    }

    public Array(int size)
    {
        array = new Expression[size];
    }
}