using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleBase : PawnController {

	ParticleSystem ps;
	public GameObject selectedPawn;


	// Use this for initialization
	void Start () {
		// get particle system
		ps = (ParticleSystem)GetComponent ("ParticleSystem");

		commandable = false;
		selectedPawn = transform.gameObject;
	}

	// function to move particle to selected area
	void FlashTo(GameObject target) {

		// update all movement vars at once (instantaneous movement)
		transform.localPosition = target.transform.position;
		currentTile = target.GetComponent<PawnController>().currentTile;
		moveCoordinates = currentTile.transform.position;
	}

	// function to start a particle playing
	void StartParticle() {
		if (ps.isPlaying == false) {
			ps.Play ();
		}
	}
	
	public override void MoveTo(int direction) {
		// delibrately blank
		return;
	}

	void Update () {
		moveCoordinates = selectedPawn.transform.position;

		// move pawn to given location using Lerp and Update
		transform.position = Vector3.Lerp(transform.position, moveCoordinates, (speed* 10)*Time.deltaTime);

	}
}
