using System.Collections.Specialized;

public enum TokenType
{
    Keyword, Identifier, Number, Operator, Bracket, Semicolon, Comma, Whitespace, Comment, Unknown
}
public class Token
{
    public Token(TokenType type, string value)
    {
        this.type = type;
        Value = value;
    }

    public TokenType type { get; set; }
    public string Value { get; set; }
    public override string ToString() => $"({type}, '{Value}')";
}

public class LexerAnalyzer
{
    private static readonly Dictionary<TokenType, string> Patterns = new Dictionary<TokenType, string>
    {
        { TokenType.Keyword, @"\b(int|float|if|else|for|while|return|class|public|static|void|new)\b" },
        { TokenType.Identifier, @"\b[a-zA-Z_][a-zA-Z_0-9]*\b" },
        { TokenType.Number, @"\b\d+(\.\d*)?\b" },
        { TokenType.Operator, @"[+\-*/=<>!]+" },
        { TokenType.Bracket, @"[(){}]" },
        { TokenType.Semicolon, @";" },
        { TokenType.Comma, @"," },
        { TokenType.Whitespace, @"\s+" },
        { TokenType.Comment, @"//.*" }
    };

    public LexerAnalyzer(List<Token> tokens, string code)
    {
        Tokens = tokens;
        this.code = code;
    }

    //private List<Token> tokens;
    //private string code;
    public List<Token> Tokens { get; set; }
    public string code { get; set; }

    public void Tokenize()
    {

    }
}