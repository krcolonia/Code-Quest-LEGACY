using System;

public enum TokenType
{
  // Data Types, used to identify the data type of a values
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
  // Variable,
  Identifier,
  Keyword,
  Punctuation,
  Terminator,
  Operator,
  Program,
}