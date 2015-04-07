using UnityEngine;
using System.Collections;

public class CloseBuildMenu : MonoBehaviour {

	public GameObject buildMenu;

	public void OnClick () {
		buildMenu.GetComponent<CanvasGroup>().alpha = 0;
		print("Closing Build menu.");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
