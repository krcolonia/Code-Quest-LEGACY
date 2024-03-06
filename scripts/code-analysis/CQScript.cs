using Godot;
using System;

public class CQScript : Node
{
  // ? if you're reading this, better read this explanation I prepared -krColonia

  /* 
    CQScript - built in scripting/programming language used inside Code Quest
    It is built as an Interpreted language (like Python) that is sandboxed within the game.
    The language is built this way so that it won't be able to interfere with the inner-workings of the game's code.

    If you wish to use this in a level, you must make an instance of the CQScriptBase Node

    P.S. - tingin ko kaya pang i-optimize ung code sa 3 modules, pero for now di ko muna iisipin masyado since
           proof of concept pa lang ung base functions na i-dedevelop natin.
    
   */

  public string ConsolePrint;

  public string RunInterpreter(string input)
  {
    string ConsolePrint = "";

    InputStream stream = new InputStream(input);

    TokenStream tokenizer = new TokenStream(stream, ConsolePrint);

    while (!tokenizer.EOF())
    {
      Token token = tokenizer.Next();
      ConsolePrint += $"(Type: {token.Type}, Value: {token.Value})\n";
    }

    ConsolePrint += tokenizer.GetConsolePrint();

    return ConsolePrint;
  }
}