using System;

namespace Errors {

    public class ArityMismatch : Exception {

        public ArityMistmatch(string name, int arity, int received)
                : base(String.Format("{0}: arity mismatch!\n\
                                      Expected: {1}\n\
                                      Given:    {2}", name, arity, received)) {};
    }

    public class ApplicationNotAProcedure : Exception {

        public ApplicationNotAProcedure(string application) 
                : base(application + " - not a procedure!") {};
    }

    public class ArrayIndexOutOfBounds : Exception {

        public ArrayIndexOutOfBounds(int index, int max)
                : base(String.Format("Index out of bounds! ({0}/{1})", index, max)) {};
    }

    public class UnboundVariable : Exception {
        public UnboundVariable(string symbol)
                : base("Unbound variable " + symbol + "!");
    }

}