using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	RaycastHit2D hit;
	public GameObject hitObject;
	public GameObject selected;
	public GameObject selectorParticle;
	public GameObject CameraNWEdge;
	public GameObject CameraSEEdge;
	public float mouseX;

	public bool paused;

	public int currentPlayer = 1;

	// Use this for initialization
	void Start () {
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
	
	// Update is called once per frame
	void Update(){

		mouseX = Input.mousePosition.x;

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
			}
		}

		// if we hit down key
		else if (Input.GetKeyDown("down")) {

			// if we hit keys while something is selected...
			if (selected != null) {
				
				selected.BroadcastMessage ("MoveTo", 2);
				selectorParticle.BroadcastMessage ("MoveTo", 2);
			}
		}

		// if we hit left key
		else if (Input.GetKeyDown("left")) {

			// if we hit keys while something is selected...
			if (selected != null) {
				
				selected.BroadcastMessage ("MoveTo", 3);
				selectorParticle.BroadcastMessage ("MoveTo", 3);
			}
		}

		// if we hit right key
		else if (Input.GetKeyDown("right")) {

			// if we hit keys while something is selected...
			if (selected != null) {
				
				selected.BroadcastMessage ("MoveTo", 4);
				selectorParticle.BroadcastMessage ("MoveTo", 4);
			}
		}

		// camera movement
		if ((Input.mousePosition.x <10) && (transform.position.x > CameraNWEdge.transform.position.x + 12) && (!paused)) {

			// move camera left
			transform.position =new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);

		}
		else if ((Input.mousePosition.x >Screen.width -10) && (transform.position.x < CameraSEEdge.transform.position.x - 13) && (!paused)) {

			// move camera right
			transform.position =new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);

		}

		if ((Input.mousePosition.y <10) && (transform.position.y > CameraSEEdge.transform.position.y + 6) && (!paused)) {
			
			// move camera down
			transform.position =new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);

		}
		else if ((Input.mousePosition.y >Screen.height -10) && (transform.position.y < CameraNWEdge.transform.position.y - 7) && (!paused)) {
			
			// move camera up
			transform.position =new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
			
		}
	}
}
