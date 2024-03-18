using Godot;
using System;
using System.Collections.Generic;

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

    List<Token> tokenlist = new List<Token>();

    // TODO -- put the parser here once it is DONE
    while (!tokenizer.EOF())
    {
      Token token = tokenizer.Next();
      // TODO -- make a 2D List that stores all the tokens in a line, and separates each line into its own item in the list
      // TODO -- make a way to pass this list onto the Parser
      // TODO -- DEVELOP THE DAMN PARSER, KURT -krcolonia (yes, this is a reminder to myself.)
      tokenlist.Add(token); // ? stores all the read tokens into a list
    }

    foreach (Token item in tokenlist)
    {
      if (item.Type == TokenType.Terminator)
      {
        ConsolePrint += $" {item.Type}:{item.Value} ";
        ConsolePrint += "\n";
      }
      else
      {
        ConsolePrint += $" {item.Type}:{item.Value} ";
      }
    }

    ConsolePrint += tokenizer.GetConsolePrint();

    return ConsolePrint;
  }
}