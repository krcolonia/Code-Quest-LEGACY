using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;

public class Token : Node
{

  private List<string> lexemes = new List<string>();
  private List<string> tokens = new List<string>();

  private bool error = false;

  private List<string> TYPES = new List<string>
  {
    "int",
    "double",
    "string",
    "char"
  };

  private List<string> METHODS = new List<string>
  {
    "print",
    "println"
  };

  public LexemesTokens RetrieveLexemesTokens(string input)
  {
    ClearVars();

    List<string> split = SplitLexemes(input);

    bool lexValid = true;

    lexValid = LexicalAnalysis(split);

    if (!lexValid)
    {
      return null;
    }

    return new LexemesTokens { lexemes = lexemes, tokens = tokens, ValidLexes = true, ValidParse = false, SyntaxType = "" };
  }

  private bool LexicalAnalysis(List<string> split)
  {
    foreach (string item in split)
    {
      if (item.Equals(";"))
      {
        MakeTokens(item);
      }
      else if (item.EndsWith(";"))
      {
        string NoSemiColon = item.Substring(0, item.Length - 1);
        MakeTokens(NoSemiColon);

        string SemiColon = ";";
        MakeTokens(SemiColon);
      }
      else
      {
        MakeTokens(item);
      }
    }

    if (error)
    {
      return false;
    }
    else
    {
      return true;
    }
  }

  private void MakeTokens(string item)
  {
    if (TYPES.Contains(item))
    {
      lexemes.Add(item);
      tokens.Add("<data_type>");
    }
    else if (METHODS.Contains(item))
    {
      lexemes.Add(item);
      tokens.Add("<method>");
    }
    else
    {
      switch (item)
      {
        case "(":
          lexemes.Add(item);
          tokens.Add("<oparen>");
          break;
        case ")":
          lexemes.Add(item);
          tokens.Add("<cparen>");
          break;
        case "=":
          lexemes.Add(item);
          tokens.Add("<assignment_operator>");
          break;
        case ";":
          lexemes.Add(item);
          tokens.Add("<delimiter>");
          break;
        case string IntVal when Regex.IsMatch(IntVal, "-?\\d+"):
        case string StrVal when Regex.IsMatch(StrVal, "\"[^\"]*\""):
        case string ChrVal when Regex.IsMatch(ChrVal, "'.'"):
        case string DblVal when Regex.IsMatch(DblVal, "-?\\d+(\\.\\d+)?"):
          lexemes.Add(item);
          tokens.Add("<value>");
          break;
        case string ident when !ident.Contains("\""):
          lexemes.Add(item);
          tokens.Add("<identifier>");
          break;
        default:
          error = true;
          break;
      }
    }
  }

  private List<string> SplitLexemes(string input)
  {
    List<string> split = new List<string>();

    // Splits input if it is calling a method
    string parenSplit = @"(\()|(\))";
    split = new List<string>(Regex.Split(input, parenSplit));
    split.RemoveAll(string.IsNullOrEmpty);
    split.ConvertAll(part => part.Trim());


    Regex pattern = new Regex("\"([^\"]*)\"|\\S+");
    MatchCollection matcher = pattern.Matches(input);

    // If not method, splits it like a variable declaration/initialization.
    if (split.Count <= 1)
    {
      split.Clear();
      foreach (Match match in matcher)
      {
        string strSplit = match.Groups[0].Value;
        split.Add(strSplit);
      }
    }

    return split;
  }


  // Clears all inputs, used when running the code in-game
  private void ClearVars()
  {
    lexemes.Clear();
    tokens.Clear();
  }
}
