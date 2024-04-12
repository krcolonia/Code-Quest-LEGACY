using Godot;
using System;

public class MenuBtn : Button
{
  // TODO : Move this script from scenes folder over to scripts/ui-elements for better file management.

  public override void _Ready()
  {

  }

  private void _on_Button_pressed()
  {
    GetNode<AudioStreamPlayer>("/root/BtnClick").Play();
  }
}
