using UnityEngine;
using System.Collections;

public class GroundScript : MonoBehaviour {

	public GameObject up_block;
	public GameObject down_block;
	public GameObject left_block;
	public GameObject right_block;
	public GameObject occupiedObject;
	public bool occupied;

	// public string for the tile type
	public string tileType;

	private Special special;

	// Use this for initialization
	void Start () {
		occupied = false;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public Special getSpecial () {
		return special;
	}

	public void SetOccupant(GameObject occupant) {
		occupiedObject = occupant;
		occupied = true;
	}
	
	public void SetSpecial (Special special) {
		this.special = special;
	}
}