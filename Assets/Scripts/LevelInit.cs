using UnityEngine;
using System.Collections;

public class LevelInit : MonoBehaviour {

	// arbitrary test prefabs here, this is what we populate the field with
	public GameObject groundPanel;
	public GameObject testPlayer;

	// must type in grid sizes in-editor; make numbers even for best results
	public int panelSize;
	public int levelWidth;
	public int levelLength;

	// Use this for initialization
	void Start () {
		// spawn one block for every spot in the defined grid
		for (int i = 0; i < levelLength; i++) {
			for (int j = 0; j < levelWidth; j++) {
				// place panels with the middle at where this gameObject lies
				// if we randomize panel placement, that code will go above this
				Instantiate(groundPanel, new Vector3(transform.position.x - (panelSize * (levelWidth / 2)) + i*panelSize,
				                                     transform.position.y - (panelSize * (levelLength / 2)) + j*panelSize), this.transform.rotation);

			}
		}

		// spawn test sprite
		Instantiate (testPlayer, transform.position, transform.rotation);
	}
	

	// Update is called once per frame
	void Update () {
	
	}
}
