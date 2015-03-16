using UnityEngine;
using System.Collections;

public class PawnController : MonoBehaviour {

	// set move speed
	public float speed;
	public bool commandable = true;
	public int OwningPlayer;

	// public for debugging purposes
	public Vector3 moveCoordinates;

	public void MoveTo(Vector3 coord) {
		// set move coord when called
		moveCoordinates = coord;
	}

	// Use this for initialization
	void Start () {
		// set default coords
		moveCoordinates = transform.position;
	}


	
	// Update is called once per frame
	void Update () {

		// move pawn to given location using Lerp and Update
		transform.position = Vector3.Lerp(transform.position, moveCoordinates, speed*Time.deltaTime);

	}


}
