using UnityEngine;
using System.Collections;

public class BuildController : MonoBehaviour {


	public CameraControls cameraControls;
	public CloseBuildMenu closeScript;

	public void OnBuildBaseBotClick () {

		UnitType unitType = UnitType.Base;

		// Close build menu
		closeScript.CloseUnitPanel (unitType);

		// Have the user place the character
		cameraControls.UnitToPlace (unitType);

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
}
