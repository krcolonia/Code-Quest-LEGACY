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
  VarDeclaration,
  FunctionDeclaration,

  AssignmentExpr,
  MemberExpr,
  CallExpr,

  Property,
  ObjectLiteral,
  StringLiteral,
  NumericLiteral,
  NullLiteral,
  Identifier,
  BinaryExpr
}

public interface Stmt
{
  NodeType Kind { get; }
}

public class Program : Stmt
{
  public NodeType Kind { get { return NodeType.Program; } set { } }
  public List<Stmt> Body { get; set; }
}

public class VarDeclaration : Stmt
{
  public NodeType Kind { get { return NodeType.VarDeclaration; } }
  public bool Constant { get; set; }
  public string Identifier { get; set; }
  public Expr? Value { get; set; }
}

public class FunctionDeclaration : Stmt
{
  public NodeType Kind { get { return NodeType.FunctionDeclaration; } }
  public List<string> Parameters { get; set; }
  public string Name { get; set; }
  public List<Stmt> Body { get; set; }
}

public interface Expr : Stmt { }

public class AssignmentExpr : Expr
{
  public NodeType Kind { get { return NodeType.AssignmentExpr; } }
  public Expr Assignee { get; set; }
  public Expr Value { get; set; }
}

public class BinaryExpr : Expr
{
  public NodeType Kind { get { return NodeType.BinaryExpr; } }
  public Expr Left { get; set; }
  public Expr Right { get; set; }
  public string Operator { get; set; }
}

public class CallExpr : Expr
{
  public NodeType Kind { get { return NodeType.CallExpr; } }
  public List<Expr> Args { get; set; }
  public Expr Caller { get; set; }
}

public class MemberExpr : Expr
{
  public NodeType Kind { get { return NodeType.MemberExpr; } }
  public Expr Object { get; set; }
  public Expr Property { get; set; }
  public bool Computed { get; set; }
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

public class StringLiteral : Expr
{
  public NodeType Kind { get { return NodeType.StringLiteral; } }
  public string String { get; set; }
}

public class NullLiteral : Expr
{
  public NodeType Kind { get { return NodeType.NullLiteral; } }
  public string String { get; set; }
}

public class Property : Expr
{
  public NodeType Kind { get { return NodeType.Property; } }
  public string Key { get; set; }
  public Expr? Value { get; set; }
}

public class ObjectLiteral : Expr
{
  public NodeType Kind { get { return NodeType.ObjectLiteral; } }
  public List<Property> Properties { get; set; }
}