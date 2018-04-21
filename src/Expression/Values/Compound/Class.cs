using System.Collections.Generic;
using Environment = Dictionary<Symbol, Expression>;
using Members = Dictionary<Symbol, Expression>;

public class Class
{
    Symbol name;
    Members mutableMembers;
    Members immutableMembers;
    Class baseClass;

    public string ToString()
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
                    throw InvalidMember(ToString(), member.ToString());
                else
                    return mutableMembers[member];
            }
            else
                return immutableMembers[member];
        }
        set
        {
            if (!mutableMembers.ContainsKey(member))
                throw InvalidMember(ToString(), member.ToString());
            else
                mutableMembers[member] = value;
        }
    }

    public Class(Symbol name, Members mutable, Members immutable, Class baseClass)
    {
        this.name = name;
        this.baseClass = baseClass;
        this.mutableMembers = mutable;
        this.immutableMembers = immutable;
    }
}