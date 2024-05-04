extends Control

# var db_ref = Firebase.Database.get_database.reference("test-data", {})

# Called when the node enters the scene tree for the first time.
func _ready():
	# $FirebaseTestLabel.text = str(db_ref.Child("data").Child())
  pass

func _on_ReturnMenu_pressed():
  get_tree().change_scene("res://main.tscn")

# _on_db_data_update():
#   db_ref.connect("new_data_update", self, "_on_db_data_update")
#   db_ref.connect("patch_data_update", self, "_on_db_data_update")
#   db_ref.connect("delete_data_update", self, "_on_db_data_update")
