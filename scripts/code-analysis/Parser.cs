using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public class Parser : Node
{
  private TokenStream input;
  private string ConsolePrint;

  private Dictionary<string, int> PRECEDENCE = new Dictionary<string, int>
  {
    { "=", 1 },
    { "||", 2},
    { "&&", 3 },
    { "<", 7 }, { ">", 7 }, { "<=", 7 }, { ">=", 7 }, { "==", 7 }, { "!=", 7 },
    { "+", 10 }, { "-", 10 },
    { "*", 20 }, { "/", 20 }, { "%", 20 },
  };

  public Parser(TokenStream input, string ConsolePrint)
  {
    this.input = input;
    this.ConsolePrint = ConsolePrint;
  }

  
}