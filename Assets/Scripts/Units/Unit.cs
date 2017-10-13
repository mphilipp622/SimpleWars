using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	UnitManager thisUnitMananger;
	
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
		
		thisUnitMananger.selectedUnit = null;
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
		private static bool hasSelected;

		//More actions will be implemented as further testing goes on.
		if(!this.Unit && hasAttacked == false && thisUnitManager.selectedUnit == null)
		{
			isSelected = true;
			thisUnitManager.selectedUnit = this;
			FindTraversableTiles();
		}
		else if(!this.Unit && thisUnitManager.selectedUnit == !this && thisUnitManager.selectedUnit == !null)
		{
			AttackRoutine(attackingUnit);
		}
		else
		{
			isSelected = false;
			thisUnitMananger.selectedUnit = null;
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
	}
}