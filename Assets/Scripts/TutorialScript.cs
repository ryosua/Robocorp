using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

	public GameObject panel1;
	public GameObject panel2;
	public GameObject panel3;
	public GameObject panel4;

	// Use this for initialization
	void Start () {
		panel1.SetActive (true);
		panel2.SetActive (false);
		panel3.SetActive (false);
		panel4.SetActive (false);
	}

	public void GoToPanel (int panelNum) {

		// shut off all panels
		panel1.SetActive (false);
		panel2.SetActive (false);
		panel3.SetActive (false);
		panel4.SetActive (false);

		if (panelNum == 1) {
			panel1.SetActive (true);
		}
		else if (panelNum == 2) {
			panel2.SetActive (true);
		}
		else if (panelNum == 3) {
			panel3.SetActive (true);
		}
		else if (panelNum == 4) {
			panel4.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
