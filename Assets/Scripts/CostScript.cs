using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CostScript : MonoBehaviour {
	
	public GameObject prefab;
	private PawnController prefabUnit;
	public Text goldCost;
	public Text oreCost;
	public Text oilCost;
	
	
	// Use this for initialization
	void Start () {
		prefabUnit = prefab.GetComponent<PawnController> ();

		goldCost.text = prefabUnit.goldCost.ToString ();
		oreCost.text = prefabUnit.oreCost.ToString ();
		oilCost.text = prefabUnit.oilCost.ToString ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
