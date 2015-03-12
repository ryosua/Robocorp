using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Other code.

public class UserController : MonoBehaviour {

	public SceneController sceneController;

	public InputField loginEmailField;
	public InputField loginPasswordField;

	public InputField registerEmailField;
	public InputField registerPasswordField;

	public void RegisterUser () {
		string email = registerEmailField.text;
		string password = registerPasswordField.text;
		
		print("The form was submitted.");
		print("Email: " + email);
		print("Password: " + password);

		sceneController.GoToLoginScene ();
	}

	public void Login () {
		string email = loginEmailField.text;
		string password = loginPasswordField.text;
		
		print("The form was submitted.");
		print("Email: " + email);
		print("Password: " + password);
		
		sceneController.GoToTestScene ();
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}