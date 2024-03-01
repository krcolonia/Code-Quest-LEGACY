using Godot;
using System;
using System.CodeDom;

public class testlevel1 : Node
{
  // Declare member variables here. Examples:
  // private int a = 2;
  // private string b = "text";

  Analyzer analyzer = new Analyzer();

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    
  }

  //  // Called every frame. 'delta' is the elapsed time since the previous frame.
  //  public override void _Process(float delta)
  //  {
  //      
  //  }

  private void _on_CheckBtn_pressed()
  {
    var input = GetNode<TextEdit>("TextEdit");

    GD.Print("Your input is: " + input.Text);

    analyzer.CodeCheck(input.Text);
  }
}
