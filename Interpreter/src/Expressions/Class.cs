using System.Collections.Generic;

namespace Interpreter.Expressions
{
    public class Class
    {
        //TODO: redo
        private Symbol name;
        private Dictionary<Symbol, Expression> mutableMembers;
        private Dictionary<Symbol, Expression> immutableMembers;
        private Class baseClass;

        public override string ToString()
        {
            return "#<class:" + name + ">";
        }

        
        public Expression this[Symbol member]
        {
            get
            {
                if (!immutableMembers.ContainsKey(member))
                {
                    if (!mutableMembers.ContainsKey(member))
                        throw new InvalidMember(ToString(), member.ToString());
                    return mutableMembers[member];
                }

                return immutableMembers[member];
            }
            set
            {
                if (!mutableMembers.ContainsKey(member))
                    throw new InvalidMember(ToString(), member.ToString());
                mutableMembers[member] = value;
            }
        }

        public Class(Symbol name, Dictionary<Symbol, Expression> mutable, 
                     Dictionary<Symbol, Expression> immutable, Class baseClass)
        {
            this.name = name;
            this.baseClass = baseClass;
            this.mutableMembers = mutable;
            this.immutableMembers = immutable;
        }
    }
}