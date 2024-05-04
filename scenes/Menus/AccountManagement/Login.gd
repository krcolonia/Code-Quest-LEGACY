extends Control

onready var http: HTTPRequest = $HTTPRequest

onready var emailLabel: Label = $LogInputs/VBoxLogin/EmailContainer/Label
onready var email: LineEdit = $LogInputs/VBoxLogin/EmailContainer/LineEdit

onready var passwordLabel: Label = $LogInputs/VBoxLogin/PasswordContainer/Label
onready var password: LineEdit = $LogInputs/VBoxLogin/PasswordContainer/LineEdit

func _ready():
  emailLabel.text = "E-mail Address"
  
  passwordLabel.text = "Password"
  password.secret = true

func _on_LoginAcc_pressed():
  if email.text.empty() or password.text.empty():
	  print("enter email and password")
  else:
    print("signed success")
    yield(get_tree().create_timer(2.0), "timeout")
    get_tree().change_scene("res://main.tscn")

func _on_HTTPRequest_request_completed(result:int, response_code:int, headers:PoolStringArray, body:PoolByteArray):
  pass # Replace with function body.
