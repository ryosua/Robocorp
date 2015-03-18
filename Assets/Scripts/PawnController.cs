using UnityEngine;
using System.Collections;

public class PawnController : MonoBehaviour {

	// set move speed
	public float speed;

	// commandable bool: for pawns player shouldn't be able to select
	public bool commandable = true;

	// movable bool: for pawns that shouldn't move (buildings, working units)
	public bool movable = true;

	// number of owning player
	public int owningPlayer;
	public int actionsPerTurn;
	public int movesPerAction;
	public GameObject currentTile;
	int currentActions;
	int currentMoves;

	// public for debugging purposes
	public Vector3 moveCoordinates;

	// function to declare the owner of a unit
	public void SetOwner(int playerNumber) {
		owningPlayer = playerNumber;
	}

	public void MoveTo(GameObject target) {

		// check if this pawn can move
		if (movable == true) {

			// check if the target is also a pawn (don't move onto another pawn)
			PawnController pc = target.GetComponent<PawnController> ();
			if (pc == null) {

				// set move coord when called
				moveCoordinates = target.transform.position;
				currentTile = target;
			}
		}
	}

	// turn function: process how many moves this pawn gets per turn here
	public void takeTurn () {

	}

	// Use this for initialization
	void Start () {
		// set default coords
		moveCoordinates = transform.position;
	}

	// Update is called once per frame
	void Update () {

		// move pawn to given location using Lerp and Update
		transform.position = Vector3.Lerp(transform.position, moveCoordinates, speed*Time.deltaTime);

	}


}
