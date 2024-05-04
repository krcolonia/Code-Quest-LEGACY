using Godot;
using System;
using Environment;
using System.Collections.Generic;

namespace Values
{
  public enum ValueType
  {
    Null,
    Boolean,
    Number,
    Literal,
    Object,
    NativeFn,
    Function,
  }

  public interface RuntimeVal
  {
    ValueType Type { get; }
  }

  public class NullVal : RuntimeVal
  {
    public static readonly NullVal Instance = new NullVal();
    public ValueType Type => ValueType.Null;
  }

  public static class MK_NULL
  {
    public static RuntimeVal Create()
    {
      return NullVal.Instance;
    }
  }

  public class BooleanVal : RuntimeVal
  {
    public ValueType Type { get; set; }
    public bool Value { get; set; }

    public BooleanVal(bool value)
    {
      this.Type = ValueType.Boolean;
      this.Value = value;
    }
  }

  public static class MK_BOOL
  {
    public static RuntimeVal Create(bool b  = true)
    {
      return new BooleanVal(b);
    }
  }

  public class NumberVal : RuntimeVal
  {
    public ValueType Type { get; set; }
    public double Value { get; set; }

    public NumberVal(double value)
    {
      this.Type = ValueType.Number;
      this.Value = value;
    }
  }

  public static class MK_NUMBER
  {
    public static RuntimeVal Create(double n = 0)
    {
      return new NumberVal(n);
    }
  }

  public class ObjectVal : RuntimeVal
  {
    public ValueType Type { get; set; }
    public Dictionary<string, RuntimeVal> Properties { get; set; }

    public ObjectVal()
    {
      this.Type = ValueType.Object;
      this.Properties = new Dictionary<string, RuntimeVal>();
    }
  }

  public delegate RuntimeVal FunctionCall(RuntimeVal[] args, Environment.Environment Env);

  public class NativeFnValue : RuntimeVal
  {
    public ValueType Type { get; set; }
    public FunctionCall Call { get; set; }

    public NativeFnValue(FunctionCall call)
    {
      this.Type = ValueType.NativeFn;
      this.Call = call;
    }
  }

  public static class MK_NATIVE_FN
  {
    public static RuntimeVal Create(FunctionCall call)
    {
      return new NativeFnValue(call);
    }
  }

  public class FunctionValue : RuntimeVal
  {
    public ValueType Type { get; set; }
    public string Name { get; set; }
    public string[] Parameters { get; set; }
    public Environment.Environment DeclarationEnv { get; set; }
    public Stmt[] Body { get; set; }

    public FunctionValue(
      string name,
      string[] parameters,
      Environment.Environment declarationEnv,
      Stmt[] body
    )
    {
      this.Type = ValueType.Function;
      this.Name = name;
      this.Parameters = parameters;
      this.DeclarationEnv = declarationEnv;
      this.Body = body;
    }
  }

  public class LiteralVal : RuntimeVal
  {
    public ValueType Type { get { return ValueType.Literal; } set { } }
    public dynamic Value { get; set; }
  }
}