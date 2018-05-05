using System;
using System.Numerics;
using static System.String;

namespace Interpreter
{
    //TODO: split into Runtime and Parsing errors
    // Errors in user's code.
    public class InternalException : Exception
    {
        protected InternalException(string what)
            : base(what) {}
    }

    public class SpecialFormArityMismatch : InternalException
    {
        public SpecialFormArityMismatch(string form, int arity, int received)
            : base(String.Format(("Error! Invalid use of {0} (arity mismatch)\n" +
                                  "Expected {1}, received {2}"), form, arity, received)) {}
    }

    public class InvalidUseOfSpecialForm : InternalException
    {
        public InvalidUseOfSpecialForm(string form, string expr)
            : base("Error! Invalid use of " + form + " in " + expr + " !") {}
    }
    
    public class InvalidClassInstantiation : InternalException
    {
        public InvalidClassInstantiation(string name)
            : base("Error! Invalid instantiation of class " + name + "!") {}
    }
    
    public class InvalidArrayIndex : InternalException
    {
        public InvalidArrayIndex(string a)
            : base("Error! Invalid array index " + a + "!") {}
    }

    public class InvalidArraySize : InternalException
    {
        public InvalidArraySize(string a)
            : base("Invalid array size " + a + "!") {}
    }
    
    public class DivisionByZero : InternalException
    {
        public DivisionByZero(BigInteger a, BigInteger b)
            : base(Format("Division by zero {0}/{1}!", a, b)) {}
    }

    public class InvalidStringCharacter : InternalException
    {
        public InvalidStringCharacter(string raw, char c)
            : base(String.Format("Invalid character {0} in string literal {1}!", c, raw)) {}
    }

    public class InvalidUseOfScopeOperator : InternalException
    {
        public InvalidUseOfScopeOperator(string expr)
            : base("Invalid use of scope generator in " + expr +
                   "!\nDid you forget to put '$' at the beginning?") {}
    }
    
    public class InvalidCompoundSymbolElement : InternalException
    {
        public InvalidCompoundSymbolElement(string symbol)
            : base("Invalid compound symbol element " + symbol + "!") {}
    }
    
    public class ArityMismatch : InternalException
    {
        public ArityMismatch(string name, int arity, int received)
            : base(Format("{0}: arity mismatch!\n " +
                                 "Expected: {1}\n Given: {2}",
                                  name, arity, received)) {}
    }

    public class InvalidArgument : InternalException
    {
        public InvalidArgument(string function, string arg)
            : base("Illegal argument " + arg + " for " + function + "!") {}
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

    public class InvalidExpression : InternalException
    {
        public InvalidExpression(string expression)
            : base("Invalid expression " + expression + "!") {}
    }
    
    // Interpreter errors.
    public class UnhandledLexerException : Exception
    {
        public UnhandledLexerException(string what)
            : base("CRITICAL ERROR! Unhandled lexer exception! " + what) {}
    }
}


