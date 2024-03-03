using System.Collections.Generic;

public class LexemesTokens
{
  public List<string> lexemes { get; set; }
  public List<string> tokens { get; set; }

  public bool ValidLexes { get; set; } = false;
  public bool ValidParse { get; set; } = false;
  
  public string SyntaxType { get; set; } = "";
}