using UnityEngine;
using System.Collections;

public class GoToMenuOnEscapePress : MonoBehaviour {

	public SceneController sceneController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			sceneController.GoToMenuScene ();
		}
	}
}