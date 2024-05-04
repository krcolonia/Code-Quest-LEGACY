extends Control

onready var http: HTTPRequest = $HTTPRequest

onready var emailLabel: Label = $RegInputs/VBoxRegister/EmailContainer/Label
onready var email: LineEdit = $RegInputs/VBoxRegister/EmailContainer/LineEdit

onready var passwordLabel: Label = $RegInputs/VBoxRegister/PasswordContainer/Label
onready var password: LineEdit = $RegInputs/VBoxRegister/PasswordContainer/LineEdit

onready var confpassLabel: Label = $RegInputs/VBoxRegister/ConfPassContainer/Label
onready var confpass: LineEdit = $RegInputs/VBoxRegister/ConfPassContainer/LineEdit

func _ready():
  emailLabel.text = "E-mail Address"

  passwordLabel.text = "Password"
  password.secret = true

  confpassLabel.text = "Confirm Password"
  confpass.secret = true

func _on_HTTPRequest_request_completed(result: int, response_code: int, headers: PoolStringArray, body: PoolByteArray):
  var response_body := JSON.parse(body.get_string_from_ascii())
  if response_code != 200:
	  print(response_body.result.error.message.capitalize())
  else:
	  print("success")
	  yield(get_tree().create_timer(2.0), "timeout")
	  get_tree().change_scene("res://scenes/Menus/AccountManagement/Login.tscn")

func _on_RegisterAcc_pressed() -> void:
  if password.text != confpass.text or email.text.empty() or password.text.empty():
	  print("invalid")
	  return
  Firebase.register(email.text, password.text, http)
