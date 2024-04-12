using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Parser : Node
{
  private InputStream input;

  private List<Token> tokens = new List<Token>();
  private string ConsolePrint = "";

  private int current = 0;

  public Parser(List<Token> tokens, string ConsolePrint)
  {
    this.tokens = tokens;
    this.ConsolePrint = ConsolePrint;
  }

  public string GetConsolePrint()
  {
    return ConsolePrint;
  }

  public Token Next()
  {
    Token tok = tokens[current];
    current++;

    return tok;
  }

  public Token Peek()
  {
    return tokens[current];
  }

  public bool EOF()
  {
    return Peek() == new Token(TokenType.EOF, null);
  }

  public string croak(string msg)
  {
    return input.croak(msg);
  }

  public Program ProduceAST()
  {
    Program program = new Program { Body = new List<Stmt>() };

    while (!EOF())
    {
      program.Body.Add(ParseStmt());
    }

    return program;
  }

  private Stmt ParseStmt()
  {
    return ParseAdditiveExpr();
  }

  private Expr ParseAdditiveExpr()
  {
    Expr left = ParseMultiplicativeExpr();

    while (Peek().Type == TokenType.Operator && (Peek().Value.ToString() == "+" || Peek().Value.ToString() == "-"))
    {
      string op = Next().Value.ToString();
      Expr right = ParseMultiplicativeExpr();
      left = new BinaryExpr { Left = left, Right = right, Operator = op };
    }

    return left;
  }

  private Expr ParseMultiplicativeExpr()
  {
    Expr left = ParsePrimaryExpr();

    while(Peek().Type == TokenType.Operator && (Peek().Value.ToString() == "/" || Peek().Value.ToString() == "*" || Peek().Value.ToString() == "%"))
    {
      string op = Next().Value.ToString();
      Expr right = ParsePrimaryExpr();
      left = new BinaryExpr { Left = left, Right = right, Operator = op };
    }

    return left;
  }

  private Expr ParsePrimaryExpr()
  {
    TokenType tk = Peek().Type;

    switch (tk)
    {
      case TokenType.Identifier:
        return new Identifier { Symbol = Next().Value.ToString() };

      case TokenType.Integer:
        return new NumericLiteral { Number = int.Parse(Next().Value.ToString()) };

      case TokenType.Double:
        return new NumericLiteral { Number = double.Parse(Next().Value.ToString()) };

      default:
        GD.Print("Unexpected token found during parsing");
        return null;
    }
  }
}