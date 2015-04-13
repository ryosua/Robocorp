using UnityEngine;
using System.Collections;

public class CloseBuildMenu : MonoBehaviour {

	public GameObject MainCamera;
	
	public void OnClick () {
		BuildController cntl = MainCamera.GetComponent<BuildController> ();
		cntl.SetBuildMenuVisible (false);
		cntl.CloseUnitPanel (cntl.getSelectedPawnType ());
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}