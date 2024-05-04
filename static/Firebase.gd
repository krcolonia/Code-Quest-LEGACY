extends Node

# Constants for accessing Firebase services through RESTful API

const API_KEY := "AIzaSyAWJri4kTZFR2SRanw-HYVCvytTSqcL9Ko"
const PROJECT_ID := "code-quest-game"

const REGISTER_URL := "https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=%s" % API_KEY
const LOGIN_URL := "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=%s" % API_KEY
const FIRESTORE_URL := "https://firestore.googleapis.com/v1/projects/%s/databases/(default)/documents/" % PROJECT_ID

# var current_token := ""

# Stores user info while in-game
var user_info = {}

func _get_token_id_from_result(result: Array) -> String:
  for i in result:
    print(i)
  var result_body := JSON.parse(result[3].get_string_from_ascii()).result as Dictionary
  return result_body.idToken

func _get_user_info(result: Array) -> Dictionary:
  var result_body := JSON.parse(result[3].get_string_from_ascii()).result as Dictionary
  return {
    "token": result_body.idToken,
    "id" : result_body.localId
  }

func _get_request_headers() -> PoolStringArray:
  return PoolStringArray([
    "Content-Type: application/json",
    "Authorization: Bearer %s" % user_info.token
  ])

# Firebase Authentication functions. Used mainly for register and login

func register(email: String, password: String, http: HTTPRequest) -> void:
  var body := {
    "email" : email,
    "password" : password,
  }
  http.request(REGISTER_URL, [], false, HTTPClient.METHOD_POST, to_json(body))
  var result := yield(http, "request_completed") as Array
  if result[1] == 200:
    user_info = _get_user_info(result)

func login(email: String, password: String, http: HTTPRequest) -> void:
  var body := {
    "email" : email,
    "password" : password,
    "returnSecureToken" : true
  }
  http.request(LOGIN_URL, [], false,HTTPClient.METHOD_POST, to_json(body))
  var result := yield(http, "request_completed") as Array
  if result[1] == 200:
    user_info = _get_user_info(result)

# Firestore CRUD operations

func save_document(path: String, fields: Dictionary, http: HTTPRequest) -> void:
  var document := { "fields" : fields }
  var body := to_json(document)
  var url := FIRESTORE_URL + path
  http.request(url, _get_request_headers(), false, HTTPClient.METHOD_POST, body)

func get_document(path: String, http: HTTPRequest) -> void:
  var url := FIRESTORE_URL + path
  http.request(url, _get_request_headers(), false, HTTPClient.METHOD_GET)

func update_document(path: String, fields: Dictionary, http: HTTPRequest) -> void:
  var document := { "fields" : fields }
  var body := to_json(document)
  var url := FIRESTORE_URL + path
  http.request(url, _get_request_headers(), false, HTTPClient.METHOD_PATCH, body)

func delete_document(path: String, http: HTTPRequest) -> void:
  var url := FIRESTORE_URL + path
  http.request(url, _get_request_headers(), false, HTTPClient.METHOD_DELETE)