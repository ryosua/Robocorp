﻿using UnityEngine;
using System.Collections;

public class BuildController : MonoBehaviour {

	public GameObject BuildPanel;
	public GameObject HeadquartersPanel;
	public GameObject MainCamera;

	public void OnBuildBaseBotClick () {

		UnitType unitType = UnitType.Base;

		// Close build menu
		CloseUnitPanel (unitType);

		CameraControls cameraControls = MainCamera.GetComponent<CameraControls> ();

		// Have the user place the character
		cameraControls.UnitToPlace = unitType;

		print ("Base built");
	}

	public void OnBuySettlerBotClick () {
		// Subtract the cost of the purchase
		// Close build menu
		// Have the user place the character

		print ("Settler bought");
	}

	public void OnBuyHeavyBotClick () {
		// Subtract the cost of the purchase
		// Close build menu
		// Have the user place the character

		print ("Heavy bought");
	}

	public void OnBuyMeleeBotClick () {
		// Subtract the cost of the purchase
		// Close build menu
		// Have the user place the character 

		print ("Melee bought");
	}

	public void OnBuySettlerBotWithGoldClick () {
		// Subtract the cost of the purchase
		// Close build menu
		// Have the user place the character

		print ("Settler bought with gold.");
	}
	
	public void OnBuyHeavyBotWithGoldClick () {
		// Subtract the cost of the purchase
		// Close build menu
		// Have the user place the character

		print ("Heavy bought with gold.");
	}
	
	public void OnBuyMeleeBoWithGoldClick () {
		// Subtract the cost of the purchase
		// Close build menu
		// Have the user place the character 

		print ("Melee bought with gold.");
	}

	public void OnBuyFortificationsWithGoldClick () {
		// Subtract the cost of the purchase
		// Close build menu
		// Have the user place the character

		print ("Fortifications bought.");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowUnitPanel (UnitType unitType) {
		print ("Showing unit panel." + unitType);
		switch (unitType) {
			
			case UnitType.Base:
				// Open Headquarters Panel
				print ("Showing action panel for headquarters unit.");
				break;
			case UnitType.SettlerBot:
				// Open Settler Bot Panel
				print ("Showing action panel for SettlerBot unit.");
				print (HeadquartersPanel);
				HeadquartersPanel.SetActive (true);
				break;
			case UnitType.WorkerBot:
				// Open Worker Bot Panel
				break;
			case UnitType.MeleeBot:
				// Open Melee Bot Panel
				break;
			case UnitType.HeavyBot:
				// Open Heavy Bot Panel
				break;
			default:
				// The button should not be shown here becuase the selected unit does not have any corresponding actions.
				break;
		}
	}

	public void CloseUnitPanel (UnitType unitType) {
		switch (unitType) {
			
		case UnitType.Base:
			// Close Settler Bot Panel
			print("Closing headquarters panel.");
			print (HeadquartersPanel);
			HeadquartersPanel.SetActive (false);
			break;
		case UnitType.SettlerBot:
			// Close Settler Bot Panel
			print("Closing settler bot panel.");
			break;
		case UnitType.WorkerBot:
			// Close Worker Bot Panel
			break;
		case UnitType.MeleeBot:
			// Close Melee Bot Panel
			break;
		case UnitType.HeavyBot:
			// Close Heavy Bot Panel
			break;
		default:
			// The button should not be shown here becuase the selected unit does not have any corresponding actions.
			break;
		}
	}

	public UnitType getSelectedPawnType() {
		UnitType type;
		
		CameraControls camera = MainCamera.GetComponent<CameraControls> ();
		GameObject lastSelected = camera.lastSelected;
		PawnController pawn = lastSelected.GetComponent<PawnController> ();
		type = pawn.GetUnitType ();
		
		return type;
	}

	public void SetBuildMenuVisible (bool visible) {
		BuildPanel.SetActive (visible);
	}
}