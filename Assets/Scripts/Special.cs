/*
	A model class representing a special tile feature. Trap and Bonus are subclasses.
*/
public abstract class Special {

	private bool used;

	public Special () {
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
		The effect of the special.
	*/
	public abstract void Reaction ();
}