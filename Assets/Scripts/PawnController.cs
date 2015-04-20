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
	public int unitID;
	private UnitType unitType;

	// economy stats
	public int goldCost;
	public int oreCost;
	public int oilCost;

	// action stats
	public bool canAct;	// to designate pawns that can use the action button
	public int actionsPerTurn;
	public int movesPerAction;
	public int currentActions;
	public int currentMoves;

	public GameObject mainCamera;

	// tile info
	public GameObject currentTile;

	// combat stats
	public int health;
	public int attackDamage;

	// vector for movement target (always lerps here)
	public Vector3 moveCoordinates;

	// function to declare the owner of a unit
	public void SetOwner(int playerNumber) {
		owningPlayer = playerNumber;
	}

	public void SetUnitType(UnitType unitType) {
		this.unitType = unitType;
	}

	public UnitType GetUnitType() {
		return unitType;
	}

	// function to set initial tile
	public void SetTile(GameObject tile) {
		currentTile = tile;
	}

	// function to set main camera
	public void SetCamera(GameObject camera) {
		mainCamera = camera;
	}

	// code to attack a target (set by camera)
	public void Attack(GameObject target) {

		if (currentActions > 0) {

			// subtract damage from target
			target.transform.gameObject.GetComponent<PawnController> ().health = target.transform.gameObject.GetComponent<PawnController> ().health - attackDamage;

			// check if object should be destroyed
			if (target.transform.gameObject.GetComponent<PawnController> ().health <= 0) {
				target.transform.gameObject.GetComponent<PawnController> ().Destroy ();
			}

			// decrement current action count
			currentActions = currentActions - 1;
		}
	}

	// code to self-destruct (if health reaches 0, or if chosen)
	public void Destroy() {

		// reset the tile info
		currentTile.GetComponent<GroundScript> ().occupied = false;
		currentTile.GetComponent<GroundScript> ().occupiedObject = null;

		// delete this unit from the owning player's unit list
		if (owningPlayer == 1) {
			mainCamera.GetComponent<CameraControls>().levelInit.GetComponent<LevelInit>().player1.RemoveUnit (unitID);
		} 
		else {
			mainCamera.GetComponent<CameraControls>().levelInit.GetComponent<LevelInit>().player2.RemoveUnit (unitID);
		}

		// delete this object
		GameObject.Destroy (transform.gameObject);
	}

	// function to move a given pawn on grid (directions: up = 1, down = 2, left = 3, right = 4)
	public override int MoveTo(int direction) {

		// store direction in nextTile to simplify code
		GameObject nextTile;
		GroundScript groundScript = currentTile.GetComponent<GroundScript> ();

		// get block from direction
		switch (direction)
		{
		case 1:
			nextTile = groundScript.up_block;
			break;
		case 2:
			nextTile = groundScript.down_block;
			break;
		case 3:
			nextTile = groundScript.left_block;
			break;
		case 4:
			nextTile = groundScript.right_block;
			break;
		default:
			// errored out, return
			return -1;
		}

		// check if this pawn can move
		if (movable == true) {

			// check if we are moving to a valid space
			if (nextTile != null) {

				// If the tile has a special, trigger the encounter.
				Special special = groundScript.getSpecial ();
				if (special != null) {
					special.OnSpecialEncounter ();
				}

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
							groundScript.occupied = false;
							groundScript.occupiedObject = null;
							nextTile.GetComponent<GroundScript>().occupied = true;
							nextTile.GetComponent<GroundScript>().occupiedObject = transform.gameObject;

							// set new currentTile
							currentTile = nextTile;

							return 0;
						}
					}
					else {
						// this space is occupied
						if (nextTile.GetComponent<GroundScript>().occupiedObject.GetComponent<PawnController>().owningPlayer != owningPlayer) {
							Attack (nextTile.GetComponent<GroundScript>().occupiedObject);
							return 1;
						}
					}
				}
			}
		}

		// if we are here, we can't move
		return -1;
	}

	// turn function: process how many moves this pawn gets per turn here
	public void TakeTurn () {

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
