using UnityEngine;
using System.Collections;

public class Bonus : Special {

	/*
		A bonus gives the player a bonus of 10 gold.
	*/
	public override void Reaction () {
		Debug.Log ("You earned a bonus");
	}

}