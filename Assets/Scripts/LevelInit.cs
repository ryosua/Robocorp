using UnityEngine;
using System.Collections;

public class LevelInit : MonoBehaviour {

	// default starting game parameters
	public int startOre;
	public int startOil;
	public int startGold;

	// init prefabs here, this is what we populate the field with
	public GroundScript currentTile;
	public GameObject groundPanel;
	public GameObject mainCamera;
	public GameObject Resource1Panel;
	public GameObject testPlayer;
	public GameObject selectorParticle;
	public GameObject MapBlocker;
	public GameObject CameraNWEdge;
	public GameObject CameraSEEdge;

	// init players
	public PlayerController player1;
	public PlayerController player2;

	// logic arrays
	GameObject[,] mapArray;

	// must type in grid sizes in-editor; make numbers even for best results
	public int panelSize;
	public int levelWidth;
	public int levelLength;
	float tileRoll;

	// Use this for initialization
	void Start () {
		// init array to hold field
		mapArray = new GameObject [levelLength+2, levelWidth+2];

		// spawn one block for every spot in the defined grid
		for (int i = 0; i < levelLength; i++) {
			for (int j = 0; j < levelWidth; j++) {
				// start panels with the middle at where this gameObject lies
				// randomize panel placement
				tileRoll = Random.value;

				if (tileRoll >= 0.05) {
					mapArray[i+1, j+1] = (GameObject)Instantiate(groundPanel, new Vector3(transform.position.x - (panelSize * (levelWidth / 2)) + i*panelSize,
				    	                                 transform.position.y - (panelSize * (levelLength / 2)) + j*panelSize), this.transform.rotation);
				}
				else {
					mapArray[i+1,j+1] = (GameObject)Instantiate(Resource1Panel, new Vector3(transform.position.x - (panelSize * (levelWidth / 2)) + i*panelSize,
					                                     transform.position.y - (panelSize * (levelLength / 2)) + j*panelSize), this.transform.rotation);
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

		// stitch together map
		for (int i = 0; i < levelLength; i++) {
			for (int j = 0; j < levelWidth; j++) {
				// get tiles from array, add map info to each GroundScript
				currentTile = mapArray[i+1,j+1].GetComponent<GroundScript> ();

				// check for valid blocks
				if (mapArray[i,j+1] != null) {
					currentTile.left_block = mapArray[i,j+1];
				}
				if (mapArray[i+1,j] != null) {
					currentTile.down_block = mapArray[i+1,j];
				}
				if (mapArray[i+1,j+2] != null) {
					currentTile.up_block = mapArray[i+1,j+2];
				}
				if (mapArray[i+2,j+1] != null) {
					currentTile.right_block = mapArray[i+2,j+1];
				}
			}
		}

		// spawn camera blockers
		Instantiate (CameraNWEdge, new Vector3(transform.position.x - (panelSize * (levelWidth / 2)) - 3*panelSize,
		                                       transform.position.y + (panelSize * levelLength / 2) + 3*panelSize), this.transform.rotation);

		Instantiate (CameraSEEdge, new Vector3(transform.position.x + (panelSize * (levelWidth / 2)) + 3*panelSize,
		                                       transform.position.y - (panelSize * levelLength / 2) - 3*panelSize), this.transform.rotation);

		// spawn test sprite in middle of map
		testPlayer = (GameObject)Instantiate (testPlayer, mapArray[Mathf.FloorToInt(levelLength/2), Mathf.FloorToInt(levelWidth/2)].transform.position, transform.rotation);
		testPlayer.BroadcastMessage("SetTile", mapArray[Mathf.FloorToInt(levelLength/2), Mathf.FloorToInt(levelWidth/2)]);

		// spawn selector particle
		selectorParticle = (GameObject)Instantiate (selectorParticle, mapArray[Mathf.FloorToInt(levelLength/2), Mathf.FloorToInt(levelWidth/2)].transform.position, transform.rotation);
		selectorParticle.BroadcastMessage("SetTile", mapArray[Mathf.FloorToInt(levelLength/2), Mathf.FloorToInt(levelWidth/2)]);

		// setup camera
		mainCamera.BroadcastMessage("SetParams");

		// setup players
		player1 = new PlayerController();
		player1.InitPlayer (1, startOre, startGold, startOil);

		player2 = new PlayerController();
		player2.InitPlayer (2, startOre, startGold, startOil);
	}
	

	// Update is called once per frame
	void Update () {
	
	}
}
