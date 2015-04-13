using UnityEngine;
using System.Collections;

public class OpenBuildMenu : MonoBehaviour {

	public GameObject MainCamera;
	
	public void OnClick () {
		BuildController cntl = MainCamera.GetComponent<BuildController> ();
		cntl.SetBuildMenuVisible (true);
		cntl.ShowUnitPanel (cntl.getSelectedPawnType());
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}