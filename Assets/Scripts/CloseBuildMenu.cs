using UnityEngine;
using System.Collections;

public class CloseBuildMenu : MonoBehaviour {

	public GameObject buildMenu;
	public GameObject mainCamera;

	public void OnClick () {
		buildMenu.SetActive (false);
		CloseUnitPanel (getSelectedPawnType());
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private UnitType getSelectedPawnType() {
		UnitType type;
		
		CameraControls camera = mainCamera.GetComponent<CameraControls> ();
		GameObject lastSelected = camera.lastSelected;
		PawnController pawn = lastSelected.GetComponent<PawnController> ();
		type = pawn.GetUnitType ();
		
		return type;
	}

	private void CloseUnitPanel (UnitType pawnType) {
		switch (pawnType) {
			
		case UnitType.SettlerBot:
			// Open Settler Bot Panel
			print("Closing settler bot panel.");
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
}