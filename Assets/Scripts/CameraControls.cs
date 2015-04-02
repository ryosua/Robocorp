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
	public GameObject player1Base;
	public GameObject player2Base;

	// dynamic UI elements
	public Text moveText;
	public Text actionText;
	public Text healthText;
	public Text attackText;
	public Text playerText;
	public Text oilText;
	public Text goldText;
	public Text oreText;
	public Button endTurn;
	public GameObject actionPanel;

	// get level init object (for player lists, unit lists)
	public GameObject levelInit;

	// turn bool
	int turn;

	// camera move bool
	bool moveBool;

	// vector for movement target (always lerps here)
	public Vector3 moveCoordinates;

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
		moveBool = false;
		paused = false;
		turn = 1;
	}

	// sets camera focus on end turn; for the given player, the camera flies to this object on their turn start
	void SetBase(int playerID, GameObject baseObject) {
		if (playerID == 1) {
			player1Base = baseObject;
		} 
		else if (playerID == 2) {
			player2Base = baseObject;
		}
	}

	// function to grab selector particle (called in init after it is created
	void SetParams() {

		// setup camera params
		selectorParticle = GameObject.FindGameObjectWithTag ("Selector Particle");
		CameraNWEdge = GameObject.FindGameObjectWithTag ("Map NW Corner");
		CameraSEEdge = GameObject.FindGameObjectWithTag ("Map SE Corner");
	}

	// set camera to other player between turns
	public void TakeTurn() {
		if (turn == 1) {

			// do turn bookkeeping
			levelInit.GetComponent<LevelInit> ().player2.TakeTurn ();
			turn = 0;

			// move camera to player 2's base
			moveCoordinates = new Vector3(player2Base.transform.position.x, player2Base.transform.position.y, -10);
			moveBool = true;
			currentPlayer = 2;
			playerText.text = "Player 2";
			UIResourceUpdate();
		} 
		else {

			// do turn bookkeeping
			levelInit.GetComponent<LevelInit>().player1.TakeTurn ();
			turn = 1;

			// move camera to player 1's base
			moveCoordinates = new Vector3(player1Base.transform.position.x, player1Base.transform.position.y, -10);
			moveBool = true;
			currentPlayer = 1;
			playerText.text = "Player 1";
			UIResourceUpdate();
		}
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

	// function to update resource UI
	public void UIResourceUpdate() {
		// update all UI text as above on resource panel
		PlayerController player;

		if (currentPlayer == 1) {
			player = levelInit.GetComponent<LevelInit> ().player1;
		} 
		else {
			player = levelInit.GetComponent<LevelInit> ().player2;
		}

		oilText.text = "Oil:\t" + player.oilCount.ToString ();
		goldText.text = "Gold:\t" + player.goldCount.ToString ();
		oreText.text = "Ore:\t" + player.oreCount.ToString ();
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

					// make sure the current player owns this pawn
					if (hit.transform.gameObject.GetComponent<PawnController>().owningPlayer == currentPlayer) {

						// update selection
						selected = hit.transform.gameObject;
						selectorParticle.GetComponent<ParticleBase>().selectedPawn = selected;

						// move selection particle
						selectorParticle.BroadcastMessage ("FlashTo", selected.transform.gameObject);
						selectorParticle.BroadcastMessage ("StartParticle");

						// update UI (flush it really quick in case we are going from one pawn to another
						UIDeselectPawn();
						UIUpdatePawnInfo();
					}
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
								if (selected.GetComponent<PawnController>().MoveTo(4) == 0) {
									moveCoordinates = new Vector3(selected.GetComponent<PawnController>().moveCoordinates.x, selected.GetComponent<PawnController>().moveCoordinates.y, -10);
									moveBool = true;
								}
							}
							else {
								if (selected.GetComponent<PawnController>().MoveTo(3) == 0) {
									moveCoordinates = new Vector3(selected.GetComponent<PawnController>().moveCoordinates.x, selected.GetComponent<PawnController>().moveCoordinates.y, -10);
									moveBool = true;
								}
							}
						}
						else {
							
							if (yDist > 0) {
								if (selected.GetComponent<PawnController>().MoveTo(1) == 0) {
									moveCoordinates = new Vector3(selected.GetComponent<PawnController>().moveCoordinates.x, selected.GetComponent<PawnController>().moveCoordinates.y, -10);
									moveBool = true;
								}
							}
							else {
								if (selected.GetComponent<PawnController>().MoveTo(2) == 0) {
									moveCoordinates = new Vector3(selected.GetComponent<PawnController>().moveCoordinates.x, selected.GetComponent<PawnController>().moveCoordinates.y, -10);;
									moveBool = true;
								}
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

				if (selected.GetComponent<PawnController>().MoveTo(1) == 0) {
					moveCoordinates = new Vector3(selected.GetComponent<PawnController>().moveCoordinates.x, selected.GetComponent<PawnController>().moveCoordinates.y, -10);
					moveBool = true;
				}

				// update UI
				UIUpdatePawnInfo();
			}
		}

		// if we hit down key
		else if (Input.GetKeyDown("down")) {

			// if we hit keys while something is selected...
			if (selected != null) {
				
				if (selected.GetComponent<PawnController>().MoveTo(2) == 0) {
					moveCoordinates = new Vector3(selected.GetComponent<PawnController>().moveCoordinates.x, selected.GetComponent<PawnController>().moveCoordinates.y, -10);
					moveBool = true;
				}

				// update UI
				UIUpdatePawnInfo();
			}
		}

		// if we hit left key
		else if (Input.GetKeyDown("left")) {

			// if we hit keys while something is selected...
			if (selected != null) {
				
				if (selected.GetComponent<PawnController>().MoveTo(3) == 0) {
					moveCoordinates = new Vector3(selected.GetComponent<PawnController>().moveCoordinates.x, selected.GetComponent<PawnController>().moveCoordinates.y, -10);
					moveBool = true;
				}

				// update UI
				UIUpdatePawnInfo();
			}
		}

		// if we hit right key
		else if (Input.GetKeyDown("right")) {

			// if we hit keys while something is selected...
			if (selected != null) {
				
				if (selected.GetComponent<PawnController>().MoveTo(4) == 0) {
					moveCoordinates = new Vector3(selected.GetComponent<PawnController>().moveCoordinates.x, selected.GetComponent<PawnController>().moveCoordinates.y, -10);
					moveBool = true;
				}

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

		// code to move camera to a location (at turn end)
		if (moveBool == true) {
			// check if we moved
			Vector3 oldPosition = transform.position;
			transform.position = Vector3.Lerp(transform.position, moveCoordinates, 10*Time.deltaTime);

			if (oldPosition == transform.position) {
				moveBool = false;
			}
		}
	}
}
