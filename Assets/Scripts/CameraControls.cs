using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraControls : MonoBehaviour {

	RaycastHit2D hit;
	public GameObject hitObject;
	public GameObject selected;
	public GameObject selectorParticle;
	public GameObject CameraNWEdge;
	public GameObject CameraSEEdge;

	// dynamic UI elements
	public Text moveText;
	public Text actionText;
	public Text healthText;
	public Text attackText;
	public GameObject actionPanel;

	// get level init object (for player lists, unit lists)
	public GameObject levelInit;

	// mouse location values
	public float mouseX;
	public float mouseY;

	// bool to stop camera movement
	public bool paused;

	// keep track of current player
	public int currentPlayer;

	// Use this for initialization
	void Start () {
		currentPlayer = 1;
		paused = false;
	}

	// function to grab selector particle (called in init after it is created
	void SetParams() {
		selectorParticle = GameObject.FindGameObjectWithTag ("Selector Particle");
		CameraNWEdge = GameObject.FindGameObjectWithTag ("Map NW Corner");
		CameraSEEdge = GameObject.FindGameObjectWithTag ("Map SE Corner");
	}

	// set camera to other player between turns
	void TakeTurn() {

	}

	// function to set up UI when a pawn is clicked; UIState = 0 if not initialized, 1 if initialized
	void UIUpdatePawnInfo() {
		// set color and correct numbers for moves/actions on UI
		// check if the unit even has those
		PawnController selectedUnit = selected.transform.gameObject.GetComponent<PawnController>();

		// check if we have selected something
		if (selected != null) {

			// if it is movable, display moves and actions
			if (selectedUnit.movable == true) {

				moveText.color = Color.black;
				moveText.text = "Moves Remaining:\t" + selectedUnit.currentMoves.ToString ();
			}

			// in all cases, display actions, health, and attack (even if 0)
			actionText.color = Color.black;
			actionText.text = "Actions Remaining:\t" + selectedUnit.currentActions.ToString ();

			healthText.color = Color.black;
			healthText.text = "Unit Health:\t\t" + selectedUnit.health.ToString ();

			attackText.color = Color.black;
			attackText.text = "Attack Power:\t" + selectedUnit.attackDamage.ToString ();

			// activate actionPanel color
			actionPanel.GetComponent<Image>().color = Color.red;
		}
	}

	// function to remove UI elements when a pawn is clicked
	void UIDeselectPawn() {
		// assume no pawn is selected now
		moveText.color = Color.grey;
		moveText.text = "Moves Remaining:";

		actionText.color = Color.grey;
		actionText.text = "Actions Remaining:";

		healthText.color = Color.grey;
		healthText.text = "Unit Health:";
		
		attackText.color = Color.grey;
		attackText.text = "Attack Power:";
		
		// activate actionPanel color
		actionPanel.GetComponent<Image>().color = Color.grey;
	}
	
	// Update is called once per frame
	void Update(){

		mouseX = Input.mousePosition.x;
		mouseY = Input.mousePosition.y;

		// if we left click in the camera, select the pawn (if selectable)
		if (Input.GetMouseButtonDown (0)) {

			// cast a ray to see what was clicked
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			
			if (hit.collider != null) {
				if ((hit.transform.gameObject.tag == "Selectable") || (hit.transform.gameObject.tag == "Selectable and Movable")) {
					selected = hit.transform.gameObject;
					selectorParticle.GetComponent<ParticleBase>().selectedPawn = selected;

					// move selection particle
					selectorParticle.BroadcastMessage ("FlashTo", selected.transform.gameObject);
					selectorParticle.BroadcastMessage ("StartParticle");

					// update UI (flush it really quick in case we are going from one pawn to another
					UIDeselectPawn();
					UIUpdatePawnInfo();
				}

				// deselection code
				else {
					// deselect pawn, move selection particle off screen
					selected = null;
					selectorParticle.BroadcastMessage ("FlashTo", CameraSEEdge.transform.gameObject);

					// update UI
					UIDeselectPawn();
				}
			}
		}

		// if we right click in the camera, move the selected pawn to that grid space (or try to)
		else if (Input.GetMouseButtonDown (1)) {

			// cast a ray to see what was clicked
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

			// check if we hit a valid panel
			if (hit.collider != null) {
				// check if there is actually something selected
				if(selected != null) {
					// check if we are moving to an invalid block
					if (!(hit.transform.gameObject.tag == ("MovementBlocker"))) {

						// move the pawn using that pawn's code
						// determine direction with distance from selection
						float xDist = hit.transform.position.x - selected.transform.position.x;
						float yDist = hit.transform.position.y - selected.transform.position.y;

						if (Mathf.Abs (xDist) >= Mathf.Abs (yDist)) {

							if (xDist > 0) {
								selected.BroadcastMessage("MoveTo", 4);
							}
							else {
								selected.BroadcastMessage("MoveTo", 3);
							}
						}
						else {
							
							if (yDist > 0) {
								selected.BroadcastMessage("MoveTo", 1);
							}
							else {
								selected.BroadcastMessage("MoveTo", 2);
							}
						}
						// update UI
						UIUpdatePawnInfo();
					}
				}
			}

			hitObject = hit.transform.gameObject;
		}

		// if we hit up key
		else if (Input.GetKeyDown("up")) {

			// if we hit keys while something is selected...
			if (selected != null) {

				selected.BroadcastMessage ("MoveTo", 1);
				selectorParticle.BroadcastMessage ("MoveTo", 1);

				// update UI
				UIUpdatePawnInfo();
			}
		}

		// if we hit down key
		else if (Input.GetKeyDown("down")) {

			// if we hit keys while something is selected...
			if (selected != null) {
				
				selected.BroadcastMessage ("MoveTo", 2);
				selectorParticle.BroadcastMessage ("MoveTo", 2);

				// update UI
				UIUpdatePawnInfo();
			}
		}

		// if we hit left key
		else if (Input.GetKeyDown("left")) {

			// if we hit keys while something is selected...
			if (selected != null) {
				
				selected.BroadcastMessage ("MoveTo", 3);
				selectorParticle.BroadcastMessage ("MoveTo", 3);

				// update UI
				UIUpdatePawnInfo();
			}
		}

		// if we hit right key
		else if (Input.GetKeyDown("right")) {

			// if we hit keys while something is selected...
			if (selected != null) {
				
				selected.BroadcastMessage ("MoveTo", 4);
				selectorParticle.BroadcastMessage ("MoveTo", 4);

				// update UI
				UIUpdatePawnInfo();
			}
		}

		// camera movement
		if ((mouseX < 7) && (transform.position.x > CameraNWEdge.transform.position.x + 12) && (!paused)) {

			// move camera left
			transform.position =new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);

		}
		else if ((mouseX >Screen.width -7) && (transform.position.x < CameraSEEdge.transform.position.x - 13) && (!paused)) {

			// move camera right
			transform.position =new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);

		}

		if ((mouseY < 5) && (transform.position.y > CameraSEEdge.transform.position.y + 6) && (!paused)) {
			
			// move camera down
			transform.position =new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);

		}
		else if ((mouseY >Screen.height -7) && (transform.position.y < CameraNWEdge.transform.position.y - 7) && (!paused)) {
			
			// move camera up
			transform.position =new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
			
		}
	}
}
