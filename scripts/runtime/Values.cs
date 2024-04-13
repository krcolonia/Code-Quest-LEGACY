using Godot;
using System;
using System.Runtime.Remoting.Messaging;

public enum ValueType
{
  Null,
  Boolean,
  Number,
  Literal
}

public interface RuntimeVal
{
  ValueType Type { get; set; }
}

public class NullVal : RuntimeVal
{
  public ValueType Type { get { return ValueType.Null; } set { } }
  public dynamic Value { get; set; }
}

public class BooleanVal : RuntimeVal
{
  public ValueType Type { get { return ValueType.Boolean; } set { } }
  public bool Value { get; set; }
}

public class NumberVal : RuntimeVal
{
  public ValueType Type { get { return ValueType.Number; } set { } }
  public double Value { get; set; }
}

public class LiteralVal : RuntimeVal
{
  public ValueType Type { get { return ValueType.Literal; } set { } }
  public dynamic Value { get; set; }
}

public class Values
{
  public NullVal MK_NULL()
  {
    return new NullVal { Type = ValueType.Null, Value = null };
  }

  public BooleanVal MK_BOOL(bool b = true)
  {
    return new BooleanVal { Type = ValueType.Boolean, Value = b };
  }

  public NumberVal MK_NUMBER(double n = 0)
  {
    return new NumberVal { Type = ValueType.Number, Value = n };
  }
}