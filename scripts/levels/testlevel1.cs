using Godot;
using System;

public class testlevel1 : Node
{
  CQScript cqscriptBase = new CQScript();

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
	
  }

  // TODO :  implement timer per level. might probably implement it using an instantiated scene again -krColonia

  //  // Called every frame. 'delta' is the elapsed time since the previous frame.
  //  public override void _Process(float delta)
  //  {
  //      
  //  }

  private void _on_CheckBtn_pressed()
  {
	var input = GetNode<TextEdit>("VBoxContainer/CodeWorkspace");
	var console = GetNode<TextEdit>("VBoxContainer/CodeConsole");

	GD.Print("Your input is:\n" + input.Text + "\n");

	console.Text = "C:\\Users\\Player>\n" + cqscriptBase.RunInterpreter(input.Text);
  }
}
