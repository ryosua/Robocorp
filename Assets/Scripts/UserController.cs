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

	private readonly string server_url = "http://ist446-ryosua.rhcloud.com/register.php";

	public void RegisterUser () {
		string email = registerEmailField.text;
		string password = registerPasswordField.text;

		// Post to server.
		WWWForm form = new WWWForm();
		form.AddField("username", email);
		form.AddField("password", password);
		WWW postRequest = new WWW(server_url, form);

		if (postRequest.error == null) {
			// TODO If the user exists
			sceneController.GoToLoginScene ();
		} else {
			print("Post error:" + postRequest.error);
		}
	
		print("The form was submitted.");
		print("Email: " + email);
		print("Password: " + password);
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