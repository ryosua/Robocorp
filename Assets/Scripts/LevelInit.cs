﻿using UnityEngine;
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
	public GameObject settlerUnit1;
	public GameObject settlerUnit2;
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

		// setup camera
		mainCamera.BroadcastMessage("SetParams");
		mainCamera.transform.position = mapArray [Mathf.FloorToInt (levelLength / 2), Mathf.FloorToInt (levelWidth / 2)].transform.position;
		mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, mainCamera.transform.position.y, -11);

		// setup players
		player1 = new PlayerController();
		player1.InitPlayer (1, startOre, startGold, startOil);
		
		player2 = new PlayerController();
		player2.InitPlayer (2, startOre, startGold, startOil);

		// spawn player1's settler on bottom of map
		settlerUnit1 = (GameObject)Instantiate (settlerUnit1, mapArray[Mathf.FloorToInt(levelLength/2), Mathf.FloorToInt(levelWidth/8)].transform.position, transform.rotation);
		settlerUnit1.BroadcastMessage("SetTile", mapArray[Mathf.FloorToInt(levelLength/2), Mathf.FloorToInt(levelWidth/8)]);
		settlerUnit1.BroadcastMessage ("SetOwner", 1);

		// add settler to player1's unit list, set unit's unitID
		settlerUnit1.GetComponent<PawnController>().unitID = player1.AddUnit(settlerUnit1);
		mainCamera.GetComponent<CameraControls> ().player1Base = settlerUnit1;


		// spawn player2's settler on bottom of map
		settlerUnit2 = (GameObject)Instantiate (settlerUnit2, mapArray[Mathf.FloorToInt(levelLength/2), Mathf.FloorToInt(levelWidth -(levelWidth/8))].transform.position, transform.rotation);
		settlerUnit2.BroadcastMessage("SetTile", mapArray[Mathf.FloorToInt(levelLength/2), Mathf.FloorToInt(levelWidth - (levelWidth/8))]);
		settlerUnit2.BroadcastMessage ("SetOwner", 2);

		// add settler to player1's unit list, set unit's unitID
		settlerUnit2.GetComponent<PawnController>().unitID = player2.AddUnit(settlerUnit2);
		mainCamera.GetComponent<CameraControls> ().player2Base = settlerUnit2;

		// spawn selector particle
		selectorParticle = (GameObject)Instantiate (selectorParticle, mapArray[Mathf.FloorToInt(levelLength/2), Mathf.FloorToInt(levelWidth/2)].transform.position, transform.rotation);
		selectorParticle.BroadcastMessage("SetTile", mapArray[Mathf.FloorToInt(levelLength/2), Mathf.FloorToInt(levelWidth/2)]);

		mainCamera.BroadcastMessage ("SetParams");
		mainCamera.BroadcastMessage ("UIResourceUpdate");
	}

	// Update is called once per frame
	void Update () {

	}
}
