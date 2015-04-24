using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour {

	public GameObject redImage;
	public GameObject blueImage;

	public GameObject prefab;

	public Text orePrice;
	public Text goldPrice;
	public Text oilPrice;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// function to turn on one of the pictures. 1 for red team, 2 for blue team
	public void EnableWithOwner(int owningPlayer) {
		// automatically disable both on start
		redImage.SetActive (false);
		blueImage.SetActive (false);

		if (owningPlayer == 1) {
			redImage.SetActive (true);
		} 
		else {
			blueImage.SetActive(true);
		}
	}
}
