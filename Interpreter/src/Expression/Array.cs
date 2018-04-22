namespace Interpreter.Expression
{
    public class Array : Value
    {
        private Expression[] array;

        public int GetSize()
        {
            return array.Length;
        }

        public override string ToString()
        {
            var result = "[";

            for (var i = 0; i < array.Length - 1; i++)
                result = result + array[i].ToString() + ", ";

            return result + array[array.Length - 1].ToString() +  "]";
        }

        public Expression this[int key]
        {
            get
            {
                if (key > array.Length)
                    throw new ArrayIndexOutOfBounds(key, array.Length);
                return array[key];
            }
            set => array[key] = value;
        }

        public Array (int size, Expression val)
        {
            array = new Expression[size];
        
            for (var i = 0; i < size; i++)
                array[i] = val;
        }

        public Array(int size)
        {
            array = new Expression[size];
        }
    }
}