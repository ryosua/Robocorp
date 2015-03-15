using UnityEngine;
using System.Collections;

public class PawnController : MonoBehaviour {

	// set move speed
	public float speed;
	public bool commands = true;

	// public for debugging purposes
	public Vector3 moveCoordinates;

	public void MoveTo(Vector3 coord) {
		// set move coord when called
		moveCoordinates = coord;
		commands = false;
	}

	// Use this for initialization
	void Start () {
		moveCoordinates = transform.position;
	}


	
	// Update is called once per frame
	void Update () {

		// move pawn to given location using Lerp and Update
		transform.position = Vector3.Lerp(transform.position, moveCoordinates, speed*Time.deltaTime);


	}


}
