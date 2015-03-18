using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleBase : PawnController {

	ParticleSystem ps;


	// Use this for initialization
	void Start () {
		// get particle system
		ps = (ParticleSystem)GetComponent ("ParticleSystem");
	}

	// function to move particle to selected area
	void FlashTo(GameObject target) {
		// update all movement vars at once (instantaneous movement)
		transform.localPosition = target.transform.position;
		moveCoordinates = target.transform.position;
		currentTile = target;
	}

	// function to start a particle playing
	void StartParticle() {
		if (ps.isPlaying == false) {
			ps.Play ();
		}
	}
	
}
