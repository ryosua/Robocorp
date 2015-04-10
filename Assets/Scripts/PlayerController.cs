using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerController {
	
	public int playerNumber;

	// unit bookkeeping
	public LinkedList<GameObject> unitList;
	LinkedList<GameObject>.Enumerator e;
	public int unitCount;
	public GameObject mainCamera;

	public int oreCount;
	public int goldCount;
	public int oilCount;

	// function to place default values to start game
	public void InitPlayer(int player_num, int startOre, int startGold, int startOil) {

		// default construct
		Start ();

		// set values
		playerNumber = player_num;
		oreCount = startOre;
		goldCount = startGold;
		oilCount = startOil;

	}

	// function to take turn for all of a player's units
	public void TakeTurn() {

		// reset enumerator
		e = unitList.GetEnumerator ();

		GameObject currentUnit;

		// while we aren't at the end of the list...
		while (e.MoveNext()) {

			// tell unit to take its turn
			currentUnit = e.Current;
			currentUnit.GetComponent<PawnController>().TakeTurn ();
		}
	}

	// getter for camera
	public void GetCamera(GameObject camera) {
		mainCamera = camera;
	}

	// function to add unit to player array
	// MAKE SURE THAT EVERYTHING ADDED HAS A TakeTurn FUNCTION!!
	public int AddUnit(GameObject unit) {

		// increase unit count
		unitCount = unitCount + 1;

		// add unit to list
		unitList.AddFirst (unit);

		// return unit list
		return unitCount;
	}

	// remove unit from the player array
	public void RemoveUnit(int unit_id) {

		// reset the enumerator
		e = unitList.GetEnumerator ();

		// while we aren't at the end of the list...
		while (e.MoveNext()) {

			// get the id of a given list object. If it matches...
			if (e.Current.GetComponent<PawnController>().unitID == unit_id) {

				// check if this player just lost
				if (e.Current.GetComponent<PawnController>().unitType == "Base") {
					mainCamera.GetComponent<CameraControls>().PlayerLoss(playerNumber);
				}

				// destroy the object
				unitList.Remove (e.Current);

				// does the player have any units left?
				if (unitList.Count == 0) {
					mainCamera.GetComponent<CameraControls>().PlayerLoss(playerNumber);
				}

				// break from while loop
				break;
			}
		}
	}

	// Use this for default initialization
	void Start () {

		// init linked list
		unitList = new LinkedList<GameObject> ();
		e = unitList.GetEnumerator();

		unitCount = 0;
		oreCount = 0;
		oilCount = 0;
		goldCount = 0;
	}
}
