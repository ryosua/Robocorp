using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Other code.

public class UserController : MonoBehaviour {

	public InputField emailField;
	public InputField passwordField;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void RegisterUser () {
		string email = emailField.text;
		string password = passwordField.text;

		print("The form was submitted.");
		print("Email: " + email);
		print("Password: " + password);
	}
	
}
