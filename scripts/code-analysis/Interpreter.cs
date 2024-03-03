using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

public class Interpreter : Node
{

  public string InterpretCode(LexemesTokens lextokens)
  {
    List<string> lexemes = lextokens.lexemes;
    List<string> tokens = lextokens.tokens;
    string syntaxType = lextokens.SyntaxType;
    string ConsolePrint = "";

    switch (syntaxType)
    {
      case "assignment":
        if (lexemes[0] == "int" && int.TryParse(lexemes[3], out int intValAss))
        {
          ConsolePrint += "Variable int " + lexemes[1] + " Initialized";
        }
        else if (lexemes[0] == "string" && lexemes[3].StartsWith("\"") && lexemes[3].EndsWith("\""))
        {
          ConsolePrint += "Variable string " + lexemes[1] + " Initialized";
        }
        else if (lexemes[0] == "char" && lexemes[3].Length == 3 && lexemes[3][0] == '\'' && lexemes[3][2] == '\'')
        {
          ConsolePrint += "Variable char " + lexemes[1] + " Initialized";
        }
        else if (lexemes[0] == "double" && double.TryParse(lexemes[3], out double doubleValue))
        {
          ConsolePrint += "Variable double " + lexemes[1] + " Initialized";
        }
        break;
      case "declaration":
        if (lexemes[0] == "int")
        {
          ConsolePrint += "Variable int " + lexemes[1] + " Declared";
        }
        else if (lexemes[0] == "string")
        {
          ConsolePrint += "Variable string " + lexemes[1] + " Declared";
        }
        else if (lexemes[0] == "char")
        {
          ConsolePrint += "Variable char " + lexemes[1] + " Declared";
        }
        else if (lexemes[0] == "double")
        {
          ConsolePrint += "Variable double " + lexemes[1] + " Declared";
        }
        break;
      case "method":
        if ((lexemes[0] == "print" || lexemes[0] == "println") && int.TryParse(lexemes[2], out int intValPrint))
        {
          ConsolePrint += lexemes[2];
          GD.Print(lexemes[2]);
        }
        else if ((lexemes[0] == "print" || lexemes[0] == "println") && lexemes[2].StartsWith("\"") && lexemes[2].EndsWith("\""))
        {
          ConsolePrint += lexemes[2].Substring(1, lexemes[2].Length - 2);
          GD.Print(lexemes[2].Substring(1, lexemes[2].Length - 2));
        }
        else if ((lexemes[0] == "print" || lexemes[0] == "println") && lexemes[2].Length == 3 && lexemes[2][0] == '\'' && lexemes[2][2] == '\'')
        {
          ConsolePrint += lexemes[2].Substring(1, lexemes[2].Length - 2);
          GD.Print(lexemes[2].Substring(1, lexemes[2].Length - 2));
        }
        else if ((lexemes[0] == "print" || lexemes[0] == "println") && double.TryParse(lexemes[2], out double doubleValue))
        {
          ConsolePrint += lexemes[2];
          GD.Print(lexemes[2]);
        }
        break;
      default:
        ConsolePrint += "ERROR: Invalid Syntax";
        break;
    }

    return ConsolePrint;
  }
}
