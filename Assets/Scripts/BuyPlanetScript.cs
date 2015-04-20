using UnityEngine;
using System.Collections;

public class BuyPlanetScript : MonoBehaviour {

	public int goldToWin = 100;
	public GameObject MainCamera;
	private CameraControls cameraControls;

	public void OnBuyButtonClick ()
	{
		bool hasEnoughGold = cameraControls.GetPlayerController () .goldCount >= goldToWin;

		if (hasEnoughGold == true) {
			TriggerWin ();
		}
	}

	// Use this for initialization
	void Start () {
		cameraControls = MainCamera.GetComponent<CameraControls> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void TriggerWin () {
		// The other player loses the game.
		int currentPlayer = cameraControls.currentPlayer;
		int loser;
		if (currentPlayer == 1) {
			loser = 2;
		}
		else {
			loser = 1;
		}
		cameraControls.PlayerLoss(loser);
	}
}
