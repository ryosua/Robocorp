using UnityEngine;
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

	// vector for movement target (always lerps here)
	public Vector3 moveCoordinates;
	Vector3 oldPos;
	public bool moveBool;
	float speed;

	public void OnBuildBaseBotClick () {

		UnitType unitType = UnitType.Base;

		// Close build menu
		CloseBuildPanel();

		// Have the user place the character
		cameraControls.UnitToPlace = unitType;

		print ("Base built");
	}

	public void OnBuyWorkerBotClick () {
		// Subtract the cost of the purchase
		// Close build menu
		// Have the user place the character

		print ("Worker bought");
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

	public void OnBuyWorkerBotWithGoldClick () {
		// Subtract the cost of the purchase
		// Close build menu
		// Have the user place the character

		print ("Worker bought with gold.");
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