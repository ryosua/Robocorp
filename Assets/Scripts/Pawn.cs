using UnityEngine;
using System.Collections;

public abstract class Pawn : MonoBehaviour {
	abstract public int MoveTo (int direction);
}