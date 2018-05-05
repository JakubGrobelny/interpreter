using System;
using System.Collections.Generic;
using System.Linq;

namespace Interpreter.Expressions
{
    public class ClassDefinition : SpecialForm
    {        
        private Symbol envObjectName;
        private Class definedClass;
        
        public override string ToString()
        {
            throw new NotImplementedException("defclass to string");
        }

        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            env[envObjectName] = definedClass;
            return Void.Instance;
        }

        public ClassDefinition(Symbol name, List<Symbol> constructor, 
                               List<Symbol> memberNames, List<Expression> memberValues)
        {
            this.envObjectName = name;
            
            var members = new Dictionary<Symbol, Expression>();
            // member names and values cound should've been checked by parser so
            // there is no point in doing it now.

            for (int i = 0; i < memberNames.Count; i++)
                members[memberNames[i]] = memberValues[i];

            definedClass = new Class(name, members, constructor);
        }

    }
}