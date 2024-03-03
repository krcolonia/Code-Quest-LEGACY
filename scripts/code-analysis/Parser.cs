using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Xml;

public class Parser : Node
{
  // ? Yung parser, ang ginagawa nito is siya yung nagchecheck kung anong type ng syntax siya. ito mga examples:

  // ? string a = "jello"; is equals to an assignment syntax type. Kasi, ina-assign niya si string na "a" ng value na "jello"
  // ? string a; is equals to declaration syntax type. Kasi, dinedeclare niya lang yung string variable na "a" pero wala siyang assigned value.
  // ? println("Hello, World!"); is equals to method syntax type. Kasi, tinatawag niya yung "println" na method para i-print yung value na "Hello, World!"

  public LexemesTokens SyntaxParser(LexemesTokens lextokens)
  {
    List<string> tokens = lextokens.tokens;
    string syntaxType = lextokens.SyntaxType;

    foreach (string item in tokens)
    {
      GD.Print(item);
    }
    
    foreach (string item in lextokens.lexemes)
    {
      GD.Print(item);
    }

    if(tokens[0] == "<data_type>" &&
       tokens[1] == "<identifier>" &&
       tokens[2] == "<assignment_operator>" &&
       tokens[3] == "<value>" &&
       tokens[4] == "<delimiter>")
    {
      syntaxType = "assignment";
      return new LexemesTokens { lexemes = lextokens.lexemes, tokens = tokens, ValidLexes = lextokens.ValidLexes, ValidParse = true, SyntaxType = syntaxType };
    }
    else if (tokens[0] == "<data_type>" &&
             tokens[1] == "<identifier>" &&
             tokens[2] == "<delimiter>")
    {
      syntaxType = "declaration";
      return new LexemesTokens { lexemes = lextokens.lexemes, tokens = tokens, ValidLexes = lextokens.ValidLexes, ValidParse = true, SyntaxType = syntaxType };
    }
    else if (tokens[0] == "<method>" &&
             tokens[1] == "<oparen>" &&
             tokens[2] == "<value>" &&
             tokens[3] == "<cparen>" &&
             tokens[4] == "<delimiter>")
    {
      syntaxType = "method";
      return new LexemesTokens { lexemes = lextokens.lexemes, tokens = tokens, ValidLexes = lextokens.ValidLexes, ValidParse = true, SyntaxType = syntaxType };
    }
    else
    {
      syntaxType = "INVALID";
      return new LexemesTokens { lexemes = lextokens.lexemes, tokens = lextokens.lexemes, ValidLexes = lextokens.ValidLexes, ValidParse = false, SyntaxType = syntaxType };
    }
  }
}
