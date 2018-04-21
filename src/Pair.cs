public class Pair : Value {
    
    private Expression first;
    private Expression second;

    public Expression GetFirst() {
        return this.first;
    }

    public Expression GetSecond() {
        return this.second;
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