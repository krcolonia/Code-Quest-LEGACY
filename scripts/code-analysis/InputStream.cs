using Godot;
using System;

public class InputStream : Node
{

  // ? Read the description I gave about this class -krColonia

  /* 
    InputStream Class

    This class is the most important component of the CQScript language.
    It takes in input (which is a String), and reads the input for every char from start to finish.

    I've given a description for each method in-case you want to know more about the main functions of this class.
   */


  // ? pos -> refers to the overall current position that InputStream is at in the input string.
  // ? line -> refers to which line InputStream is currently at. Incremented once it detects a new line or '\n'
  // ? col -> refers to the current position of the InputStream at the current line.
  private int pos = 0, line = 1, col = 0;

  // ? input is our input string from within the game's code workspace
  private string input;

  // ? Constructor method, used to retrieve the input string from in-game code workspace
  public InputStream(string input)
  {
    this.input = input;
  }

  // ? Next method, checks the next character and removes the previous character from the input stream.
  // ? Also checks if the current input switches to a new line.
  public char Next()
  {
    char ch = input[pos++];
    if(ch == '\n')
    {
      line++;
      col = 0;
    }
    else
    {
      col++;
    }
    return ch;
  }

  // ? Peek method, checks the current character without removing it from the input stream.
  public char Peek()
  {
    return pos < input.Length ? input[pos] : '\0';
  }

  // ? EOF method, checks if InputStream has reached the very end of the input.
  public bool EOF()
  {
    return Peek() == '\0';
  }

  // ? Croak method, used for exceptions within CQScript.
  // ? Returns a specified message from the method's arguments, then points to which line and column the error has taken place.
  public string croak(string msg)
  {
    GD.Print($"{msg} (Line {line}: Column {col})");
    return $"{msg} (Line {line}: Column {col})";
  }

}