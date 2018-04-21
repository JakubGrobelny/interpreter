using System.Collections.Generic;
using System.Text;

public class Tokenizer {

    private static instance = null;

    private bool IsNested(Dictionary<char, int> brackets) {
        return brackets['{'] != brackets['}'] ||
               brackets['('] != brackets[')'] ||
               brackets['['] != brackets[']'];
    }

    // Tokenizes single expression (string with matching brackets)
    public ArrayList<Object> Tokenize(string text) {

        var tokens = new ArrayList<Object>();
        var stringBuilder = new StringBuilder();
        var brackets = new Dictionary<char, int>();
        var readingString = false;

        foreach (char c in text) {

            if (Char.IsWhiteSpace(c) && !readingString ) {
                if (stringBuilder.Length != 0) {
                    tokens.Add(stringBuilder.ToString());
                }
                stringBuilder.Clear();
            }
            else if (c == '"') {
                if (readingString) {
                    readingString = false;
                    stringBuilder.Append(c);
                    tokens.Add(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
                else {
                    readingString = true;
                    stringBuilder.Append(c);
                    if (stringBuilder.Length != 0) {
                        tokens.Add(stringBuilder.ToString());
                    }
                    stringBuilder.Clear();
                }
            }
            else if (c == '(' || c == '[' || c == '{') {
                brackets[c]++;
            }
            else if (c == ')' || c == ']' || c == '}') {
                brackets[c]++;
                if (brackets[c] == 0) {
                    tokens.Add(Tokenize(stringBuilder.ToString()));
                    stringBuilder.Clear();
                }
            }


        }

    }

    public static Tokenizer Instance {
        get {
            if (instance == null) {
                instance = new Tokenizer();
            }
            return instance;
        }
    }

    private Tokenizer():
}