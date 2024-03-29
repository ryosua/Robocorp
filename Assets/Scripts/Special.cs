﻿/*
	A model class representing a special tile feature. Trap and Bonus are subclasses.
*/
public abstract class Special {

	private CameraControls cameraControls;
	private bool used;

	public Special (CameraControls cameraControls) {
		this.cameraControls = cameraControls;
		used = false;
	}

	/*
		What is called when a player moves to this space.
	*/
	public void OnSpecialEncounter () {
		if (used == false) {
			Reaction ();
		}
		used = true;
	}

	/*
		The effect of the special, call OnSpecialEncounter instead.
	*/
	public abstract void Reaction ();

	protected CameraControls GetCameraControls () {
		return cameraControls;
	}
}