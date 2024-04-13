using Godot;
using System;

public class Interpreter : Node
{
  public static RuntimeVal EvaluateProgram(Program program)
  {
    RuntimeVal lastEvaluated = new NullVal();
    foreach (Stmt statement in program.Body)
    {
      lastEvaluated = Evaluate(statement);
    }
    return lastEvaluated;
  }

  public static NumberVal EvaluateNumericBinaryExpr(NumberVal lhs, NumberVal rhs, string op)
  {
    double result = 0;
    switch(op)
    {
      case "+":
        result = lhs.Value + rhs.Value;
        break;
      case "-":
        result = lhs.Value - rhs.Value;
        break;
      case "*":
        result = lhs.Value * rhs.Value;
        break;
      case "/":
        result = lhs.Value / rhs.Value; // TODO -- add divide by zero checks
        GD.Print(result);
        break;
      case "%":
        result = lhs.Value % rhs.Value;
        break;
      default:
        GD.Print("Invalid Operator: " + op);
        break;
    }

    return new NumberVal { Value = result };
  }

  public static RuntimeVal EvaluateBinaryExpr(BinaryExpr binop)
  {
    RuntimeVal lhs = Evaluate(binop.Left);
    RuntimeVal rhs = Evaluate(binop.Right);

    if (lhs is NumberVal && rhs is NumberVal)
    {
      return EvaluateNumericBinaryExpr((NumberVal)lhs, (NumberVal)rhs, binop.Operator);
    }

    return new NullVal();
  }

  public static RuntimeVal Evaluate(Stmt astNode)
  {
    switch (astNode)
    {
      case NumericLiteral numLiteral:
        return new NumberVal { Value = numLiteral.Number };
      case NullLiteral _:
        return new NullVal();
      case BinaryExpr binExpr:
        return EvaluateBinaryExpr(binExpr);
      case Program program:
        return EvaluateProgram(program);
      default:
        GD.Print("This AST node has not yet been set up for interpretation");
        return new NullVal();
    }
  }
}