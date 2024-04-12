using Godot;
using System;
using System.Collections.Generic;

public class CQScript : Node
{
  /* 
    CQScript - built in scripting/programming language used inside Code Quest
    It is built as an Interpreted language (like Python) that is sandboxed within the game.
    The language is built this way so that it won't be able to interfere with the inner-workings of the game's code,
    which is programmed with C#.

    If you wish to use this in a level, you must make an instance of the CQScriptBase Node
   */

  public string ConsolePrint;

  public string RunInterpreter(string input)
  {
    string ConsolePrint = "";

    InputStream stream = new InputStream(input);

    TokenStream tokenizer = new TokenStream(stream, ConsolePrint);

    List<Token> tokenlist = new List<Token>();

    while (!tokenizer.EOF())
    {
      Token token = tokenizer.Next();
      tokenlist.Add(token); // ? stores all the read tokens into a list
      // TODO -- pass the tokenlist over to the Parser, which generates ASTs based on the input code
    }

    Parser parse = new Parser(tokenlist, ConsolePrint);

    /* foreach (Token item in tokenlist)
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
    */

    ConsolePrint += tokenizer.GetConsolePrint();

    return ConsolePrint;
  }
}