using System;

namespace Errors
{
    public class InternalException : Exception {}

    public class ArityMismatch : InternalException
    {
        public ArityMistmatch(string name, int arity, int received)
                : base(String.Format("{0}: arity mismatch!\n\
                                      Expected: {1}\n\
                                      Given:    {2}", name, arity, received)) {};
    }

    public class ApplicationNotAProcedure : InternalException
    {
        public ApplicationNotAProcedure(string application) 
                : base(application + " - not a procedure!") {};
    }

    public class ArrayIndexOutOfBounds : InternalException
    {
        public ArrayIndexOutOfBounds(int index, int max)
                : base(String.Format("Index out of bounds! ({0}/{1})", index, max)) {};
    }

    public class UnboundVariable : InternalException
    {
        public UnboundVariable(string symbol)
                : base("Unbound variable " + symbol + "!") {}
    }

    public class InvalidMember : InternalException
    {
        public InvalidMember(string object, string member)
                : base (object + " invalid member " + member + "!") {}
    }
}


