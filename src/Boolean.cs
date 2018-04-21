public class Bool : Value {

    private bool value;

    public bool GetValue() {
        return value;
    }

    public ToString() {
        return value ? "#t" : "#f";
    }

    public Bool(bool value) {
        this.value = value;
    }
}