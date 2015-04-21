using UnityEngine;
using System.Collections;

public class Trap : Special {

	public Trap (CameraControls cameraControls)
		: base(cameraControls) {
	}
	
	/*
		A trap destroys the robot that encounters it.
	*/
	public override void Reaction () {
		// Destroy the selected pawn.
		GetCameraControls ().selected.GetComponent<PawnController> ().Destroy ();
		Debug.Log ("You hit a trap, your selected unit died :(");
	}
	
}