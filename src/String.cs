public class StringLiteral : Value {

    private string value;

    public string ToString() {
        return "\"" + value + "\"";
    }

    public StringLiteral(string value) {
        this.value = value;
    }
}