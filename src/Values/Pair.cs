public class Pair : Value {
    
    private Expression first;
    private Expression second;

    public Expression First {
        get {
            return first;
        }
        set {
            first = value;
        }
    }

    public Expression Second {
        get {
            return second;
        }
        set {
            second = value;
        }
    }

    public Expression GetFirst() {
        return first;
    }

    public Expression GetSecond() {
        return second;
    }

    public string ToString() {
        if (second is Pair)
            return "(" + first.ToString() + " " + second.ToString() + ")";
        else if (second is Null)
            return "(" + first.ToString() + ")";
        else
            return "(" + first.ToString() + " . " + second.ToString() + ")";
    }

    public Pair(Expression first, Expression second) {
        this.first = first;
        this.second = second;
    }
}