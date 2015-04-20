﻿using UnityEngine;
using System.Collections;

public class BuildController : MonoBehaviour {

	public GameObject BuildPanel;
	public GameObject HeadquartersPanel;
	public GameObject WorkerPanel;
	public GameObject MeleePanel;
	public GameObject HeavyPanel;
	public GameObject FortificationPanel;
	public GameObject MinePanel;
	public GameObject MainCamera;
	CameraControls cameraControls;

	UnitType unit;

	// vector for movement target (always lerps here)
	public Vector3 moveCoordinates;
	Vector3 oldPos;
	public bool moveBool;
	float speed;

	public void OnBuildBaseBotClick () {

		unit = UnitType.Base;

		// Close build menu
		CloseBuildPanel();

		// Spawn the unit nearby
		cameraControls.SpawnSelection (unit);

		if (cameraControls.currentPlayer == 1) {
			cameraControls.levelInit.GetComponent<LevelInit> ().settlerUnit1.GetComponent<PawnController> ().Destroy ();
		} 
		else {
			cameraControls.levelInit.GetComponent<LevelInit> ().settlerUnit2.GetComponent<PawnController> ().Destroy ();
		}

		print ("Base built");
	}

	public void OnBuyWorkerBotClick () {
		UnitType unit = UnitType.WorkerBot;
		
		// Close build menu
		CloseBuildPanel();
		
		// Spawn the unit nearby
		cameraControls.SpawnSelection (unit);

		print ("Worker bought");
	}

	public void OnBuyHeavyBotClick () {
		UnitType unit = UnitType.HeavyBot;
		
		// Close build menu
		CloseBuildPanel();
		
		// Spawn the unit nearby
		cameraControls.SpawnSelection (unit);

		print ("Heavy bought");
	}

	public void OnBuyMeleeBotClick () {
		UnitType unit = UnitType.MeleeBot;
		
		// Close build menu
		CloseBuildPanel();
		
		// Spawn the unit nearby
		cameraControls.SpawnSelection (unit); 

		print ("Melee bought");
	}

	// Use this for initialization
	void Start () {
		cameraControls = MainCamera.GetComponent<CameraControls> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void ShowUnitPanel (UnitType unitType) {
		print ("Showing unit panel." + unitType);

		// clear panel initially
		HeadquartersPanel.SetActive (false);
		WorkerPanel.SetActive (false);
		MeleePanel.SetActive (false);
		HeavyPanel.SetActive (false);
		FortificationPanel.SetActive (false);
		MinePanel.SetActive (false);

		switch (unitType) {
			
			case UnitType.Base:
				// Show Headquarters build options
				WorkerPanel.SetActive (true);
				WorkerPanel.GetComponent<PanelScript>().EnableWithOwner(cameraControls.currentPlayer);

				MeleePanel.SetActive (true);
				MeleePanel.GetComponent<PanelScript>().EnableWithOwner(cameraControls.currentPlayer);

				HeavyPanel.SetActive (true);
				HeavyPanel.GetComponent<PanelScript>().EnableWithOwner(cameraControls.currentPlayer);
				break;

			case UnitType.SettlerBot:
				// Open Settler Bot Panel
				HeadquartersPanel.SetActive (true);
				HeadquartersPanel.GetComponent<PanelScript>().EnableWithOwner(cameraControls.currentPlayer);
				break;

			case UnitType.WorkerBot:
				// Open Worker Bot Panel
				FortificationPanel.SetActive (true);
				FortificationPanel.GetComponent<PanelScript>().EnableWithOwner(cameraControls.currentPlayer);

				MinePanel.SetActive (true);
				MinePanel.GetComponent<PanelScript>().EnableWithOwner(cameraControls.currentPlayer);
				break;

			case UnitType.MeleeBot:
				// no options for meleebot
				break;

			case UnitType.HeavyBot:
				// no options for heavybot
				break;
			default:
				// The button should not be shown here becuase the selected unit does not have any corresponding actions.
				break;
		}
	}

	public void CloseBuildPanel () {

		// clear build panel in any case
		SetBuildMenuVisible (false);
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

		// set move control to true
		moveBool = true;

		BuildPanel.SetActive (visible);
	}
}