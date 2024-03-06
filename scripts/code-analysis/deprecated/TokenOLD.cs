using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;

public class TokenOLD : Node
{

  // ? Stores Lexemes, or ung mga napaghiwa-hiwalay na words sa isang line.
  private List<string> lexemes = new List<string>();

  // ? Stores tokens, ito ung nagdedetermine kung ano ba ung specific na word na nasplit. Punta ka line 81, mas explained ko don.
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

  // ? ito yung bukod tanging public na method sa class na to.
  // ? ito yung tinatawag sa CQScript, dito nagsisimula and nage-end ung function ng Token class.
  public LexemesTokens RetrieveLexemesTokens(string input)
  {
    ClearVars(); // ? gamit to pang clear ng variables para pag magru-run uli si user ng code niya, di masama sa pag-run ung previous run niya.

    List<string> split = SplitLexemes(input);

    bool lexValid = true;

    lexValid = LexicalAnalysis(split);

    if (!lexValid)
    {
      return null;
    }

    // ? ito yung binabato pabalik sa CQScript.cs para magamit pa nung Parser and Interpreter.
    return new LexemesTokens { lexemes = lexemes, tokens = tokens, ValidLexes = true, ValidParse = false, SyntaxType = "" };
  }

  // ? Galing sa SplitLexemes method, ipapass niya yung bawat split item sa MakeTokens method.
  // ? Magru-run to hanggang sa makita niya na ung current item niya ung delimiter, which indicates a line end in coding.
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

  // ? Ito yung gumagawa ng tinatawag na "tokens"
  // ? Tokens, nagrerefer sa kung ano ba ung function ng isang part/word ng code.
  // ? For example, int a = 1; pag na-split na siya, magiging 'int', 'a', '=', '1', ';'
  // ? ang gagawin ng tokenizer, magseset ng kung ano ung meaning ng bawat split word
  // ? 'int' == '<data_type>', 'a' == '<identifier>', '=' == '<assignment_operator>', '1' == '<value>', ';' == '<delimiter>'
  // ? kumbaga, para syang dictionary, binibigyan niyang meaning ung split-apart na codes.
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

    // ? Splits input if it is calling a method
    string parenSplit = @"(\()|(\))";
    split = new List<string>(Regex.Split(input, parenSplit));
    split.RemoveAll(string.IsNullOrEmpty);
    split.ConvertAll(part => part.Trim());


    Regex pattern = new Regex("\"([^\"]*)\"|\\S+");
    MatchCollection matcher = pattern.Matches(input);

    // ? If not method, splits it like a variable declaration/initialization.
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

  //  ? Clears all inputs, used when running the code in-game
  private void ClearVars()
  {
    lexemes.Clear();
    tokens.Clear();
  }
}
