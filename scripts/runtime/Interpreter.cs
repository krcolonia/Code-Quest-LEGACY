using Godot;
using System;
using Expressions;
using Environment;
using Statements;
using Values;

namespace Interpreter
{
  public class Eval
  {
    public static RuntimeVal Evaluate(Stmt astNode, Environment.Environment env)
    {
      switch (astNode)
      {
        case NumericLiteral numLiteral:
          GD.Print("Evaluated: NumberVal");
          return new NumberVal(numLiteral.Number);
        case NullLiteral _:
          GD.Print("Evaluated: NullVal");
          return new NullVal();
        case Identifier ident:
          GD.Print("Evaluated: Identifier");
          return Expressions.Eval.EvaluateIdentifier(ident, env);
        case BinaryExpr binExpr:
          GD.Print("Evaluated: Binary Expression");
          return Expressions.Eval.EvaluateBinaryExpr(binExpr, env);
        case Program program:
          GD.Print("Evaluated: Program");
          return Statements.Eval.EvaluateProgram(program, env);
        default:
          GD.Print("This AST node has not yet been set up for interpretation");
          return MK_NULL.Create();
      }
    }
  }
}