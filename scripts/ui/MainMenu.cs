using Godot;
using System;

public class MainMenu : Control
{

  public override void _Ready()
  {

  }

  private void _on_StartBtn_pressed()
  {
    GetTree().ChangeScene("res://scenes/level1.tscn");
  }

  private void _on_ExitBtn_pressed()
  {
    if (GetNode<AudioStreamPlayer>("/root/BtnClick").Playing == false)
    {
      GetTree().Quit();
    }


  }



}