using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class TokenStream : Node
{
  private string ConsolePrint;
  private InputStream input;
  private Token current = null;

  private HashSet<string> KEYWORDS = new HashSet<string>
    {
      "int",
      "double",
      "string",
      "char",
      "boolean",

      // "if",
      // "else",
      "true",
      "false",
    };

  public TokenStream(InputStream input, string ConsolePrint)
  {
    this.input = input;
    this.ConsolePrint = ConsolePrint;
  }

  public string GetConsolePrint()
  {
    return ConsolePrint;
  }

  public Token Next()
  {
    Token tok = current;
    current = null;

    return tok ?? ReadNext();
  }

  public Token Peek()
  {
    return current ?? (current = ReadNext());
  }

  public bool EOF()
  {
    return Peek() == null;
  }

  private bool IsKeyword(string x)
  {
    return KEYWORDS.Contains(x);
  }

  private bool IsDigit(char ch)
  {
    return char.IsDigit(ch);
  }

  private bool IsIdStart(char ch)
  {
    return char.IsLetter(ch) || ch == '_';
  }

  private bool IsId(char ch)
  {
    return IsIdStart(ch) || "?!-<>=0123456789".IndexOf(ch) >= 0;
  }

  private bool IsOpChar(char ch)
  {
    return "+-*/%=&|<>!".IndexOf(ch) >= 0;
  }

  private bool IsPunc(char ch)
  {
    return ",;(){}[]".IndexOf(ch) >= 0;
  }

  private bool IsWhitespace(char ch)
  {
    return " \t\n".IndexOf(ch) >= 0;
  }

  private string ReadWhile(Func<char, bool> predicate)
  {
    string str = "";
    while (!input.EOF() && predicate(input.Peek()))
    {
      str += input.Next();
    }
    return str;
  }

  private Token ReadDigit()
  {
    bool hasDot = false;
    string number = ReadWhile(ch =>
    {
      if (ch == '.')
      {
        if (hasDot) return false;
        hasDot = true;
        return true;
      }
      return IsDigit(ch);
    });

    if (number.EndsWith("."))
    {
      ConsolePrint += input.croak("ERROR: Invalid decimal point placement at");
      return null;
    }
    else if (hasDot && double.TryParse(number, out double doubleCheck))
    {
      GD.Print("double");
      return new Token(TokenType.Double, double.Parse(number));
    }
    else
    {
      GD.Print("integer");
      return new Token(TokenType.Integer, int.Parse(number));
    }
  }

  private Token ReadIdent()
  {
    string id = ReadWhile(IsId);
    return new Token(IsKeyword(id) ? TokenType.Keyword : TokenType.Variable, id);
  }

  private string ReadEscaped(char end)
  {
    bool escaped = false;
    string str = "";


    int charCount = 0; // keeps count of characters within current token. used for validating char
    char strEnd = '\0';

    input.Next();

    while (!input.EOF())
    {
      char ch = input.Next();
      charCount++;
      GD.Print(charCount);

      if (escaped)
      {
        str += ch;
        escaped = false;
      }
      else if (ch == '\\')
      {
        escaped = true;
      }
      else if (ch == end)
      {
        strEnd = ch;
        break;
      }
      else
      {
        str += ch;
      }
    }

    if (strEnd != end)
    {
      ConsolePrint += input.croak($"ERROR: missing {end} at");
      return null;
    }

    if (end == '\'' && (charCount-1 > 1 || charCount-1 < 1))
    {
      ConsolePrint += input.croak($"ERROR: char variable must contain only 1 alphanumeric value");
      return null;
    }

    else
    {
      return str;
    }
  }

  private Token ReadString()
  {
    string str = ReadEscaped('"');

    GD.Print(str);

    if(str == null)
    {
      return null;
    }
    else
    {
      return new Token(TokenType.String, str);
    }

  }

  private Token ReadChar()
  {
    string str = ReadEscaped('\'');

    if(str == null)
    {
      return null;
    }
    else
    {
      return new Token(TokenType.Char, str); //char.Parse(str)
    }
  }

  private void SkipComment()
  {
    ReadWhile(ch => ch != '\n');
    input.Next(); // Skip the newline character
  }

  private Token ReadNext()
  {
    ReadWhile(IsWhitespace);
    if (input.EOF()) return null;
    char ch = input.Peek();
    if (ch == '#')
    {
      SkipComment();
      return ReadNext();
    }
    if (ch == '\'') return ReadChar();
    if (ch == '"') return ReadString();
    if (IsDigit(ch)) return ReadDigit();
    if (IsIdStart(ch)) return ReadIdent();
    if (IsPunc(ch)) return new Token(TokenType.Punctuation, input.Next().ToString());
    if (IsOpChar(ch)) return new Token(TokenType.Operator, ReadWhile(IsOpChar));

    return null;
  }
}