using Godot;
using System;

public enum TokenType
{
  // Data Types
  Integer,
  Double,
  String,
  Char,
  Boolean,

  // Basic Arthimetic Operations
  Plus,
  Minus,
  Mult,
  Div,
  Mod,

  // Tokens na hindi ko pa macategorize
  Variable,
  Keyword,
  Punctuation,
  Operator
}