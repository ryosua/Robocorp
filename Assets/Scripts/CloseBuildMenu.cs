using UnityEngine;
using System.Collections;

public class CloseBuildMenu : MonoBehaviour {

	public GameObject MainCamera;
	public BuildController cntl;
	
	public void OnClick () { 
		cntl.SetBuildMenuVisible (false);
		cntl.CloseBuildPanel ();
	}

	// Use this for initialization
	void Start () {
		cntl = MainCamera.GetComponent<BuildController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}