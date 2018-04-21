using System;

namespace Errors {

    public class ArityMismatch : Exception {

        public ArityMistmatch(string name, int arity, int received)
                : base(name ": arity mismatch!\
                        \nExpected: " + arity.ToString() +
                    "\nGiven: " + received.ToString()) {};
    }

    public class ApplicationNotAProcedure : Exception {

        public ApplicationNotAProcedure(string application) 
                : base(application + " - not a procedure!") {};
    }


    public class ArrayIndexOutOfBounds : Exception {

        public ArrayIndexOutOfBounds(int index, int max)
                : base("Index out of bounds! (" + index.ToString() + 
                       "/" + max.ToString() ")") {};
    }

}