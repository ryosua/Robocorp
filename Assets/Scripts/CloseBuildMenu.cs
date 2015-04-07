using UnityEngine;
using System.Collections;

public class CloseBuildMenu : MonoBehaviour {

	public GameObject buildMenu;

	public void OnClick () {
		buildMenu.SetActive (false);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
