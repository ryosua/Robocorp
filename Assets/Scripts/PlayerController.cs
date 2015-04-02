using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerController {
	
	public int playerNumber;

	// unit bookkeeping
	public LinkedList<GameObject> unitList;
	LinkedList<GameObject>.Enumerator e;
	public int unitCount;

	public int oreCount;
	public int goldCount;
	public int oilCount;

	// function to place default values to start game
	public void InitPlayer(int player_num, int startOre, int startGold, int startOil) {

		// set values
		playerNumber = player_num;
		oreCount = startOre;
		goldCount = startGold;
		oilCount = startOil;

	}

	// function to take turn for all of a player's units
	public void TakeTurn() {

		GameObject currentUnit;

		// while we aren't at the end of the list...
		while (e.MoveNext()) {

			// tell unit to take its turn
			currentUnit = e.Current;
			currentUnit.GetComponent<PawnController>().TakeTurn ();
		}

		// reset enumerator
		e = unitList.GetEnumerator ();
	}

	// function to add unit to player array
	// MAKE SURE THAT EVERYTHING ADDED HAS A TakeTurn FUNCTION!!
	public int AddUnit(GameObject unit) {

		// increase unit count
		unitCount = unitCount + 1;

		// add unit to list
		unitList.AddLast (unit);

		// return unit list
		return unitCount;
	}

	public void RemoveUnit(int unit_id) {

		// while we aren't at the end of the list...
		while (e.MoveNext()) {

			// get the id of a given list object. If it matches...
			if (e.Current.GetComponent<PawnController>().unitID == unit_id) {

				// destroy the object
				unitList.Remove (e.Current);

				// reset the enumerator, break
				e = unitList.GetEnumerator ();
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
