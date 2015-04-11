using UnityEngine;
using System.Collections;

public class OpenBuildMenu : MonoBehaviour {

	public GameObject buildMenu;
	public GameObject mainCamera;
	
	public void OnClick () {
		buildMenu.SetActive (true);

		ShowUnitPanel (getSelectedPawnType());
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private PawnType getSelectedPawnType() {
		PawnType type;

		CameraControls camera = mainCamera.GetComponent<CameraControls> ();
		GameObject lastSelected = camera.lastSelected;
		PawnController pawn = lastSelected.GetComponent<PawnController> ();
		type = pawn.GetPawnType ();

		return type;
	}

	private void ShowUnitPanel (PawnType pawnType) {
		switch (pawnType) {

		case PawnType.SettlerBot:
			// Open Settler Bot Panel
			print("Showing settler bot panel.");
			break;
		case PawnType.WorkerBot:
			// Open Worker Bot Panel
			break;
		case PawnType.MeleeBot:
			// Open Melee Bot Panel
			break;
		case PawnType.HeavyBot:
			// Open Heavy Bot Panel
			break;
		default:
			// The button should not be shown here becuase the selected unit does not have any corresponding actions.
			break;
		}
	}
}