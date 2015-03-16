using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	RaycastHit2D hit;
	public GameObject hitObject;
	public GameObject selected;
	public GameObject selectorParticle;
	public float mouseX;
	public GameObject CameraNWEdge;
	public GameObject CameraSEEdge;

	// Use this for initialization
	void Start () {

	}

	// function to grab selector particle (called in init after it is created
	void SetParams() {
		selectorParticle = GameObject.FindGameObjectWithTag ("Selector Particle");
		CameraNWEdge = GameObject.FindGameObjectWithTag ("Map NW Corner");
		CameraSEEdge = GameObject.FindGameObjectWithTag ("Map SE Corner");
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

					// move selection particle
					selectorParticle.BroadcastMessage ("MoveTo", selected.transform.gameObject.transform.position);
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
						selected.BroadcastMessage ("MoveTo", hit.transform.gameObject.transform.position);
						selectorParticle.BroadcastMessage ("MoveTo", hit.transform.gameObject.transform.position);
					}
				}
			}

			hitObject = hit.transform.gameObject;
		}

		if ((Input.mousePosition.x <20)) {

			// move camera left
			transform.position =new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);

		}
		else if ((Input.mousePosition.x >Screen.width -20)) {

			// move camera right
			transform.position =new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);

		}

		if ((Input.mousePosition.y <20)) {
			
			// move camera down
			transform.position =new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);

		}
		else if ((Input.mousePosition.y >Screen.height -20)) {
			
			// move camera up
			transform.position =new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
			
		}
	}
}
