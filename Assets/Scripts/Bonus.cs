using UnityEngine;
using System.Collections;

public class Bonus : Special {

	private CameraControls cameraControls;

	public Bonus (CameraControls cameraControls) {
		this.cameraControls = cameraControls;
	}

	/*
		A bonus gives the player a bonus of 10 gold.
	*/
	public override void Reaction () {
		int bonusAmount = 10;
		PlayerController playerController = cameraControls.GetPlayerController ();
		playerController.goldCount = playerController.goldCount + bonusAmount;
		cameraControls.goldText.text = "Gold:	" + playerController.goldCount.ToString ();
		Debug.Log ("You earned a bonus of " + bonusAmount + "gold!");
	}
}