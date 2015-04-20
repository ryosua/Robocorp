using UnityEngine;
using System.Collections;

public class Trap : Special {
	
	/*
		A trap destroys the robot that encounters it.
	*/
	public override void Reaction () {
		Debug.Log ("You hit a trap");
	}
	
}