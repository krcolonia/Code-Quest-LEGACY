using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class Parser : Node
{
  private TokenStream input;
  private string ConsolePrint;

  private Dictionary<string, int> PRECEDENCE = new Dictionary<string, int>
  {
    { "=", 1 },
    { "||", 2},
    { "&&", 3 },
    { "<", 7 }, { ">", 7 }, { "<=", 7 }, { ">=", 7 }, { "==", 7 }, { "!=", 7 },
    { "+", 10 }, { "-", 10 },
    { "*", 20 }, { "/", 20 }, { "%", 20 },
  };

  public Parser(TokenStream input, string ConsolePrint)
  {
    this.input = input;
    this.ConsolePrint = ConsolePrint;
  }

  // ? Checks each character in input is a punctuation, keyword, or operator

  /* private Token IsPunc(string ch)
  {
    var tok = input.Peek();
    return tok != null && tok.Type == TokenType.Punctuation && (string.IsNullOrEmpty(ch) || tok.Value.ToString() == ch) ? tok : null;
  }

  private Token IsKeyword(string kw)
  {
    var tok = input.Peek();
    return tok != null && tok.Type == TokenType.Keyword && (string.IsNullOrEmpty(kw) || tok.Value.ToString() == kw) ? tok : null;
  }

  private Token IsOp(string op)
  {
    var tok = input.Peek();
    return tok != null && tok.Type == TokenType.Operator && (string.IsNullOrEmpty(op) || tok.Value.ToString() == op) ? tok : null;
  }



  private void SkipPunc(string ch)
  {
    if (IsPunc(ch) != null)
      input.Next();
    else
      input.croak("Expecting punctuation: \"" + ch + "\"");
  }
  private void SkipKeyword(string kw)
  {
    if (IsPunc(kw) != null)
      input.Next();
    else
      input.croak("Expecting keyword: \"" + kw + "\"");
  }

  private void SkipOp(string op)
  {
    if (IsOp(op) != null)
      input.Next();
    else
      input.croak("Expecting operator: \"" + op + "\"");
  }

  private void Unexpected()
  {
    input.croak("Unexpected token: " + input.Peek());
  }

  private dynamic MaybeBinary(dynamic left, dynamic myPrec)
  {
    var tok = IsOp("");
    if (tok != null)
    {
      var hisPrec = PRECEDENCE[tok.Value.ToString()];
      if (hisPrec > myPrec)
      {
        input.Next();
        return MaybeBinary(
          new
          {
            Type = tok.Value.ToString() == "=" ? "assign" : "binary",
            Operator = tok.Value,
            Left = left,
            Right = MaybeBinary(ParseAtom(), hisPrec)
          },
          myPrec
        );
      }
    }
    return left;
  }

  private List<dynamic> Delimited(string start, string stop, string separator, Func<dynamic> parser)
  {
    var a = new List<dynamic>();
    var first = true;
    SkipPunc(start);
    while (!input.EOF())
    {
      if (IsPunc(stop) != null)
      {
        break;
      }

      if (first)
      {
        first = false;
      }
      else
      {
        SkipPunc(separator);
      }

      if (IsPunc(stop) != null)
      {
        break;
      }

      a.Add(parser());
    }
    SkipPunc(stop);
    return a;
  }

  private dynamic ParseCall(dynamic func)
  {
    return new
    {
      Type = "call",
      Func = func,
      Args = Delimited("(", ")", ",", ParseExpression),
    };
  }

  private string ParseVariableName()
  {
    var name = input.Next();
    if (name.Type != TokenType.Identifier)
    {
      input.croak("Expecting variable name");
    }
    return name.Value.ToString();
  }

  // private dynamic ParseIf()
  // {
  //   SkipKeyword("if");
  //   var cond = ParseExpression();
  //   if(IsPunc("{") != null)
  //   {
  //     SkipKeyword("then");
  //   }
  //   var then = ParseExpression();
  //   var ret = new
  //   {
  //     Type = "if",
  //     Cond = cond,
  //     Then = then,
  //   };

  //   if(IsKeyword("else") != null)
  //   {
  //     input.Next();
  //     ret.Else = ParseExpression();
  //   }
  //   return ret;
  // }

  private dynamic ParseBool()
  {
    return new
    {
      Type = "bool",
      Value = input.Next().Value == "true"
    };
  }

  private dynamic MaybeCall(Func<dynamic> expr)
  {
    var result = expr();
    return IsPunc("(") != null ? ParseCall(result) : result;
  }

  private dynamic ParseAtom()
  {
    return MaybeCall(() =>
    {
      if (IsPunc("(") != null)
      {
        input.Next();
        var exp = ParseExpression();
        SkipPunc(")");
        return exp;
      }

      // if (IsPunc("{") != null)
      // {
      //   return ParseProg();
      // }

      // if(IsKeyword("if") != null)
      // {
      //   return ParseIf();
      // }

      if(IsKeyword("true") != null || IsKeyword("false") != null)
      {
        return ParseBool();
      }

      var tok = input.Next();

      if (tok.Type == TokenType.Identifier || tok.Type == TokenType.Integer || tok.Type == TokenType.Double || tok.Type == TokenType.String || tok.Type == TokenType.Char)
      {
        return tok;
      }

      Unexpected();
      return null;
    });
  }

  private dynamic ParseExpression()
  {
    return MaybeCall(() =>
    {
      return MaybeBinary(ParseAtom(), 0);
    });
  } */
}