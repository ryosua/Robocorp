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

			GameObject currentTile = currentUnit.GetComponent<PawnController>().currentTile;
		
			string tileType = currentTile.GetComponent<GroundScript>().tileType;

			//Debug.Log(tileType);

			switch (tileType)
			{
			case "Oil1":
				oilCount++;
				break;

			case "Oil2":
				oilCount += 2;
				break;

			case "Oil3":
				oilCount +=3;
				break;

			case "Ore1":
				oreCount++;
				break;

			case "Ore2":
				oreCount += 2;
				break;

			case "Ore3":
				oreCount += 3;
				break;

			case "Gold1":
				goldCount++;
				break;

			case "Gold2":
				goldCount += 2;
				break;

			case "Gold3":
				goldCount += 3;
				break;

			}
		}

		//Debug.Log (unitList.Count);
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

	public void RemoveUnit(int unit_id) {

		// reset the enumerator
		e = unitList.GetEnumerator ();

		// while we aren't at the end of the list...
		while (e.MoveNext()) {

			// get the id of a given list object. If it matches...
			if (e.Current.GetComponent<PawnController>().unitID == unit_id) {

				// destroy the object
				unitList.Remove (e.Current);

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
