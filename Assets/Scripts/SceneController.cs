using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	public void GoToCreditsScene() {
		Application.LoadLevel("Credits");
	}

	public void GoToInstructionsScene() {
		Application.LoadLevel("Instructions");
	}

	public void GoToMenuScene() {
		Application.LoadLevel("MainMenu");
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