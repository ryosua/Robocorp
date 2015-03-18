using UnityEngine;
using System.Collections;

public class LevelInit : MonoBehaviour {

	// arbitrary test prefabs here, this is what we populate the field with
	public GroundScript currentTile;
	public GameObject groundPanel;
	public GameObject mainCamera;
	public GameObject Resource1Panel;
	public GameObject testPlayer;
	public GameObject selectorParticle;
	public GameObject MapBlocker;
	public GameObject CameraNWEdge;
	public GameObject CameraSEEdge;

	// must type in grid sizes in-editor; make numbers even for best results
	public int panelSize;
	public int levelWidth;
	public int levelLength;
	float tileRoll;

	// Use this for initialization
	void Start () {
		// spawn one block for every spot in the defined grid
		for (int i = 0; i < levelLength; i++) {
			for (int j = 0; j < levelWidth; j++) {
				// start panels with the middle at where this gameObject lies
				// randomize panel placement
				tileRoll = Random.value;

				if (tileRoll >= 0.05) {
					Instantiate(groundPanel, new Vector3(transform.position.x - (panelSize * (levelWidth / 2)) + i*panelSize,
				    	                                 transform.position.y - (panelSize * (levelLength / 2)) + j*panelSize), this.transform.rotation);
					// get current tile, stitch the tiles together to from grid
					currentTile = groundPanel.GetComponent<GroundScript>();
				}
				else {
					Instantiate(Resource1Panel, new Vector3(transform.position.x - (panelSize * (levelWidth / 2)) + i*panelSize,
					                                     transform.position.y - (panelSize * (levelLength / 2)) + j*panelSize), this.transform.rotation);
					currentTile = Resource1Panel.GetComponent<GroundScript>();
				}
			}
		}

		// Spawn Map edge
		for (int i = 0; i < levelLength + 6; i++) {
			for (int j = 0; j < levelWidth + 6; j++) {
				if (((i < 3) || (i > levelLength + 2)) || ((j < 3) || (j > levelWidth + 2))) {
					Instantiate(MapBlocker, new Vector3(transform.position.x - (panelSize * (levelWidth / 2)) + i*panelSize - 3*panelSize,
					                                 	transform.position.y - (panelSize * (levelLength / 2)) + j*panelSize - 3*panelSize), this.transform.rotation);
				}
			}
		}

		// spawn camera blockers
		Instantiate (CameraNWEdge, new Vector3(transform.position.x - (panelSize * (levelWidth / 2)) - 3*panelSize,
		                                       transform.position.y + (panelSize * levelLength / 2) + 3*panelSize), this.transform.rotation);

		Instantiate (CameraSEEdge, new Vector3(transform.position.x + (panelSize * (levelWidth / 2)) + 3*panelSize,
		                                       transform.position.y - (panelSize * levelLength / 2) - 3*panelSize), this.transform.rotation);

		// spawn test sprite
		Instantiate (testPlayer, transform.position, transform.rotation);

		// spawn selector particle
		Instantiate (selectorParticle, new Vector3(transform.position.x, transform.position.y, transform.position.z - 4), transform.rotation);
		mainCamera.BroadcastMessage ("SetParams");
	}
	

	// Update is called once per frame
	void Update () {
	
	}
}
