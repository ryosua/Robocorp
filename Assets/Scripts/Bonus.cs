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
		int bonusAmount = 5;
		CameraControls cameraControls = GetCameraControls ();
		PlayerController playerController = cameraControls.GetPlayerController ();
		playerController.goldCount = playerController.goldCount + bonusAmount;
		cameraControls.goldText.text = "Gold:	" + playerController.goldCount.ToString ();
		NotificationController notifcationController = cameraControls.GetComponent<NotificationController> ();
		notifcationController.ShowNotification ("You earned a bonus of " + bonusAmount + " gold!");
	}
}