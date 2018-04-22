using System.Collections.Generic;

namespace Interpreter
{
    public class Class
    {
        Symbol name;
        Dictionary<Symbol, Expression> mutableMembers;
        Dictionary<Symbol, Expression> immutableMembers;
        Class baseClass;

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
                    else
                        return mutableMembers[member];
                }
                else
                    return immutableMembers[member];
            }
            set
            {
                if (!mutableMembers.ContainsKey(member))
                    throw new InvalidMember(ToString(), member.ToString());
                else
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