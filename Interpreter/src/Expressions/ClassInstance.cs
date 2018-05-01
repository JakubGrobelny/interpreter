using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public class ClassInstance : Value
    {
        private Dictionary<Symbol, Expression> objectEnvironment;
        private Symbol className;
        
        public override string ToString() => "#<instance-of:" + className + ">";
        
        public Expression GetMember(Symbol memberName)
        {
            if (memberName.IsThis())
                return this;
            
            if (!objectEnvironment.ContainsKey(memberName))
                throw new InvalidMember(ToString(), memberName.ToString());

            return objectEnvironment[memberName];
        }

        public ClassInstance(Class type, List<Expression> constructor)
        {
            className = type.Name;
            
            if (constructor.Count != type.ConstructorParameters.Count)
                throw new InvalidClassInstantiation(type.Name.ToString());

            objectEnvironment = new Dictionary<Symbol, Expression>(type.Members);
            
            for (int i = 0; i < constructor.Count; i++)
                objectEnvironment[type.ConstructorParameters[i]] = constructor[i];
        }
    }
}