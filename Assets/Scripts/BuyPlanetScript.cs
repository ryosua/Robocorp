using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyPlanetScript : MonoBehaviour {

	public int goldToWin;
	public GameObject MainCamera;
	public Text question;
	private CameraControls camera;

	public void OnBuyButtonClick ()
	{
		bool hasEnoughGold = camera.GetPlayerController () .goldCount >= goldToWin;

		if (hasEnoughGold == true) {
			TriggerWin ();
		}
	}

	// Use this for initialization
	void Start () {
		camera = MainCamera.GetComponent<CameraControls> ();
		goldToWin = camera.winGoldCount;
		question.text = "Buy the planet for " + camera.winGoldCount.ToString () + " Gold?";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void TriggerWin () {
		// The other player loses the game.
		int currentPlayer = camera.currentPlayer;
		int loser;
		if (currentPlayer == 1) {
			loser = 2;
		}
		else {
			loser = 1;
		}
		camera.PlayerLoss(loser);
	}
}
