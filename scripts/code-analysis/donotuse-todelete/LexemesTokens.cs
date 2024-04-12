using System.Collections.Generic;

// ? itong LexemesTokens class, data class to.
// ? parang ung gamit lang natin sa RecyclerView sa AndroidStudio
// ? class siya na pwedeng gawing instanced object sa iba't ibang classes para makapag store ng bagay bagay
// ? mostly, gamit to sa pag-pasa pasa from Tokenizer to Parser to Interpreter.
// ? also, naka-store pala dito syntax type, which is gamit ng Interpreter na pang base ng type ng code na ie-execute.

public class LexemesTokens
{
  public List<string> lexemes { get; set; }
  public List<string> tokens { get; set; }

  public bool ValidLexes { get; set; } = false;
  public bool ValidParse { get; set; } = false;
  
  public string SyntaxType { get; set; } = "";
}