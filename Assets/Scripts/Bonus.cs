using UnityEngine;
using System.Collections;

public class Bonus : Special {
	
	public Bonus (CameraControls cameraControls)
		: base(cameraControls) {
	}

	/*
		A bonus gives the player a bonus of 10 gold.
	*/
	public override void Reaction () {
		int bonusAmount = 10;
		PlayerController playerController = GetCameraControls() .GetPlayerController ();
		playerController.goldCount = playerController.goldCount + bonusAmount;
		GetCameraControls() .goldText.text = "Gold:	" + playerController.goldCount.ToString ();
		GetCameraControls ().GetComponent<NotificationController> ().ShowNotification ("You earned a bonus of " + bonusAmount + " gold!");
	}
}