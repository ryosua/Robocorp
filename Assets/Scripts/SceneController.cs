using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	public void GoToRegisterScene() {
		Application.LoadLevel("RegisterUser");
	}

	public void GoToLoginScene() {
		Application.LoadLevel("Login");
	}

	public void GoToTestScene() {
		Application.LoadLevel("TestScene");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
