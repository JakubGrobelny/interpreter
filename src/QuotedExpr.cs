public class QuotedExpr : Value {

    private Expression expr;

    public Expression Evaluate(Environment env) {
        return expr;
    }

    public string ToString() {
        return "'" + expr.ToString();
    }

    public QuotedExpr(Expression expr) {
        this.expr = expr;
    }
}