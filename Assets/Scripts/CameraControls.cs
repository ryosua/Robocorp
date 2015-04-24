using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CameraControls : MonoBehaviour
{

	RaycastHit2D hit;
	public GameObject hitObject;
	public GameObject selected;
	public GameObject lastSelected;
	public GameObject selectorParticle;
	public GameObject CameraNWEdge;
	public GameObject CameraSEEdge;
	public GameObject player1Base;
	public GameObject player2Base;

	// dynamic UI elements
	public Text moveText;
	public Text actionText;
	public Text healthText;
	public Text attackText;
	public Text playerText;
	public Text oilText;
	public Text goldText;
	public Text oreText;
	public Text lossText;
	public Text GoalText;
	public Button endTurn;
	public Button lossButton;
	public Button buildButton;
	public GameObject actionPanel;
	public GameObject lossPanel;
	public GameObject BuyPlanetPanel;
	public GameObject TradingPanel;

	// red unit prefabs
	public GameObject SettlerBotRedPrefab;
	public GameObject HeavyBotRedPrefab;
	public GameObject MeleeBotRedPrefab;
	public GameObject WorkerBotRedPrefab;
	public GameObject RangedBotRedPrefab;
	public GameObject BaseRedPrefab;

	// blue unit prefabs
	public GameObject SettlerBotBluePrefab;
	public GameObject HeavyBotBluePrefab;
	public GameObject MeleeBotBluePrefab;
	public GameObject WorkerBotBluePrefab;
	public GameObject RangedBotBluePrefab;
	public GameObject BaseBluePrefab;

	// get level init object (for player lists, unit lists)
	public GameObject levelInit;

	// turn bool
	int turn;

	// trading values
	public int oreConversion;
	public int oilConversion;
	public int winGoldCount;

	// camera move bool
	bool moveBool;
	int cameraMin;
	int cameraMax;
	float scrollValue;

	// vector for movement target (always lerps here)
	public Vector3 moveCoordinates;

	// mouse location values
	public float mouseX;
	public float mouseY;
	public float mouseZ;

	
	// bool to stop camera movement
	public bool paused;

	// keep track of current player
	public int currentPlayer;

	// When a player buys a unit it is stored here until it is placed.
	public UnitType UnitToPlace { get; set; }
		
	// Use this for initialization
	void Start ()
	{
		UnitToPlace = UnitType.None;
		currentPlayer = 1;
		moveBool = false;
		lastSelected = null;
		paused = false;
		turn = 1;

		cameraMax = 10;
		cameraMin = 1;
		scrollValue = 0F;
	}

	// trade function for resources to gold. 1 for ore, 2 for oil
	public void TradeforGold (int function)
	{

		// get current player
		PlayerController callingPlayer = GetPlayerController ();

		// get function, see if trade is possible
		if (function == 1) {

			//trade ore
			if (callingPlayer.oreCount >= oreConversion) {
				callingPlayer.oreCount -= oreConversion;
				callingPlayer.goldCount += 1;
				UIResourceUpdate ();
			}
		} else {

			//trade oil
			if (callingPlayer.oilCount >= oilConversion) {
				callingPlayer.oilCount -= oilConversion;
				callingPlayer.goldCount += 1;
				UIResourceUpdate ();
			}
		}
	}

	// sets camera focus on end turn for the given player, the camera flies to this object on their turn start
	void SetBase (int playerID, GameObject baseObject)
	{
		if (playerID == 1) {
			player1Base = baseObject;
		} else if (playerID == 2) {
			player2Base = baseObject;
		}
	}

	// function to grab selector particle (called in init after it is created
	void SetParams ()
	{

		// setup camera params
		selectorParticle = GameObject.FindGameObjectWithTag ("Selector Particle");
		CameraNWEdge = GameObject.FindGameObjectWithTag ("Map NW Corner");
		CameraSEEdge = GameObject.FindGameObjectWithTag ("Map SE Corner");
	}

	// set camera to other player between turns
	public void TakeTurn ()
	{
		if (turn == 1) {

			// do turn bookkeeping
			levelInit.GetComponent<LevelInit> ().player2.TakeTurn ();
			turn = 0;

			// move camera to player 2's base
			MoveTo (player2Base);
			currentPlayer = 2;
			playerText.text = "Player 2";
			UIResourceUpdate ();
		} else {

			// do turn bookkeeping
			levelInit.GetComponent<LevelInit> ().player1.TakeTurn ();
			turn = 1;

			// move camera to player 1's base
			MoveTo (player1Base);
			currentPlayer = 1;
			playerText.text = "Player 1";
			UIResourceUpdate ();
		}
	}

	// function to end game (given player number loses)
	public void PlayerLoss (int playerNumber)
	{

		// enable loss panel
		lossPanel.SetActive (true);

		// check who lost, set text accordingly
		if (playerNumber == 1) {
			lossText.text = "Player 2 Wins!";
		} else {
			lossText.text = "Player 1 Wins!";
		}
	}

	// function to freely move camera to a target
	public void MoveTo (GameObject target)
	{
		moveCoordinates = new Vector3 (target.transform.position.x, target.transform.position.y, -10);
		moveBool = true; 
	}

	// function to set up UI when a pawn is clicked;
	void UIUpdatePawnInfo (GameObject target, int owned)
	{
		// set color and correct numbers for moves/actions on UI

		// check if we have selected something
		if (target != null) {

			// check if the unit even has a pawn controller
			PawnController targetUnit = target.GetComponent<PawnController> ();

			if (owned == 1) {
				// if it is movable, display moves and actions
				if (targetUnit.movable == true) {

					moveText.color = Color.black;
					moveText.text = "Moves Remaining:\t" + targetUnit.currentMoves.ToString ();
				}

				if (targetUnit.canAct == true) {
					buildButton.interactable = true;
				}

				// in all cases, display actions, health, and attack (even if 0)
				actionText.color = Color.black;
				actionText.text = "Actions Remaining:\t" + targetUnit.currentActions.ToString ();

				healthText.color = Color.black;
				healthText.text = "Unit Health:\t\t" + targetUnit.health.ToString ();

				attackText.color = Color.black;
				attackText.text = "Attack Power:\t" + targetUnit.attackDamage.ToString ();

				// activate actionPanel color
				actionPanel.GetComponent<Image> ().color = Color.red;

				// Check unit type, build button for unit

			} else {
				healthText.color = Color.black;
				healthText.text = "Unit Health:\t\t" + targetUnit.health.ToString ();

				moveText.color = Color.grey;
				moveText.text = "Moves Remaining:";
				
				actionText.color = Color.grey;
				actionText.text = "Actions Remaining:";
				
				attackText.color = Color.grey;
				attackText.text = "Attack Power:";

				actionPanel.GetComponent<Image> ().color = Color.black;
			}
		}
	}

	// function to remove UI elements when a pawn is clicked
	void UIDeselectPawn ()
	{
		// assume no pawn is selected now
		moveText.color = Color.grey;
		moveText.text = "Moves Remaining:";
		
		actionText.color = Color.grey;
		actionText.text = "Actions Remaining:";
		
		healthText.color = Color.grey;
		healthText.text = "Unit Health:";
		
		attackText.color = Color.grey;
		attackText.text = "Attack Power:";
		
		// activate actionPanel color
		actionPanel.GetComponent<Image> ().color = Color.grey;

		if (lastSelected != null) {
			if (lastSelected.GetComponent<PawnController> ().canAct != true) {
				buildButton.interactable = false;
			}
		}
	}

	// function to update resource UI
	public void UIResourceUpdate ()
	{

		// update all UI text as above on resource panel
		PlayerController player = GetPlayerController ();

		if (player.goldCount >= winGoldCount) {
			BuyPlanetPanel.SetActive (true);
			GoalText.text = "0 Gold to Win";
		} else {
			GoalText.text = (winGoldCount - player.goldCount).ToString () + " Gold to Win";
		}

		oilText.text = "Oil:\t" + player.oilCount.ToString ();
		goldText.text = "Gold:\t" + player.goldCount.ToString ();
		oreText.text = "Ore:\t" + player.oreCount.ToString ();
	}

	// mine a tile (claim territory)
	public void MineTile ()
	{

		// make sure we have *something* selected
		if (lastSelected != null) {

			PawnController worker = lastSelected.GetComponent<PawnController> ();
			GroundScript mineTile = worker.currentTile.GetComponent<GroundScript> ();

			// check if this is a mineable tile
			if (mineTile.isResource == true) {

				// check if worker has actions left
				if (worker.currentActions > 0) {

					// subtract action, change sprite
					worker.currentActions -= 1;
					mineTile.ClaimTerritory (currentPlayer);
					mineTile.isClaimed = currentPlayer;

					// add territory to total for player
					GetPlayerController ().AddTerritory (mineTile);
				}
			}
		}
	}

	// function to choose the unit to spawn based off of unittype
	public void SpawnSelection (UnitType unit)
	{

		UnitToPlace = unit;
		GameObject chosenPrefab = null;
		bool resetBase = false;
		PlayerController player = GetPlayerController ();

		// check which player (determines color prefab set)
		if (currentPlayer == 1) {

			switch (UnitToPlace) {
			
			case UnitType.Base: 
				chosenPrefab = BaseRedPrefab;
				resetBase = true;
				break;
			case UnitType.SettlerBot: 
				chosenPrefab = SettlerBotRedPrefab;
				break;
			case UnitType.WorkerBot:
				chosenPrefab = WorkerBotRedPrefab;
				break;
			case UnitType.MeleeBot:
				chosenPrefab = MeleeBotRedPrefab;
				break;
			case UnitType.HeavyBot:
				chosenPrefab = HeavyBotRedPrefab;
				break;
			}
		}
		// player 2 (blue)
		else {

			switch (UnitToPlace) {
				
			case UnitType.Base:
				chosenPrefab = BaseBluePrefab;
				resetBase = true;
				break;
			case UnitType.SettlerBot: 
				chosenPrefab = SettlerBotBluePrefab;
				break;
			case UnitType.WorkerBot:
				chosenPrefab = WorkerBotBluePrefab;
				break;
			case UnitType.MeleeBot:
				chosenPrefab = MeleeBotBluePrefab;
				break;
			case UnitType.HeavyBot:
				chosenPrefab = HeavyBotBluePrefab;
				break;
			default:
				throw new UnityException ("Built a unit with an unsupported UnitType");
			//break;
			}
		}

		if ((player.BuyUnit (chosenPrefab)) == 0) {
			if ((chosenPrefab = (GameObject)(SpawnPawn (chosenPrefab, lastSelected.GetComponent<PawnController> ().currentTile, currentPlayer, UnitToPlace))) == null) {
				// show UI panel saying no valid spawn location around building unit
			} else {
				if (resetBase) {
					if (currentPlayer == 1) {
						player1Base = chosenPrefab;
					} else {
						player2Base = chosenPrefab;
					}
				}
			}
		} else {
			// show UI panel saying the player can't afford it
			GetComponent<NotificationController> ().ShowNotification ("You can not afford this robot.");
		}
		
		// The unit has been placed.
		UnitToPlace = UnitType.None;
		UIResourceUpdate ();
	}

	// spawn an arbitrary pawn at spawnTileLocation. Spawns beside tile location (or not at all)
	public GameObject SpawnPawn (GameObject spawnPrefab, GameObject spawnTileLocation, int owningPlayer, UnitType unitType)
	{

		// check if this tile exists
		if (spawnTileLocation != null) {

			GroundScript spawnTile = spawnTileLocation.GetComponent<GroundScript> ();

			// check if this tile location is occupied (maybe a building)
			if (spawnTile.occupied != true) {
				return Spawn (spawnPrefab, spawnTileLocation, owningPlayer, unitType);
			}

			// if the left block of the spawn tile isn't null, spawn there
			else if (spawnTile.left_block != null) {

				// if it is not occupied, spawn
				if (spawnTile.left_block.GetComponent<GroundScript> ().occupied != true) {
					return Spawn (spawnPrefab, spawnTile.left_block, owningPlayer, unitType);
				}
			}
			// if the upper block of the spawn tile isn't null, spawn there
			else if (spawnTile.up_block != null) {
				
				// if it is not occupied, spawn
				if (spawnTile.up_block.GetComponent<GroundScript> ().occupied != true) {
					return Spawn (spawnPrefab, spawnTile.up_block, owningPlayer, unitType);
				}
			}
			// if the right block of the spawn tile isn't null, spawn there
			else if (spawnTile.right_block != null) {
				
				// if it is not occupied, spawn
				if (spawnTile.right_block.GetComponent<GroundScript> ().occupied != true) {
					return Spawn (spawnPrefab, spawnTile.right_block, owningPlayer, unitType);
				}
			}
			// if the lower block of the spawn tile isn't null, spawn there
			else if (spawnTile.down_block != null) {
				
				// if it is not occupied, spawn
				if (spawnTile.down_block.GetComponent<GroundScript> ().occupied != true) {
					return Spawn (spawnPrefab, spawnTile.right_block, owningPlayer, unitType);
				}
			}
		}
		// no valid spot to spawn
		return null;
	}

	// private spawn method to handle bookkeeping
	GameObject Spawn (GameObject spawnPrefab, GameObject spawnTileLocation, int owningPlayer, UnitType unitType)
	{

		// instantiate the pawn, set it's tile, set it's owner, set the tile's occupancy
		spawnPrefab = (GameObject)Instantiate (spawnPrefab, spawnTileLocation.transform.position, spawnTileLocation.transform.rotation);
		spawnPrefab.BroadcastMessage ("SetTile", spawnTileLocation);
		spawnPrefab.BroadcastMessage ("SetOwner", owningPlayer);
		spawnPrefab.BroadcastMessage ("SetCamera", transform.gameObject);
		spawnPrefab.GetComponent<PawnController> ().currentTile.GetComponent<GroundScript> ().SetOccupant (spawnPrefab);
		
		// add settler to the owning player's unit list, set unit's unitID
		if (owningPlayer == 1) {
			spawnPrefab.GetComponent<PawnController> ().unitID = levelInit.GetComponent<LevelInit> ().player1.AddUnit (spawnPrefab);
		} else {
			spawnPrefab.GetComponent<PawnController> ().unitID = levelInit.GetComponent<LevelInit> ().player2.AddUnit (spawnPrefab);
		}

		spawnPrefab.GetComponent<PawnController> ().SetUnitType (unitType);

		return spawnPrefab;
	}
	
	// Update is called once per frame; here we handle selection, deselection, movement
	void Update ()
	{

		mouseX = Input.mousePosition.x;
		mouseY = Input.mousePosition.y;
		mouseZ = Input.mousePosition.z;

		// if we left click in the camera, select the pawn (if selectable)
		if (Input.GetMouseButtonDown (0)) {

			// cast a ray to see what was clicked
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			
			if (hit.collider != null) {
				if ((hit.transform.gameObject.tag == "Selectable") || (hit.transform.gameObject.tag == "Selectable and Movable")) {

					// make sure the current player owns this pawn
					if (hit.transform.gameObject.GetComponent<PawnController> ().owningPlayer == currentPlayer) {

						// update selection
						selected = hit.transform.gameObject;
						selectorParticle.GetComponent<ParticleBase> ().selectedPawn = selected;

						// move selection particle
						selectorParticle.BroadcastMessage ("FlashTo", selected.transform.gameObject);
						selectorParticle.BroadcastMessage ("StartParticle");

						// update UI (flush it really quick in case we are going from one pawn to another
						UIDeselectPawn ();
						UIUpdatePawnInfo (selected, 1);
					}

					// if it is an enemy player, show health
					else {
						UIUpdatePawnInfo (hit.transform.gameObject, 0);
					}
				}

				// deselection code
				else {
					// Save the last pawn selected so when the player clicks the build button we know which panels to show.
					if (selected != null) {
						lastSelected = selected;
					}

					// deselect pawn, move selection particle off screen
					selected = null;
					selectorParticle.BroadcastMessage ("FlashTo", CameraSEEdge.transform.gameObject);

					// update UI
					UIDeselectPawn ();
				}
			}
		}

		// if we right click in the camera, move the selected pawn to that grid space (or try to)
		else if (Input.GetMouseButtonDown (1)) {

			// cast a ray to see what was clicked
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

			// check if we hit a valid panel
			if (hit.collider != null) {
				// check if there is actually something selected
				if (selected != null) {
					// check if we are moving to an invalid block
					if ((hit.transform.gameObject.tag != ("MovementBlocker"))) {

						// If there is no pawn to place
						if (UnitToPlace == UnitType.None) {
							// move the pawn using that pawn's code
							// determine direction with distance from selection
							float xDist = hit.transform.position.x - selected.transform.position.x;
							float yDist = hit.transform.position.y - selected.transform.position.y;
							
							if (Mathf.Abs (xDist) >= Mathf.Abs (yDist)) {
								
								if (xDist > 0) {
									if (selected.GetComponent<PawnController> ().MoveTo (4) == 0) {
										moveCoordinates = new Vector3 (selected.GetComponent<PawnController> ().moveCoordinates.x, selected.GetComponent<PawnController> ().moveCoordinates.y, -10);
										moveBool = true;
									}
								} else {
									if (selected.GetComponent<PawnController> ().MoveTo (3) == 0) {
										moveCoordinates = new Vector3 (selected.GetComponent<PawnController> ().moveCoordinates.x, selected.GetComponent<PawnController> ().moveCoordinates.y, -10);
										moveBool = true;
									}
								}
							} else {
								
								if (yDist > 0) {
									if (selected.GetComponent<PawnController> ().MoveTo (1) == 0) {
										moveCoordinates = new Vector3 (selected.GetComponent<PawnController> ().moveCoordinates.x, selected.GetComponent<PawnController> ().moveCoordinates.y, -10);
										moveBool = true;
									}
								} else {
									if (selected.GetComponent<PawnController> ().MoveTo (2) == 0) {
										moveCoordinates = new Vector3 (selected.GetComponent<PawnController> ().moveCoordinates.x, selected.GetComponent<PawnController> ().moveCoordinates.y, -10);
										moveBool = true;
									}
								}
							}
						}
						// update UI
						UIUpdatePawnInfo (selected, 1);
					}
				}
			}

			hitObject = hit.transform.gameObject;
		}

		// if we hit up key
		else if (Input.GetKeyDown ("up")) {

			// if we hit keys while something is selected...
			if (selected != null) {

				if (selected.GetComponent<PawnController> ().MoveTo (1) == 0) {
					moveCoordinates = new Vector3 (selected.GetComponent<PawnController> ().moveCoordinates.x, selected.GetComponent<PawnController> ().moveCoordinates.y, -10);
					moveBool = true;
				}

				// update UI
				UIUpdatePawnInfo (selected, 1);
			}
		}

		// if we hit down key
		else if (Input.GetKeyDown ("down")) {

			// if we hit keys while something is selected...
			if (selected != null) {
				
				if (selected.GetComponent<PawnController> ().MoveTo (2) == 0) {
					moveCoordinates = new Vector3 (selected.GetComponent<PawnController> ().moveCoordinates.x, selected.GetComponent<PawnController> ().moveCoordinates.y, -10);
					moveBool = true;
				}

				// update UI
				UIUpdatePawnInfo (selected, 1);
			}
		}

		// if we hit left key
		else if (Input.GetKeyDown ("left")) {

			// if we hit keys while something is selected...
			if (selected != null) {
				
				if (selected.GetComponent<PawnController> ().MoveTo (3) == 0) {
					moveCoordinates = new Vector3 (selected.GetComponent<PawnController> ().moveCoordinates.x, selected.GetComponent<PawnController> ().moveCoordinates.y, -10);
					moveBool = true;
				}

				// update UI
				UIUpdatePawnInfo (selected, 1);
			}
		}

		// if we hit right key
		else if (Input.GetKeyDown ("right")) {

			// if we hit keys while something is selected...
			if (selected != null) {
				
				if (selected.GetComponent<PawnController> ().MoveTo (4) == 0) {
					moveCoordinates = new Vector3 (selected.GetComponent<PawnController> ().moveCoordinates.x, selected.GetComponent<PawnController> ().moveCoordinates.y, -10);
					moveBool = true;
				}

				// update UI
				UIUpdatePawnInfo (selected, 1);
			}
		}

		// camera movement
		if ((mouseX < 7) && (transform.position.x > CameraNWEdge.transform.position.x + 12) && (!paused)) {

			// move camera left
			transform.position = new Vector3 (transform.position.x - 0.1f, transform.position.y, transform.position.z);

		} else if ((mouseX > Screen.width - 7) && (transform.position.x < CameraSEEdge.transform.position.x - 13) && (!paused)) {

			// move camera right
			transform.position = new Vector3 (transform.position.x + 0.1f, transform.position.y, transform.position.z);

		}

		if ((mouseY < 5) && (transform.position.y > CameraSEEdge.transform.position.y + 6) && (!paused)) {
			
			// move camera down
			transform.position = new Vector3 (transform.position.x, transform.position.y - 0.1f, transform.position.z);

		} else if ((mouseY > Screen.height - 7) && (transform.position.y < CameraNWEdge.transform.position.y - 7) && (!paused)) {
			
			// move camera up
			transform.position = new Vector3 (transform.position.x, transform.position.y + 0.1f, transform.position.z);
			
		}

		// code to move camera to a location (at turn end)
		if (moveBool == true) {
			// check if we moved
			Vector3 oldPosition = transform.position;
			transform.position = Vector3.Lerp (transform.position, moveCoordinates, 10 * Time.deltaTime);

			if (oldPosition == transform.position) {
				moveBool = false;
			}
		}

		// depth scrolling
		if ((scrollValue = Input.GetAxis ("Mouse ScrollWheel")) != 0) { // forward
			if (scrollValue > 0) {
				Camera.main.orthographicSize--;
			} else {
				Camera.main.orthographicSize++;
			}

			Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize, cameraMin, cameraMax);
		}
		
		// handle space button
		if (Input.GetKeyDown ("space")) {
			TakeTurn ();
		}

		UIResourceUpdate ();
	}

	public PlayerController GetPlayerController ()
	{
		PlayerController player;
		
		if (currentPlayer == 1) {
			player = levelInit.GetComponent<LevelInit> ().player1;
		} else {
			player = levelInit.GetComponent<LevelInit> ().player2;
		}

		return player;
	}
}
