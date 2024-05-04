extends Control

onready var logregBox: HBoxContainer = $BGCanvas/CanvasLayer/HBoxContainer

# Called when the node enters the scene tree for the first time.
func _ready():
  print(str(Firebase.user_info))
  if Firebase.user_info.empty():
    logregBox.visible = true
  else:
    logregBox.visible = false


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
  pass


func _on_LoginBtn_pressed():
	get_tree().change_scene("res://scenes/Menus/AccountManagement/Login.tscn")

func _on_RegisterBtn_pressed():
	get_tree().change_scene("res://scenes/Menus/AccountManagement/Register.tscn")

func _on_StartBtn_pressed():
	get_tree().change_scene("res://scenes/Levels/tutorial/TutLvl.tscn")

func _on_OptionBtn_pressed():
	get_tree().change_scene("res://scenes/Menus/Options.tscn")

func _on_ExitBtn_pressed():
	get_tree().quit()