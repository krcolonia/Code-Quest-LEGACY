using Godot;
using System;
using System.Collections.Generic;

// ? this file contains all of the available ASTs that CQScript can parse.
// ? An AST is basically what tells the structure of something

// ? Example, "1 + 2" is a Binary Expression.
// ? Turning that expression into an AST will be structured like so:
// ? BinaryExpr
// ? {
// ?    Type: NodeType.BinaryExpr
// ?    Left: 1
// ?    Right: 2
// ?    Operator: +
// ? }

public enum NodeType
{
  Program,
  NumericLiteral,
  NullLiteral,
  StringLiteral,
  Identifier,
  BinaryExpr
}

public interface Stmt
{
  NodeType Kind { get; }
}

public interface Expr : Stmt { }

public class Program : Stmt
{
  public NodeType Kind { get { return NodeType.Program; } }
  public List<Stmt> Body { get; set; }
}

public class BinaryExpr : Expr 
{
  public NodeType Kind { get { return NodeType.BinaryExpr; } }
  public Expr Left { get; set; }
  public Expr Right { get; set; }
  public string Operator { get; set; }
}

public class Identifier : Expr
{
  public NodeType Kind { get { return NodeType.Identifier; } }
  public string Symbol { get; set; }
}

public class NumericLiteral : Expr
{
  public NodeType Kind { get { return NodeType.NumericLiteral; } }
  public dynamic Number { get; set; }
}

public class NullLiteral : Expr
{
  public NodeType Kind { get { return NodeType.NullLiteral; }  }
  public string Null = "null";
}

public class StringLiteral : Expr
{
  public NodeType Kind { get { return NodeType.StringLiteral; } }
  public string String { get; set; }
}
