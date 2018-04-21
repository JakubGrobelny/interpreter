public class Array : Value {

    private Expression[] array;

    public GetSize() {
        return this.array.Length;
    }

    public string ToString() {
        string result = "[";

        for (int i = 0; i < array.Length - 1; i++)
            result = result + array[i].ToString() + ", ";

        return result + array[array.Length - 1].ToString() +  "]";
    }

    public Expression this[int key] {
        get {
            if (key > array.Length) {

            }
            return rray[key];
        }
        set {
            array[key] = value;
        }
    }

    public Array (int size, Expression val) {
        this.array = new Expression[size];
        
        for (int i = 0; i < size; i++)
            this.array[i] = val;
    }

    public Array(int size) {
        this.array = new Expression[size];
    }
}