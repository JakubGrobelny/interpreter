using System;
using static System.String;

namespace Interpreter
{
    // Errors in user's code.
    public class InternalException : Exception
    {
        protected InternalException(string what)
            : base(what) {}
    }

    public class ArityMismatch : InternalException
    {
        public ArityMismatch(string name, int arity, int received)
            : base(Format("{0}: arity mismatch!\n " +
                                 "Expected: {1}\n Given: {2}",
                                  name, arity, received)) {}
    }

    public class ApplicationNotAProcedure : InternalException
    {
        public ApplicationNotAProcedure(string application) 
            : base(application + " - not a procedure!") {}
    }

    public class ArrayIndexOutOfBounds : InternalException
    {
        public ArrayIndexOutOfBounds(int index, int max)
            : base(Format("Index out of bounds! ({0}/{1})", index, max)) {}
    }

    public class UnboundVariable : InternalException
    {
        public UnboundVariable(string symbol)
            : base("Unbound variable " + symbol + "!") {}
    }

    public class InvalidMember : InternalException
    {
        public InvalidMember(string obj, string member)
            : base (obj + " invalid member " + member + "!") {}
    }

    public class ParenthesisError : InternalException
    {
        public ParenthesisError(char brace) 
            : base("Unmatched '" + brace + "' brace!") {}
    }

    public class InvalidEmptyExpression : InternalException
    {
        public InvalidEmptyExpression()
            : base("Empty expression is not valid!") {}
    }
    
    // Interpreter errors.
    public class UnhandledLexerException : Exception
    {
        public UnhandledLexerException(string what)
            : base("Unhandled lexer exception! " + what) {}
    }
}


