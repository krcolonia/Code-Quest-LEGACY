using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

public class Analyzer : Node
{
  private List<string> output = new List<string>();
  private List<string> syntaxList = new List<string>();
  private bool error = false;
  private string syntaxFormat = "";

  // public override void _Ready()
  // {
  //
  // }

  //  public override void _Process(float delta)
  //  {
  //      
  //  }

  public void CodeCheck(string expression)
  {
    List<string> splitted = splitString(expression);

    bool lexValid = true;
    bool syntaxValid = true;

    lexValid = lexical(splitted);

    if (lexValid)
    {
      syntaxValid = syntax(output);
    }

    if (syntaxValid)
    {
      semantic(syntaxList);
    }

    ClearVars();
  }

  private void ClearVars()
  {
    output.Clear();
    syntaxList.Clear();
  }

  private bool semantic(List<string> syntaxList)
  {
    if (syntaxFormat == "assignment")
    {
      if (syntaxList[0] == "int" && int.TryParse(syntaxList[3], out int intValue))
      {
        GD.Print("Semantically Correct");
        return true;
      }
      else if (syntaxList[0] == "String" && syntaxList[3].StartsWith("\"") && syntaxList[3].EndsWith("\""))
      {
        GD.Print("Semantically Correct");
        return true;
      }
      else if (syntaxList[0] == "char" && syntaxList[3].Length == 3 && syntaxList[3][0] == '\'' && syntaxList[3][2] == '\'')
      {
        GD.Print("Semantically Correct");
        return true;
      }
      else if (syntaxList[0] == "double" && double.TryParse(syntaxList[3], out double doubleValue))
      {
        GD.Print("Semantically Correct");
        return true;
      }
      else
      {
        GD.Print("Semantically Incorrect");
      }
    }
    else if (syntaxFormat == "declaration")
    {
      GD.Print("Semantically Correct since no value is included in the expression");
      return true;
    }
    return false;
  }

  private bool syntax(List<string> output)
  {
    if (output.Count == 5 && output[0] == "<data_type>" && output[1] == "<identifier>" && output[2] == "<assignment_operator>" && output[3] == "<value>" && output[4] == "<delimiter>")
    {
      syntaxFormat = "assignment";
      return true;
    }
    else if (output.Count == 3 && output[0] == "<data_type>" && output[1] == "<identifier>" && output[2] == "<delimiter>")
    {
      syntaxFormat = "declaration";
      return true;
    }
    else
    {
      GD.Print("Syntactically Incorrect");
      return false;
    }
  }

  private bool lexical(List<string> splited)
  {
    foreach (string item in splited)
    {
      if (item.Equals(";"))
      {
        tokenizer(item);
      }
      else if (item.EndsWith(";"))
      {
        string noSemiColon = item.Substring(0, item.Length - 1);
        tokenizer(noSemiColon);

        string semiColon = ";";
        tokenizer(semiColon);
      }
      else
      {
        tokenizer(item);
      }
    }

    if (error)
    {
      GD.Print("Lexically Incorrect");
      return false;
    }
    else
    {
      return true;
    }
  }

  private void tokenizer(string item)
  {
    if (item == "int" || item == "double" || item == "char" || item == "String")
    {
      syntaxList.Add(item);
      output.Add("<data_type>");
    }
    else if (item == "=")
    {
      syntaxList.Add(item);
      output.Add("<assignment_operator>");
    }
    else if (item == ";")
    {
      syntaxList.Add(item);
      output.Add("<delimiter>");
    }
    else if (Regex.IsMatch(item, "-?\\d+") || Regex.IsMatch(item, "\"[^\"]*\"") || Regex.IsMatch(item, "'.'") || Regex.IsMatch(item, "-?\\d+(\\.\\d+)?"))
    {
      syntaxList.Add(item);
      output.Add("<value>");
    }
    else if (!output.Contains("\""))
    {
      syntaxList.Add(item);
      output.Add("<identifier>");
    }
    else
    {
      error = true;
    }
  }

  private List<string> splitString(string input)
  {
    List<string> split = new List<string>();
    Regex pattern = new Regex("\"([^\"]*)\"|\\S+");
    MatchCollection matcher = pattern.Matches(input);

    foreach (Match match in matcher)
    {
      string strSplit = match.Groups[0].Value;
      split.Add(strSplit);
    }

    return split;
  }
}
