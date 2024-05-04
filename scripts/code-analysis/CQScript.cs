using Godot;
using System;
using System.Collections.Generic;
using Interpreter;
using Statements;
using Environment;

public class CQScript : Node
{
  // ? If you'd like a better experience reading this code,
  // ? I suggest downloading the "Better Comments" extension in VS Code by Aaron Bond
  // ? It color-codes comments based on the symbol at the start. Very useful. -krColonia
  // ! Too bad the colors don't work on multi-line comments like the one below this very comment.

  /* 
    CQScript - built in scripting/programming language used inside Code Quest
    It is built as an Interpreted language that is sandboxed within the game.
    The language is built this way so that it won't be able to interfere with the inner-workings of the game's code,
    which is programmed with C#.

    This base script is used inside the CodeUI scene, which should be imported in scenes that are meant to be game levels.
    This script is also a necessity, as it pretty much calls every single class needed in order to parse, interpret, and run codes.
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
    tokenlist.Add(new Token(TokenType.EOF, null)); // ? adds an EOF token at the end of the token list. needed for the parser to know where the input ends.

    // ! The following commented lines are used to test the tokenizer.
    // ! this just prints every token generated into the console.
    // ! I used specifically to see how the tokenizer splits each piece of code.
    // ! don't un-comment unless you know what you're doing.

    foreach (Token item in tokenlist)
    {
      if (item.Type == TokenType.Terminator)
      {
        ConsolePrint += $"{item.Type}:{item.Value} ";
        ConsolePrint += "\n";
      }
      else if (item.Type == TokenType.EOF)
      {
        ConsolePrint += "END OF FILE.";
      }
      else
      {
        ConsolePrint += $"{item.Type}:{item.Value} ";
      }
    }

    // Parser parse = new Parser(tokenizer, tokenlist, ConsolePrint);

    // Program parsedProg = parse.ProduceAST();
    /* Environment.Environment env = new Environment.Environment();

    Statements.Eval.EvaluateProgram(parsedProg, env); */ // TODO -- make the interpeter output basic arithmetic operations into in-game code workspace console. 

    // ConsolePrint += tokenizer.GetConsolePrint();

    return ConsolePrint;
  }
}