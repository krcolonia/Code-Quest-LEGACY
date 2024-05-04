using Godot;
using System;
using Values;
using Interpreter;
using Environment;
// using Expressions;

namespace Statements
{
  public class Eval
  {
    public static RuntimeVal EvaluateProgram(Program program, Environment.Environment env)
    {
      RuntimeVal lastEvaluated = new NullVal();
      foreach (Stmt statement in program.Body)
      {
        lastEvaluated = Interpreter.Eval.Evaluate(statement, env);
      }
      return lastEvaluated;
    }
  }
}