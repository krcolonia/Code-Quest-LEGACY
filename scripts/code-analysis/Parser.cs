using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Parser : Node
{
  private InputStream input;

  private TokenStream tokenizer;

  private List<Token> tokens = new List<Token>();
  private string ConsolePrint = "";

  public Parser(TokenStream tokenizer, List<Token> tokens, string ConsolePrint)
  {
    this.tokenizer = tokenizer;
    this.tokens = tokens;
    this.ConsolePrint = ConsolePrint;
  }

  public string GetConsolePrint()
  {
    GD.Print("Parser.GetConsolePrint()");
    return ConsolePrint;
  }

  public Token Next()
  {
    Token tok = tokens[0];
    tokens.RemoveAt(0);
    GD.Print("Next(): " + tok.Value);

    return tok;
  }

  public Token Peek()
  {
    GD.Print("Peek():" + tokens[0].Value);
    return tokens[0];
  }

  public bool EOF()
  {
    GD.Print("Parser.EOF()");
    return Peek().Type == TokenType.EOF;
  }

  

  public string croak(string msg)
  {
    return input.croak(msg);
  }

  /* public Program ProduceAST()
  {
    GD.Print("ProduceAST()");
    Program program = new Program { Kind = NodeType.Program , Body = new List<Stmt>() };
    
    if (EOF()) 
    {
      GD.Print("PARSER: EOF REACHED, IDK WHAT'S NEXT");
      return null;
    }
    // while (!EOF())
    // {
    //   program.Body.Add(ParseStmt());
    // }

    return program;
  }

  private Stmt ParseStmt()
  {
    GD.Print("Parser.ParseStmt()");
    return ParseAdditiveExpr();
    // switch (Peek().Type)
    // {
    //   case TokenType.Keyword:
    //     switch (Peek().Value)
    //     {
    //       case "string":
    //       case "int":
    //       case "double":
    //       case "char":
    //       case "boolean":
    //         return ParseVarDeclaration();
    //     }
    //     break;
    //   case TokenType.Function:
    //     return ParseFnDeclaration();
    //   default:
    //     return ParseExpr();
      
    // }
  }

  // private Expr ParseAssignmentExpr()
  // {
  //   Expr left = ParseObjectExpr();

  //   if (Peek().Type == TokenType.Equals)
  //   {
  //     Next();
  //     Expr value = ParseAssignmentExpr();
  //     left = new AssignmentExpr { Value = value, Assignee = left };
  //   }
  //   return left;
  // }

  private Expr ParseAdditiveExpr()
  {
    GD.Print("Parser.ParseAdditiveExpr()");
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
    GD.Print("Parser.ParseMultiplicativeExpr()");
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
    GD.Print("Parser.ParsePrimaryExpr()");
    TokenType tk = Peek().Type;

    switch (tk)
    {
      case TokenType.Identifier:
        GD.Print(Peek().Value.ToString());
        return new Identifier { Symbol = Next().Value.ToString() };

      case TokenType.Integer:
        GD.Print(Peek().Value.ToString());
        return new NumericLiteral { Number = int.Parse(Next().Value.ToString()) };

      case TokenType.Double:
        GD.Print(Peek().Value.ToString());
        return new NumericLiteral { Number = double.Parse(Next().Value.ToString()) };

      default:
        GD.Print("Unexpected token found during parsing");
        return null;
    }
  } */

  /* private Token Expect(TokenType type, string err)
    {
        Token prev = Next();
        if (prev == null || prev.Type != type)
        {
            Console.WriteLine("Parser Error:\n" + err + prev + " - Expecting: " + type);
            return null;
        }
        return prev;
    }

    public Program ProduceAST(string sourceCode)
    {
        Program program = new Program
        {
            Kind = NodeType.Program,
            Body = new List<Stmt>()
        };

        while (!EOF())
        {
            program.Body.Add(ParseStmt());
        }

        return program;
    }

    private Stmt ParseStmt()
    {
        switch (Peek().Type)
        {
            case TokenType.Keyword:
              return ParseVarDeclaration();
            case TokenType.Function:
                return ParseFnDeclaration();
            default:
                return ParseExpr();
        }
    }

    private Stmt ParseFnDeclaration()
    {
        Eat(); // eat fn keyword
        string name = Expect(TokenType.Identifier, "Expected function name following fn keyword").Value;

        List<Expr> args = ParseArgs();
        List<string> paramsList = new List<string>();
        foreach (Expr arg in args)
        {
            if (!(arg is Identifier))
            {
                Console.WriteLine(arg);
                throw new Exception("Inside function declaration expected parameters to be of type string.");
            }

            paramsList.Add((arg as Identifier).Symbol);
        }

        Expect(TokenType.OpenBrace, "Expected function body following declaration");
        List<Stmt> body = new List<Stmt>();

        while (At().Type != TokenType.EOF && At().Type != TokenType.CloseBrace)
        {
            body.Add(ParseStmt());
        }

        Expect(TokenType.CloseBrace, "Closing brace expected inside function declaration");

        return new FunctionDeclaration
        {
            Body = body,
            Name = name,
            Parameters = paramsList,
            Kind = "FunctionDeclaration"
        };
    }

    private Stmt ParseVarDeclaration()
    {
        bool isConstant = Eat().Type == TokenType.Const;
        string identifier = Expect(TokenType.Identifier, "Expected identifier name following let | const keywords.").Value;

        if (At().Type == TokenType.Semicolon)
        {
            Eat(); // expect semicolon
            if (isConstant)
            {
                throw new Exception("Must assign value to constant expression. No value provided.");
            }

            return new VarDeclaration
            {
                Kind = "VarDeclaration",
                Identifier = identifier,
                Constant = false
            };
        }

        Expect(TokenType.Equals, "Expected equals token following identifier in var declaration.");

        VarDeclaration declaration = new VarDeclaration
        {
            Kind = "VarDeclaration",
            Value = ParseExpr(),
            Identifier = identifier,
            Constant = isConstant
        };

        Expect(TokenType.Semicolon, "Variable declaration statement must end with semicolon.");

        return declaration;
    }

    private Expr ParseExpr()
    {
        return ParseAssignmentExpr();
    }

    private Expr ParseAssignmentExpr()
    {
        Expr left = ParseObjectExpr();

        if (At().Type == TokenType.Equals)
        {
            Eat(); // advance past equals
            Expr value = ParseAssignmentExpr();
            return new AssignmentExpr
            {
                Value = value,
                Assignee = left,
                Kind = "AssignmentExpr"
            };
        }

        return left;
    }

    private Expr ParseObjectExpr()
    {
        if (At().Type != TokenType.OpenBrace)
        {
            return ParseAdditiveExpr();
        }

        Eat(); // advance past open brace
        List<Property> properties = new List<Property>();

        while (NotEof() && At().Type != TokenType.CloseBrace)
        {
            string key = Expect(TokenType.Identifier, "Object literal key expected").Value;

            if (At().Type == TokenType.Comma)
            {
                Eat(); // advance past comma
                properties.Add(new Property { Key = key, Kind = "Property" });
                continue;
            }
            else if (At().Type == TokenType.CloseBrace)
            {
                properties.Add(new Property { Key = key, Kind = "Property" });
                continue;
            }

            Expect(TokenType.Colon, "Missing colon following identifier in ObjectExpr");
            Expr value = ParseExpr();

            properties.Add(new Property { Kind = "Property", Value = value, Key = key });
            if (At().Type != TokenType.CloseBrace)
            {
                Expect(TokenType.Comma, "Expected comma or closing bracket following property");
            }
        }

        Expect(TokenType.CloseBrace, "Object literal missing closing brace.");
        return new ObjectLiteral { Kind = "ObjectLiteral", Properties = properties };
    } */
}