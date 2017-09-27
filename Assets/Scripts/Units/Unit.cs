using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

	public int x, y, movement = 2, range = 1;

	public int attack = 2, defense = 1, hp = 10;

	bool isSelected = false;

	public bool hasAttacked = false;

	public Vector2 gridPosition
	{
		get
		{
			return new Vector2(x, y);
		}
	}

	void Start ()
	{
		transform.position = GridManager.gridMan.tiles[x, y].position;
	}
	
	void Update ()
	{
		
	}

	protected void FindTraversableTiles()
	{
		for (int i = -1; i < 2; i++)
		{
			for (int j = -1; j < 2; j++)
			{
				if (i == 0 && j == 0)
					continue;

				if (x + i > GridManager.gridMan.tiles.GetLength(0) - 1 || x + i < 0 || y + j > GridManager.gridMan.tiles.GetLength(1) - 1 || y + j < 0)
					continue;

				Tile currentTile = GridManager.gridMan.tiles[x + i, y + j];

				float totalCost = 0;

				while (totalCost <= movement)
				{
					totalCost += (Vector2.Distance(currentTile.gridPosition, GridManager.gridMan.tiles[currentTile.x - i, currentTile.y - j].gridPosition) / GridManager.gridMan.grid.cellSize.x) + currentTile.movementModifier;

					if (totalCost <= movement && currentTile.unit == null)
						currentTile.SetTraversable();
					else if (totalCost > movement)
						break;

					// bounds checking
					if (currentTile.x + i > GridManager.gridMan.tiles.GetLength(0) - 1 || currentTile.x + i < 0 || currentTile.y + j > GridManager.gridMan.tiles.GetLength(1) - 1 || currentTile.y + j < 0)
						break;

					currentTile = GridManager.gridMan.tiles[currentTile.x + i, currentTile.y + j]; // assign next tile
				}
			}

		}
	}
	
	private void OnMouseDown()
	{

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
			AttackRoutine();
		}
		else if (isSelected)
		{
			// if this unit is selected and we click on it again, cancel selection
			isSelected = false;
			UnitManager.unitManager.selectedUnit = null;
		}
	}

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

	void HasAttacked()
	{
		isSelected = false;
		hasAttacked = true;
		UnitManager.unitManager.selectedUnit = null;
	}

	void AttackRoutine()
	{
		// check if attacker is in range of our potential defending unit
		if (!CheckAttackRange())
			return; // exit the function if not in range

		// If we make it here, then we're in range. Execute unit attack against this unit
		UnitManager.unitManager.selectedUnit.Attack(this); // attack unit and make it take damage

		// now the unit gets to return fire if they are in range
		if (!CheckReturnFireRange())
			return;

		// if we make it here, then defender is in range. Execute a counter attack against attacking unit
		this.Attack(UnitManager.unitManager.selectedUnit);

		// if we make it all the way here, then we can set the selectedUnit's turn to be over
		UnitManager.unitManager.selectedUnit.HasAttacked();
	}

	bool CheckAttackRange()
	{
		/// <summary>
		/// Check if enemy unit is in range to attack. Returns true if distance is within range. False otherwise.
		/// Terrain modifiers for movement are not considered. This is a standard distance calculation with no weights.
		/// </summary>
		
		if (Vector2.Distance(gridPosition, UnitManager.unitManager.selectedUnit.gridPosition) > UnitManager.unitManager.selectedUnit.range)
			return false;
		else
			return true;
	}

	bool CheckReturnFireRange()
	{
		/// <summary>
		/// Check if the defending unit is in attack range of the attacking unit.
		/// As with the CheckAttackRange function, terrain movement modifiers are not considered here.
		/// </summary>

		if (Vector2.Distance(gridPosition, UnitManager.unitManager.selectedUnit.gridPosition) > this.range)
			return false;
		else
			return true;
	}

	void Attack(Unit defendingUnit)
	{
		/// <summary>
		/// call TakeDamage on defendingUnit. Pass the attacking unit's attack value as a parameter
		/// We don't currently need this function. We could just call TakeDamage directly in the AttackRoutine function
		/// However, if we want to create some kind of Attack state, which can dictate animations, we will want a separate function like this
		/// </summary> 
		
		defendingUnit.TakeDamage(this.attack, this.hp);
	}

	void TakeDamage(int attackDamage, int attackerHP)
	{
		///<summary>
		/// Perform damage calculations against this unit.
		/// attackDamage variable includes terrain modifiers since Tile class is handling updating those stats.
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
