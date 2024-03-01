using Godot;
using System;

public class MenuBtn : Button
{
  // Declare member variables here. Examples:
  // private int a = 2;
  // private string b = "text";

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    
  }

  private void _on_Button_pressed()
  {
    GetNode<AudioStreamPlayer>("/root/BtnClick").Play();
  }
}
