namespace Interpreter
{
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

        public override string ToString()
        {
            return "()";
        }

        private Null() {}
    }
}