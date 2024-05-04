using Godot;
using System;
using Interpreter;
using Environment;
using Values;
using System.Collections.Generic;

namespace Expressions
{
  public class Eval
  {
    public static NumberVal EvaluateNumericBinaryExpr(NumberVal lhs, NumberVal rhs, string op)
    {
      double result = 0;
      switch (op)
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

      return new NumberVal(result);
    }

    public static RuntimeVal EvaluateBinaryExpr(BinaryExpr binop, Environment.Environment env)
    {
      RuntimeVal lhs = Interpreter.Eval.Evaluate(binop.Left, env);
      RuntimeVal rhs = Interpreter.Eval.Evaluate(binop.Right, env);

      if (lhs is NumberVal && rhs is NumberVal)
      {
        return EvaluateNumericBinaryExpr((NumberVal)lhs, (NumberVal)rhs, binop.Operator);
      }

      return MK_NULL.Create();
    }

    public static RuntimeVal EvaluateIdentifier(Identifier ident, Environment.Environment env)
    {
      return env.LookupVar(ident.Symbol);
    }

    public static RuntimeVal EvaluateAssignment(AssignmentExpr node, Environment.Environment env)
    {
      if (node.Assignee.Kind != NodeType.Identifier)
      {
        GD.Print($"Invalid LHS in assignment expression: {node.Assignee}");
        return null;
      }

      var varName = (node.Assignee as Identifier).Symbol;
      return env.AssignVar(varName, Interpreter.Eval.Evaluate(node.Value, env));
    }

    public static RuntimeVal EvalObjectExpr(ObjectLiteral obj, Environment.Environment env)
    {
      var objectVal = new ObjectVal { Type = Values.ValueType.Object, Properties = new Dictionary<string, RuntimeVal>() };

      foreach(var property in obj.Properties)
      {
        var runtimeVal = property.Value == null ? env.LookupVar(property.Key) : Interpreter.Eval.Evaluate(property.Value, env);
        objectVal.Properties.Add(property.Key, runtimeVal);
      }

      return objectVal;
    }
  }
}