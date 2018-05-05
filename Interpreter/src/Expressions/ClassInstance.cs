using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public class ClassInstance : Value
    {
        private Dictionary<Symbol, Expression> objectEnvironment;
        private Symbol className;
        
        public override string ToString() => "#<instance-of:" + className + ">";

        public void SetMember(Symbol member, Expression value)
        {
            if (!objectEnvironment.ContainsKey(member))
                throw new InvalidMember(ToString(), member.ToString());

            objectEnvironment[member] = value;
        }
        
        public Expression GetMember(Symbol memberName, Dictionary<Symbol, Expression> env)
        {
            if (memberName.IsThis())
                return this;
            
            if (!objectEnvironment.ContainsKey(memberName))
                throw new InvalidMember(ToString(), memberName.ToString());

            var extendedEnv = new Dictionary<Symbol, Expression>(env);
            
            foreach (var definition in objectEnvironment)
                extendedEnv[definition.Key] = definition.Value;
            
            return objectEnvironment[memberName].Evaluate(extendedEnv);
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

        private ClassInstance(Symbol className, Dictionary<Symbol, Expression> objEnv)
        {
            this.className = className;
            this.objectEnvironment = objEnv;
        }
        
        public override object Clone()
        {
            var name = (Symbol)className.Clone();
            var objEnv = new Dictionary<Symbol, Expression>(objectEnvironment);
            return new ClassInstance(name, objEnv);
        }
    }
}