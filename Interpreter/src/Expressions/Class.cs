using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public class Class : Expression
    {
        private Symbol name;
        private Dictionary<Symbol, Expression> members;
        private List<Symbol> constructorParameters;

        public Symbol Name => name;
        public Dictionary<Symbol, Expression> Members => members;
        public List<Symbol> ConstructorParameters => constructorParameters;

        public override string ToString() => "#<class:" + name + ">";
        
        public Class(Symbol name, Dictionary<Symbol, 
                     Expression> members, 
                     List<Symbol> constructorParameters)
        {
            this.name = name;
            this.members = members;
            this.constructorParameters = constructorParameters;
        }
    
        //TODO
        public override object Clone()
        {
            throw new System.NotImplementedException();
        }

        public override Expression Evaluate(Dictionary<Symbol, Expression> env)
        {
            throw new System.NotImplementedException();
        }
    }
}