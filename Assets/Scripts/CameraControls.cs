using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	public RaycastHit2D hit;
	public GameObject selected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update(){

		// if we left click in the camera, select the pawn (if selectable)
		if (Input.GetMouseButtonDown (0)) {

			// cast a ray to see what was clicked
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			
			if (hit.collider != null) {
				if ((hit.transform.gameObject.tag == "Selectable") || (hit.transform.gameObject.tag == "Selectable and Movable")) {
					selected = hit.transform.gameObject;
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
					// move the pawn using that pawn's code
					selected.BroadcastMessage ("MoveTo", hit.transform.gameObject.transform.position);
				}
			}
		}
	}
}
