using Godot;
using System;

public class MainMenu : Control
{

  public override void _Ready()
  {

  }

  public override void _Process(float delta)
  {
    // Called every frame. Delta is time since the last frame.
    // Update game logic here
  }
  
  private void _on_LoginBtn_pressed()
  {
    GetTree().ChangeScene("res://scenes/Menus/AccountManagement/Login.tscn");
  }

  private void _on_RegisterBtn_pressed()
  {
    GetTree().ChangeScene("res://scenes/Menus/AccountManagement/Register.tscn");
  }

  private void _on_StartBtn_pressed()
  {
    GetTree().ChangeScene("res://scenes/Levels/tutorial/TutLvl.tscn");
  }

  private void _on_OptionBtn_pressed()
  {
    GetTree().ChangeScene("res://scenes/Menus/Options.tscn");
  }
  private void _on_ExitBtn_pressed()
  {
    GetTree().Quit();
  }


}
