using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIController : MonoBehaviour {

	public Text playerText;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnEndTurnPress () {
		if (playerText.text == "Player 1") {
			playerText.text = "Player 2";
		}
		else if (playerText.text == "Player 2") {
			playerText.text = "Player 1";
		}
	}

}