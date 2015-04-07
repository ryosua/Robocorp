using UnityEngine;
using System.Collections;

public class OpenBuildMenu : MonoBehaviour {

	public GameObject buildMenu;
	public GameObject baseMenu;

	public void OnClick () {
		buildMenu.GetComponent<CanvasGroup>().alpha = 1;
		buildMenu.SetActive (true);
		print("Opening Build menu.");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
