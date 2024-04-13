using Godot;
using System;

public class CodingUI : Node
{
  CQScript cqscriptBase = new CQScript();

  public override void _Ready()
  {

  }

  public override void _Input(InputEvent @event)
  {
    base._Input(@event);

    var input = GetNode<TextEdit>("CanvasLayer/VBoxContainer/CodeWorkspace");
    var console = GetNode<TextEdit>("CanvasLayer/VBoxContainer/CodeConsole");

    if (Input.IsKeyPressed(16777254))
    {
      GetNode<AudioStreamPlayer>("/root/BtnClick").Play();
      console.Text = "C:\\Users\\Player>\n" + cqscriptBase.RunInterpreter(input.Text);
    }
  }

  // TODO :  implement timer per level. might probably implement it using an instantiated scene again -krColonia

  // private void _on_CheckBtn_pressed()
  // {
  //   var input = GetNode<TextEdit>("VBoxContainer/CodeWorkspace");
  //   var console = GetNode<TextEdit>("VBoxContainer/CodeConsole");

  //   GD.Print("Your input is:\n" + input.Text + "\n");

  //   console.Text = "C:\\Users\\Player>\n" + cqscriptBase.RunInterpreter(input.Text);
  // }
}
