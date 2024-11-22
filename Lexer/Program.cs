using System.Collections.Specialized;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

public enum TokenType
{
    PalavraReservada, Identificador, Numero, Operador, Delimitador, Vírgula, CadeiaCaracteres, EspaçoEmBranco, Comentário, Desconhecido
}
public class Token
{
    public Token(TokenType type, string value)
    {
        this.Type = type;
        Value = value;
    }

    public TokenType Type { get; set; }
    public string Value { get; set; }
    public override string ToString() => $"({Type}, '{Value}')";
}

public class LexerAnalyzer
{
    private static readonly List<(TokenType, string)> TokenPatterns = new List<(TokenType, string)>
    {
        (TokenType.PalavraReservada, @"\b(abstract|assert|boolean|break|byte|case|catch|char|class|const|continue|default|do|double|else|enum|extends|final|finally|float|for|goto|if|implements|import|instanceof|int|interface|long|native|new|null|package|private|protected|public|return|short|static|strictfp|super|switch|synchronized|this|throw|throws|transient|try|void|volatile|while)\b"),
        (TokenType.CadeiaCaracteres, "\"(\\\\.|[^\"])*\""),
        (TokenType.Comentário, @"//.*"),
        (TokenType.Numero, @"\b\d+(\.\d+)?([fFdD]?)\b"),
        (TokenType.Identificador, @"\b[a-zA-Z_][a-zA-Z_0-9]*\b"),
        (TokenType.Operador, @"[\+\-\*/\=\&\|\!\<\>]+"),
        (TokenType.Delimitador, @"[(){}[\].:;]"),
        (TokenType.Vírgula, @","),
        (TokenType.EspaçoEmBranco, @"\s+"),
    };

    public List<Token> Tokens { get; private set; }
    public string Code { get; }

    public LexerAnalyzer(string code)
    {
        Code = code;
        Tokens = new List<Token>();
    }

    public void Tokenize()
    {
        int position = 0;
        foreach (var (type,pattern) in TokenPatterns) {
            Console.WriteLine($"tipo: {type},regex : {pattern}");
        }
        while (position < Code.Length)
        {
            Token longestMatchToken = null;
            int longestMatchLength = 0;

            foreach (var (type, pattern) in TokenPatterns)
            {
                Regex regex = new Regex($"^{pattern}", RegexOptions.Compiled); // Força o match no início
                var match = regex.Match(Code.Substring(position)); // Verifica o substring a partir da posição atual

                if (match.Success && match.Length > longestMatchLength)
                {
                    longestMatchToken = new Token(type, match.Value);
                    longestMatchLength = match.Length;
                }
            }

            if (longestMatchToken != null)
            {
                if (longestMatchToken.Type != TokenType.EspaçoEmBranco) // Ignora espaços em branco
                {
                    Tokens.Add(longestMatchToken);
                }
                position += longestMatchLength; // Avança com base no comprimento do token mais longo
            }
            else
            {
                Tokens.Add(new Token(TokenType.Desconhecido, Code[position].ToString()));
                position++;
            }
        }
    }
}

public class Program
{
        public static void Main()
        {
            string filePath = "C:\\Users\\pedro\\Downloads\\lex2.txt"; // Caminho do arquivo C#

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Arquivo não encontrado.");
                return;
            }

            string code = File.ReadAllText(filePath);

            var lexer = new LexerAnalyzer(code);
            lexer.Tokenize();
            foreach (var token in lexer.Tokens)
            {
                Console.WriteLine(token);
            }
        }
}
