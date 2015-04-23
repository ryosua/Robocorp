using UnityEngine;
using System.Collections;

public class Trap : Special {

	public Trap (CameraControls cameraControls)
		: base(cameraControls) {
	}
	
	/*
		A trap halves the health of the robot that encounters it.
	*/
	public override void Reaction () {
		// Destroy the selected pawn.
		GetCameraControls ().selected.GetComponent<PawnController> ().health = Mathf.CeilToInt(GetCameraControls ().selected.GetComponent<PawnController> ().health / 2);
		GetCameraControls ().GetComponent<NotificationController> ().ShowNotification ("You hit a trap, your unit's health was halved!");
	}
	
}