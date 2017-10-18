using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class Unit : MonoBehaviour
{

	public int x, y, range = 1;

	public float movement = 2f;

	protected float currentMovement = 2f;

	protected List<Tile> traversableTiles;
	protected List<Tile> enemyTiles;

	public int attack = 2, defense = 1, hp = 10;

	protected bool isSelected = false;

	public bool hasAttacked = false, hasMoved = false, doneMoving = false;

	public Vector3 gridPosition
	{
		get
		{
			return new Vector3(x, y, transform.position.z);
		}
	}

	void Start ()
	{
		transform.position = GridManager.gridMan.tiles[x, y].position;
		currentMovement = movement;
		traversableTiles = new List<Tile>();
		enemyTiles = new List<Tile>();
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			isSelected = false; // if user right-clicks, deselect this unit.
			UnitManager.unitManager.selectedUnit = null;
		}
	}

	protected void HighlightEnemies()
=======
public class Unit : MonoBehaviour 
{
	//Unit Stat Variables
	[SerializeField] 
	protected int hp, attack, defense, range, movement, productionCost;
	
	//Check variables for actions done on each units
	protected bool isSelected; 
	protected bool hasAttacked;
	protected bool hasMoved;

	//Map coordinates variables
	private int xPos;
	private int yPos;

	//Misc. class variables
	Vector3 worldPosition;
	Building checkBuilding;
	Player unitOwner;
	UnitManager unitMananger;
	
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
>>>>>>> origin/JosephUnit
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

<<<<<<< HEAD
	protected void FindTraversableTiles()
=======
	public int getattack()
>>>>>>> origin/JosephUnit
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

				if (x + i > GridManager.gridMan.tiles.GetLength(0) - 1 || x + i < 0 || y + j > GridManager.gridMan.tiles.GetLength(1) - 1 || y + j < 0)
				{
				//	Debug.Log("Out of Bounds: " + (x + i) + ", " + (y + j));
					continue;
				}

				//Debug.Log((x + i) + ", " + (y + j));

