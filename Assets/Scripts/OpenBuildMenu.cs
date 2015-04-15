using UnityEngine;
using System.Collections;

public class OpenBuildMenu : MonoBehaviour {

	public GameObject MainCamera;
	public BuildController cntl;
	
	public void OnClick () {
		cntl.SetBuildMenuVisible (true);
		cntl.ShowUnitPanel (cntl.getSelectedPawnType());
	}
	
	// Use this for initialization
	void Start () {
		cntl = MainCamera.GetComponent<BuildController> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}