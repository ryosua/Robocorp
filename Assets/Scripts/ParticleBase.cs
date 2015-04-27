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

		PawnController currentPawn;
		// update all movement vars at once (instantaneous movement)
		transform.position = target.transform.position;

		// check if this is actually a pawn
		if ((currentPawn = target.GetComponent<PawnController> ()) != null) {
			// get current tile if so
			currentTile = currentPawn.currentTile;
			moveCoordinates = currentTile.transform.position;
		} 
		else {
			// send particle safely off screen, deselect pawn
			selectedPawn = null;
			moveCoordinates = target.transform.position;
		}
	}

	// function to start a particle playing
	void StartParticle() {
		if (ps.isPlaying == false) {
			ps.Play ();
		}
	}

	// function to stop a particle
	void StopParticle() {
		if (ps.isPlaying == true) {
			ps.Stop ();
		}
	}
	
	public override int MoveTo(int direction) {
		// delibrately blank
		return 0;
	}

	void Update () {

		// if selected pawn isn't null...
		if (selectedPawn != null) {

			// set move coords to this pawn (stick to it)
			moveCoordinates = selectedPawn.transform.position;

			// move pawn to given location using Lerp and Update
			transform.position = Vector3.Lerp(transform.position, moveCoordinates, (speed* 10)*Time.deltaTime);
		}
	}
}