				Tile currentTile = GridManager.gridMan.tiles[x + i, y + j]; // Set our current tile. We don't start with our unit's pos.

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
				tempMovement -= Vector2.Distance(new Vector2(x, y), new Vector2(currentTile.x, currentTile.y)) + currentTile.movementModifier;
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

<<<<<<< HEAD
	protected void OnMouseDown()
	{
		///<summary>
		/// This function is called whenever a unit is clicked on with the mouse. We need this function to have several if statements
		/// 
		/// 1. If Statement 1 will set our selected unit and find traversible tiles for movement.
		///		- If this unit is not selected AND has not attacked AND the Unit Manager's selectedUnit is null, THEN set this unit toselected, set the Unit Manager's selectedUnit to this, and FindTraversibleTiles()
		///	
		/// 2. else if statement 1 will commence the AttackRoutine() function.
		///		- If this unit is not selected AND Unit MAnager's selectedUnit is NOT this, AND Unit Manager's selectedUnit is NOT null, THEN call AttackRoutine()
		///	
		/// 3. else if statement 2 will cancel unit selection if we click on the selected unit again.
		///		- If this unit is selected, THEN set this unit to not selected, set Unit Manager's selectedUnit to null
		/// </summary>
		if (!isSelected && !hasAttacked && UnitManager.unitManager.selectedUnit == null)
		{
			// if no unit is selected at all, select this unit for movement/attacking.
			isSelected = true;
			UnitManager.unitManager.selectedUnit = this;
			FindTraversableTiles();
			//Move();
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

	public void StartMove(Tile targetTile)
=======
	public int getdefense()
	{
		return this.defense;
	}

	public int getrange()
>>>>>>> origin/JosephUnit
	{
		if (doneMoving) return;

		StartCoroutine(Move(targetTile));
	}

<<<<<<< HEAD
	protected IEnumerator Move(Tile targetTile)
=======
	public int getmovement()
>>>>>>> origin/JosephUnit
	{
		hasMoved = false;

		Tile currentTile = GridManager.gridMan.tiles[x, y];
		
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
		if(enemyTiles.Count > 0)
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

		this.x = targetTile.x;
		this.y = targetTile.y;
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
<<<<<<< HEAD

	/*public void BuildUnit(string newName)
	{
		Debug.Log(newName);
		if (newName == "infantry")
		{
			if (PlayerManager.currentPlayer.money > infantryCost)
				BuildInfantry();
			else
				return;
		}
		else if (newName == "tank") Debug.Log("Hi Tank");
	}*/

	public void IncreaseStats(int attackMod, int defenseMod)
	{
		///<summary>
		/// This function will be called on by the Tile that the unit enters
		/// Tile will pass it's attack and defense mods to this function and the unit will update their stats accordingly.
		/// </summary>
		
		attack += attackMod;
		defense += defenseMod;
	}

	public void DecreaseStats(int attackMod, int defenseMod)
	{
		///<summary>
		/// This function will be called on by the Tile that the unit leaves
		/// Tile will pass it's attack and defense mods to this function and the unit will update their stats accordingly.
		/// </summary>
		
		attack -= attackMod;
		defense -= defenseMod;
	}

	protected void HasAttacked()
	{
		///<summary>
		/// This function will do three things:
		/// 1. Set isSelected to false
		/// 2. set hasAttacked to true
		/// 3. set the Unit Manager's selectedUnit to null
		/// 
		/// This function will be called at the end of the attack routine. It will be called on the attacking unit.
		/// E.G:    attackingUnit.HasAttacked();
		/// </summary>
		isSelected = false;
		hasAttacked = true;
		UnitManager.unitManager.selectedUnit = null;
	}

	protected void AttackRoutine(Unit attackingUnit)
	{
		// check if attacker is in range of this unit
		if (!attackingUnit.CheckAttackRange(this))
			return; // exit the function if not in range

		// If we make it here, then we're in range. Execute unit attack against this unit
		attackingUnit.Attack(this); // attack unit and make it take damage

		// now the unit gets to return fire if they are in range
		if (!this.CheckAttackRange(attackingUnit))
			return;

		// if we make it here, then defender is in range. Execute a counter attack against attacking unit
		this.Attack(attackingUnit);

		// if we make it all the way here, then we can set the selectedUnit's turn to be over
		attackingUnit.HasAttacked();
	}

	protected bool CheckAttackRange(Unit defendingUnit)
	{
		/// <summary>
		/// Check if enemy unit is in range to attack. Returns true if distance is within range. False otherwise.
		/// Terrain modifiers for movement are not considered. This is a standard distance calculation with no weights.
		/// </summary>
		
		if (Vector2.Distance(gridPosition, defendingUnit.gridPosition) > this.range)
			return false;
		else
			return true;
	}

	protected void Attack(Unit defendingUnit)
	{
		/// <summary>
		/// call TakeDamage on defendingUnit. Pass the attacking unit's attack value as a parameter
		/// We don't currently need this function. We could just call TakeDamage directly in the AttackRoutine function
		/// However, if we want to create some kind of Attack state, which can dictate animations, we will want a separate function like this
		/// </summary> 
		
		defendingUnit.TakeDamage(this.attack, this.hp);
=======

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

	//Special Attributes 
	private void die() //Needs editing due to player list rather than dictionary
	{
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
		checkBuilding.captureCheck();
		hasMoved = false;
		hasAttacked = false;
	}
	
	public void startMove(Unit thisUnit) //Empty for now, needed for compiling purposes.
	{
		//Code coming soon!
	}

	public bool getHasMoved() //Returns boolean value of 'hasMoved' when called.
	{
		return hasMoved;
	}

	public Player getUnitOwner(Unit thisUnit) //Returns this unit's player owner.
	{
		return unitOwner;
	}

	public Vector2 gridPosition() //Returns the x and y coordinates.
	{	
		
			return newVector2(x,y);
	}

	void HasAttacked(bool isSelected, bool hasAttacked) //Sets of the conditions for after a unit attacks
	{
		isSelected = false;
		hasAttacked = true;
		
		unitMananger.selectedUnit = null;
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

	public void takeDamage(int attackerDamage, int attackerHp) //Damage calculation is done in this function and checks if the defending unit is destroyed.
	{
		int totalDamageDone = ((attackerDamage)*(attackerHP/10))/(this.defense); //Algorithm for calculating total damage.

		if(totalDamageDone < 0.5) //If damage is under 0.5, reduced to zero.
			totalDamageDone = 0;
		else if(totalDamageDone >= 0.5 && totalDamageDone < 1) //If damage is over 0.5 but under 1, sets damage to 1.
			totalDamageDone = 1;
		else //Else proceed as normal with no additional modifiers
			this.hp = this.hp - totalDamageDone;

		if (hp <= 0)
		{
			die();
		}
	}

	private void OnMouseDown() //Function is called when the mouse is clicked, and will run checks to execute the proper action.
	{
		if (!isSelected && !hasAttacked && UnitManager.unitManager.selectedUnit == null)
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

	protected void FindTraversableTiles()
	{
		///<summary>
		/// Primitive pathfinding implementation.
		/// Code will look left, right, up, down, diagonal for tiles it can move to.
		/// </summary>
	
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

				if (x + i > GridManager.gridMan.tiles.GetLength(0) - 1 || x + i < 0 || y + j > GridManager.gridMan.tiles.GetLength(1) - 1 || y + j < 0)
					continue;

				Tile currentTile = GridManager.gridMan.tiles[x + i, y + j]; // Set our current tile. We don't start with our unit's pos.

				float totalCost = 0; // initialize our totalCost variable. This will keep a running tally of the movement cost in a direction.

				while (totalCost <= movement)
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
				}
			}
		}
>>>>>>> origin/JosephUnit
	}

	protected void TakeDamage(int attackDamage, int attackerHP)
	{
		///<summary>
		/// Perform damage calculations against the unit that calls this function.
		/// 
		/// This function should implement our equations we came up with
		/// 
		/// NOTE: attackDamage variable includes terrain modifiers since Tile class is handling updating those stats. Yo udo not have to worry about handling those modifiers in this function
		/// 
		/// ANOTHER NOTE: Casting gets tricky in this function. We only want to remove the decimal place when we go to subtract totalDamageDone from hp. the totalDamageDone variable needs to have decimal precision. However, keep in mind that all our variables we're working with are integers. 
		/// 
		/// My hint would be that you can create float values in C# by putting an 'f' at the end of a number. 1f, 3f, 2.5f, etc. Remember, if you have integers performing any math with a floating number, the result will be cast to a float.
		/// 
		/// Additionally, you can cast values in C# by putting (type) in front of the variable you want to cast. For instance
		/// 
		/// float x = (float) 3; // this will turn x into 3.0
		/// 
		/// 1.	totalDamageDone = equation we came up with.
		/// 2.	If statement 1: if totalDamageDone less than 0.5, THEN totalDamageDone = 0
		/// 3.	if statement 2: if totalDamageDone greater or equal to 0.5 AND above result less than 1, THEN totalDamageDone = 1
		/// 4.	subtract totalDamageDone from defending unit's hp
		/// 5.	For testing, I recommend putting a Debug.Log(transform.name + " Took " + finalDamage + " Damage") statement at end of function.
		/// 
		/// </summary>

		float totalDamage = attackDamage * ((attackerHP / 10f) * (attackDamage / this.defense));

		if (totalDamage < 0.5f)
			totalDamage = 0;
		else if (totalDamage >= 0.5f && totalDamage < 1)
			totalDamage = 1f;

		hp -= (int)totalDamage;
		Debug.Log(transform.name + " Took " + (int)totalDamage + " Damage");
	}


}
