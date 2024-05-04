// ? holds structure for scope , aka env

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Godot;
using Environment;
using Values;

namespace Environment
{
  public class Environment
  {
    private readonly Environment? parent;
    private readonly Dictionary<string, RuntimeVal> variables;
    private readonly HashSet<string> constants;

    public Environment(Environment? parentENV = null)
    {
      this.parent = parentENV;
      this.variables = new Dictionary<string, RuntimeVal>();
      this.constants = new HashSet<string>();
    }

    public static Environment CreateGlobalEnv()
    {
      Environment env = new Environment();

      // Create Default Global Environment
      env.DeclareVar("true", MK_BOOL.Create(true), true);
      env.DeclareVar("false", MK_BOOL.Create(false), true);
      env.DeclareVar("null", MK_NULL.Create(), true);

      env.DeclareVar(
        "print",
        MK_NATIVE_FN.Create((args, scope) => 
        {
          GD.Print(args);
          return MK_NULL.Create();
        }),
        true
      );

      env.DeclareVar(
        "time",
        MK_NATIVE_FN.Create((_args, _env) => MK_NUMBER.Create(DateTime.Now.Ticks)),
        true
      );

      return env;
    }

    public RuntimeVal DeclareVar(string varName, RuntimeVal value, bool constant)
    {
      if (this.variables.ContainsKey(varName))
      {
        GD.Print($"Cannot declare variable {varName} as it is already defined.");
      }

       this.variables.Add(varName, value);
       if (constant)
       {
        this.constants.Add(varName);
       }

      // variables[varName] = value;
      return value;
    }

    public RuntimeVal AssignVar(string varName, RuntimeVal value)
    {
      Environment env = Resolve(varName);

      if(env.constants.Contains(varName))
      {
        GD.Print($"Cannot reassign to variable '{varName}' as it was declared constant.");
      }

      env.variables[varName] = value;
      return value;
    }

    public RuntimeVal LookupVar(string varName)
    {
      Environment env = Resolve(varName);
      return env.variables[varName];
    }

    public Environment Resolve(string varName)
    {
      if (variables.ContainsKey(varName))
      {
        return this;
      }
      if (parent == null)
      {
        GD.Print($"Cannot resolve '{varName}' as it does not exist");
      }

      return parent.Resolve(varName);
    }
  }
}