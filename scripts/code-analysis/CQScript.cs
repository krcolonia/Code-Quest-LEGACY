using Godot;
using System;

public class CQScript : Node
{
  // TODO : if you're reading this, better read this explanation I prepared -krColonia

  /* 
    CQScript - built in scripting/programming language used inside Code Quest
    It is built as an Interpreted language (like Python).

    If you wish to use this in a level, you must make an instance of the CQScriptBase Node
    
    The CQScriptBase Node has 3 main components:
    - Token : Splits, Tokenizes, and Lexically analyzes the code.
    - Parser : Takes code from tokenizer, parses the code with syntax and meaning for interpreter.
    - Interpreter : Reads the code, executes said code based on passed syntax type from Parser.

    These three components are what make up CQScript. Currently, it's only able to read one line at a time.


    P.S. - I'm working on making the game be able to read multiple lines and store variables, but this'll do for now.
    P.P.S. - tingin ko kaya pang i-optimize ung code sa 3 modules, pero for now di ko muna iisipin masyado since
             proof of concept pa lang ung base functions na i-dedevelop natin.
    
   */


  Token tokenizer = new Token();
  Parser parser = new Parser();
  Interpreter interpreter = new Interpreter();

  public string ConsolePrint = "";

  public string RunInterpreter(string input)
  {
    string ConsolePrint = "";

    // Calls Token method, retrieved Lexemes and Tokens after Tokenizing is done
    LexemesTokens lextokens = tokenizer.RetrieveLexemesTokens(input);

    // Initialize boolean variables for code analysis
    bool lexValid = lextokens.ValidLexes;
    bool parseValid = lextokens.ValidParse;

    if(lextokens.ValidLexes)
    {
      lextokens = parser.SyntaxParser(lextokens);
      // TODO : pang debug lang tong print na to, delete later
      GD.Print("ValidLexes  = " + lextokens.ValidLexes + ", ValidParse  = " + lextokens.ValidParse + ", SyntaxType  = " + lextokens.SyntaxType);
    }

    if(lextokens.ValidParse)
    {
      ConsolePrint = interpreter.InterpretCode(lextokens);
      // TODO : pang debug lang tong print na to, delete later
      GD.Print("ValidParse  = " + lextokens.ValidParse);
    }

    // Pinapasa tong console print pabalik sa level na pinag-gagamitan niya, lumalabas ung output sa in-game console/terminal window.
    return ConsolePrint;
  }
}