using UnityEngine;
using System.Collections;

public abstract class Pawn : MonoBehaviour {
	abstract public void MoveTo (int direction);
}