using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public class ClassInstance
    {
        //TODO: redo
        private Dictionary<Symbol, Expression> members;
        private Class type;
        
        public ClassInstance(Class type, List<Expression> constructor)
        {
        }
    }
}