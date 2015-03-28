using UnityEngine;
using System.Collections;

public class PawnController : Pawn {

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
	public int currentActions;
	public int currentMoves;

	public Vector3 moveCoordinates;

	// function to declare the owner of a unit
	public void SetOwner(int playerNumber) {
		owningPlayer = playerNumber;
	}

	// function to set initial tile
	public void SetTile(GameObject tile) {
		currentTile = tile;
	}

	// function to move a given pawn on grid (directions: up = 1, down = 2, left = 3, right = 4)
	public override void MoveTo(int direction) {

		// store direction in nextTile to simplify code
		GameObject nextTile;

		// get block from direction
		switch (direction)
		{
		case 1:
			nextTile = currentTile.GetComponent<GroundScript>().up_block;
			break;
		case 2:
			nextTile = currentTile.GetComponent<GroundScript>().down_block;
			break;
		case 3:
			nextTile = currentTile.GetComponent<GroundScript>().left_block;
			break;;
		case 4:
			nextTile = currentTile.GetComponent<GroundScript>().right_block;
			break;
		default:
			// errored out, return
			return;
		}


		// check if this pawn can move
		if (movable == true) {

			// check if we are moving to a valid space
			if (nextTile != null) {

				// check if unit is commandable
				if (commandable == true) {

					// check if the tile is occupied
					if (nextTile.GetComponent<GroundScript>().occupied != true) {

						// check if the pawn has moves left
						if (currentMoves > 0) {
						
							// set move coord when called
							moveCoordinates = nextTile.transform.position;
						
							// subtract moves
							currentMoves = currentMoves -1;
						
							// set occupied for current, next tile

							currentTile.GetComponent<GroundScript>().occupied = false;
							nextTile.GetComponent<GroundScript>().occupied = true;
							// set new currentTile
							currentTile = nextTile;
						}
					}
				}
			}
		}
		else {
			// UI code for "invalid move"
		}
	}

	// turn function: process how many moves this pawn gets per turn here
	public void takeTurn () {

		// refresh moves
		currentMoves = movesPerAction;
		currentActions = actionsPerTurn;
	}

	// Use this for initialization
	void Start () {
		// set default coords
		moveCoordinates = transform.position;

		// set default moves
		currentMoves = movesPerAction;
		currentActions = actionsPerTurn;
	}

	// Update is called once per frame
	void Update () {

		// move pawn to given location using Lerp and Update
		transform.position = Vector3.Lerp(transform.position, moveCoordinates, speed*Time.deltaTime);

	}


}
