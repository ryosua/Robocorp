using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GroundScript : MonoBehaviour {

	public GameObject up_block;
	public GameObject down_block;
	public GameObject left_block;
	public GameObject right_block;
	public GameObject occupiedObject;
	public Sprite blue_territory;
	public Sprite red_territory;
	public Sprite neutral_territory;
	public bool occupied;
	public bool isResource;

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

	// function to claim game territory
	public void ClaimTerritory(int currentPlayer) {

		if (isResource) {

			if (currentPlayer == 1) {
				transform.gameObject.GetComponent<SpriteRenderer> ().sprite = red_territory;
			} else {
				transform.gameObject.GetComponent<SpriteRenderer> ().sprite = blue_territory;
			}
		}
	}

	public void LoseTerritory() {
		transform.gameObject.GetComponent<SpriteRenderer> ().sprite = neutral_territory;
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