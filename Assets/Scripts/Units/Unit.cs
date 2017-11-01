using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour 
{
	//Unit Stat Variables
	[SerializeField] 
	protected int hp, attack, defense, range, movement, productionCost;
	
	//Check variables for actions done on each units
	protected bool isSelected = false; 
	protected bool hasAttacked = false;
	protected bool hasMoved = false;
	protected bool doneMoving = false;

	//Map coordinates variables
	private int xPos;
	private int yPos;

	//Misc. class variables
	Vector3 worldPosition;
	Building checkBuilding;
	Player unitOwner;
	Color unitColor;

	//Movement variables
	protected List<Tile> traversableTiles;
	protected List<Tile> enemyTiles;
	protected float currentMovement;

	private void Awake()
	{
		int finalX = (int)Mathf.Abs((GetComponent<RectTransform>().anchoredPosition.x / GameObject.FindGameObjectWithTag("Grid").GetComponent<GridLayoutGroup>().cellSize.x));
		int finalY = (int)Mathf.Abs((GetComponent<RectTransform>().anchoredPosition.y / GameObject.FindGameObjectWithTag("Grid").GetComponent<GridLayoutGroup>().cellSize.y));
		this.xPos = finalX;
		this.yPos = finalY;
	}

	private void Start()
	{
		//transform.position = GridManager.gridMan.tiles[xPos, yPos].position; //x,y changed to xPos, yPos
		currentMovement = movement;
		traversableTiles = new List<Tile>();
		enemyTiles = new List<Tile>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			isSelected = false; // if user right-clicks, deselect this unit.
			UnitManager.unitManager.selectedUnit = null;
		}
	}

	//Map Attributes and Coordinates
	public int getxPos()
	{
		return this.xPos;
	}
	
	public int getyPos()
	{
		return this.yPos;
	}

	//Unit Attributes for receiving variable values
	public int gethp()
	{
		return this.hp;
	}

	public int getattack()
	{
		return this.attack;
	}

	public int getdefense()
	{
		return this.defense;
	}

	public int getrange()
	{
		return this.range;
	}

	public int getmovement()
	{
		return this.movement;
	}

	public int getproductionCost()
	{ 
		return this.productionCost;
	}		
 
	public bool getisSelected()
	{ 
		return this.isSelected;
	}

	public bool gethasAttacked()
	{
		return this.hasAttacked;
	}

	public void SetOwner(Player newOwner)
	{
		unitOwner = newOwner;
		GetComponent<Image>().color = newOwner.getColor();
	}

	//Special Attributes 
	private void die() //Needs editing due to player list rather than dictionary
	{
		// Need to remove object from list first.
        // Created a removeUnit(unit removedUnit) function
        Destroy(gameObject);
		//get player owner from list.
	}

	public void increaseStats(int attackMod, int defenseMod) //Provides a boost to this unit's atk and def stats via mods.
	{
		attack += attackMod;
		defense += defenseMod;
	}

	public void resetStats(int attackMod, int defenseMod) //Removes the boost from the this unit's stats after its use.
	{
		attack -= attackMod;
		defense -= defenseMod;	
	}

	public void reset() //Checks if a building is captured by the unit and assigns false to hasMove and hasAttack.
	{
		if(checkBuilding != null)
			checkBuilding.CaptureCheck();
		hasMoved = false;
		hasAttacked = false;
	}

	public void setBuilding(Building newBuilding)
	{
		checkBuilding = newBuilding;
	}

	protected void HighlightEnemies()
	{
		if (hasAttacked) return;

		/*foreach(Unit enemyUnit in PlayerManager.playerManager.GetInactivePlayer().units)
		{
			if(range >= Vector2.Distance(new Vector2(x, y), new Vector2(enemyUnit.x, enemyUnit.y)))
			{
				GridManager.gridMan.tiles[enemyUnit.x, enemyUnit.y].SetEnemy();
				enemyTiles.Add(enemyUnit);
			}
		}*/
	}

	protected void FindTraversableTiles()
	{
		///<summary>
		/// Primitive pathfinding implementation.
		/// Code will look left, right, up, down, diagonal for tiles it can move to.
		/// </summary>

		if (doneMoving) return;

		for (int i = -1; i < 2; i++)
		{
			for (int j = -1; j < 2; j++)
			{
				// we use nested for loops bounded from -1 to 1, which allows us to look at all adjacent tiles to our current position.
				// To do this, we call x + i, x + j. For instance, [4 + -1, 4 + -1] will check the tile [3, 3]. The loop is nested because
				// the next iteration will do [4 + -1, 4 + 0], which checks tile [3, 4].

				if (i == 0 && j == 0)
					continue; // this continues loop if our [x + i, y + j] is equal to our current position. We don't want to check our pos.


				// The below if statement performs bounds checking to make sure our next tile is within the bounds of our 2D tile array.
				// tiles.GetLength(0) grabs the 0th dimension of the tiles array and returns it's length.
				// For instance, if tiles is size [3][4]. tiles.GetLength(0) will return 3. GetLength(1) will return 4

				if (xPos + i > GridManager.gridMan.tiles.GetLength(0) - 1 || xPos + i < 0 || yPos + j > GridManager.gridMan.tiles.GetLength(1) - 1 || yPos + j < 0)
				{
					//	Debug.Log("Out of Bounds: " + (x + i) + ", " + (y + j));
					continue;
				}

				//Debug.Log((x + i) + ", " + (y + j));

				Tile currentTile = GridManager.gridMan.tiles[xPos + i, yPos + j]; // Set our current tile. We don't start with our unit's pos.

				/*if (currentTile.unit != null)
				{
					// unit on tile, add it to enemy tile
					enemyTiles.Add(currentTile);
					currentTile.SetEnemy();
					continue;
				}*/

				float totalCost = 0; // initialize our totalCost variable. This will keep a running tally of the movement cost in a direction.
				float tempMovement = currentMovement; // initialize tempMovement to our max movement
													  //Debug.Log("Temp Movement: " + tempMovement);

				//Debug.Log("Distance to Tile " + currentTile.x + ", " + currentTile.y + ": " + (Vector2.Distance(new Vector2(x, y), new Vector2(currentTile.x, currentTile.y)) + currentTile.movementModifier));
				tempMovement -= Vector2.Distance(new Vector2(xPos, yPos), new Vector2(currentTile.x, currentTile.y)) + currentTile.movementModifier;
				//tempMovement -= (Vector2.Distance(currentTile.gridPosition, GridManager.gridMan.tiles[currentTile.x - i, currentTile.y - j].gridPosition) / GridManager.gridMan.grid.cellSize.x) + currentTile.movementModifier;

				//Debug.Log("Post Temp Movement: " + tempMovement);

				if (tempMovement >= 0 && currentTile.unit == null)
				{
					traversableTiles.Add(currentTile);
					currentTile.SetTraversable();
				}
				else if (tempMovement < 0)
					continue;

				/*while (totalCost <= movement)
				{
					// This loop will run as long as total cost does not exceed our unit's movement.

					// the below equation adds the distance from our currentTile to our previous tile and adds the movementModifier of the current tile. The weird division black magic is necessary due to the way the GridLayoutManager is implemented. In most circumstances, you would just run Vector2.Distance between one position and another position.
					totalCost += (Vector2.Distance(currentTile.gridPosition, GridManager.gridMan.tiles[currentTile.x - i, currentTile.y - j].gridPosition) / GridManager.gridMan.grid.cellSize.x) + currentTile.movementModifier;

					if (totalCost <= movement && currentTile.unit == null)
						// If our cost has not exceeded movement and no unit is on the current tile, then we can move to this tile.
						currentTile.SetTraversable();
					else if (totalCost > movement)
						// If our totalCost has exceeded our movement, then we can't access the currentTile. Break loop and start next search
						break;

					// bounds checking again
					if (currentTile.x + i > GridManager.gridMan.tiles.GetLength(0) - 1 || currentTile.x + i < 0 || currentTile.y + j > GridManager.gridMan.tiles.GetLength(1) - 1 || currentTile.y + j < 0)
						break;

					currentTile = GridManager.gridMan.tiles[currentTile.x + i, currentTile.y + j]; // assign next tile
				}*/
			}
		}
	}

	public void StartMove(Tile targetTile)
	{
		if (doneMoving) return;

		StartCoroutine(Move(targetTile));
	}

	protected IEnumerator Move(Tile targetTile)
	{
		hasMoved = false;

		Tile currentTile = GridManager.gridMan.tiles[xPos, yPos]; //Changed x,y to xPos,yPos

		//float currentMovement = movement;
		currentMovement -= (Vector3.Distance(currentTile.gridPosition, targetTile.gridPosition) / GridManager.gridMan.grid.cellSize.x) + targetTile.movementModifier;

		Vector3 newVect = targetTile.position - transform.position;
		//transform.position += newVect * .2f;
		transform.position += newVect;
		if (traversableTiles.Count > 0)
		{
			foreach (Tile tile in traversableTiles)
				tile.StopFlash();

			traversableTiles.Clear();
		}
		if (enemyTiles.Count > 0)
		{
			foreach (Tile tile in enemyTiles)
				tile.StopFlash();

			enemyTiles.Clear();
		}
		/*while (transform.position != targetTile.position)
		{
			// move unit to target tile.
			Vector3 newVect = targetTile.position - transform.position;
			//transform.position += newVect * .2f;
			transform.position = newVect;
			yield return null;
		}*/

		this.xPos = targetTile.x;
		this.yPos = targetTile.y;
		//hasMoved = true;
		yield return null;

		if (currentMovement <= 0)
			doneMoving = true;
		else if (currentMovement > 0)
		{
			//	hasMoved = false;
			FindTraversableTiles();
		}

		yield return null;

		if (traversableTiles.Count == 0)
			// if we cannot find any more traversable tiles, then we can't move anymore.
			doneMoving = true;
	}

	public bool getHasMoved() //Returns boolean value of 'hasMoved' when called.
	{
		return hasMoved;
	}

	public Player getUnitOwner() //Returns this unit's player owner.
	{
		return unitOwner;
	}

	public Vector3 gridPosition() //Returns the x and y coordinates.
	{	
		return new Vector3(xPos, yPos, transform.position.z);
	}

	void HasAttacked() //Sets of the conditions for after a unit attacks
	{
		isSelected = false;
		hasAttacked = true;
		//Debug.Log("Hi");
		UnitManager.unitManager.selectedUnit = null;
	}

	void AttackRoutine(Unit attackingUnit) //Performs various attack actions based on the current conditions
	{
		if(!attackingUnit.CheckAttackRange(this))
			return;

		attackingUnit.Attack(this);

		if(!this.CheckAttackRange(attackingUnit))
			return;

		this.Attack(attackingUnit);

		attackingUnit.HasAttacked();
	}

	bool CheckAttackRange(Unit defendingUnit) //This function checks the ranges of the battling units to determine if an attack can occur.
	{
		float defendingRange = Vector2.Distance(this.gridPosition(), defendingUnit.gridPosition());

		if(this.range >= defendingRange) //If this unit's range is greater or equal to defender's range, battle can occur, otherwise it cannot.
			return true;
		else
			return false;
	}

	void Attack(Unit defendingUnit) //Attack function to inflict damage on the defending unit 
	{
		defendingUnit.TakeDamage(this.attack, this.hp);
	}

	public void TakeDamage(int attackerDamage, int attackerHP) //Damage calculation is done in this function and checks if the defending unit is destroyed.
	{
		int totalDamageDone = (int) ((attackerDamage)*(attackerHP/10f)) / (this.defense); //Algorithm for calculating total damage.
		
		if(totalDamageDone < 0.5) //If damage is under 0.5, reduced to zero.
			totalDamageDone = 0;
		else if(totalDamageDone >= 0.5 && totalDamageDone < 1) //If damage is over 0.5 but under 1, sets damage to 1.
			totalDamageDone = 1;
		else //Else proceed as normal with no additional modifiers
			this.hp = this.hp - totalDamageDone;
		Debug.Log(gameObject.name + " took " + totalDamageDone + " damage.");

		if (hp <= 0)
		{
			die();
		}
	}

	private void OnMouseDown() //Function is called when the mouse is clicked, and will run checks to execute the proper action.
	{
		if (!isSelected && !hasAttacked && UnitManager.unitManager.selectedUnit == null && !this.unitOwner.isLocked)
		{
			// if no unit is selected at all, select this unit for movement/attacking.
			isSelected = true;
			UnitManager.unitManager.selectedUnit = this;
			FindTraversableTiles();
		}
		else if(!isSelected && UnitManager.unitManager.selectedUnit != this && UnitManager.unitManager.selectedUnit != null /* && UnitManager.selectedUnit.player != this.player*/)
		{
			// This means a different unit is selected and they are attempting to attack this unit
			AttackRoutine(UnitManager.unitManager.selectedUnit);
		}
		else if (isSelected)
		{
			// if this unit is selected and we click on it again, cancel selection
			isSelected = false;
			UnitManager.unitManager.selectedUnit = null;
		}
	}
	

	public void SetX(int x_coord)
	{
		xPos = x_coord;
	}

	public void SetY(int y_coord)
	{
		yPos = y_coord;
	}
}