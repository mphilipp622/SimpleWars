using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour 
{
	// Unit Attributes
	[SerializeField] 
	protected int hp, attack, defense,range,movement,productionCost;
	// Movement 
	protected bool isSelected; 
	protected bool hasAttacked;
	protected bool hasMoved;

	// Special Attributes 
	protected bool captureState; // remove
	// Map Attributes 
	private int xPos;
	private int yPos;
	Vector3 worldPosition;// grid posisition
	Building checkBuilding;

	// Update Comment
	
	// Map Attributes 
	public int getxPos()
	{
		return this.xPos;
	}
	
	public int getyPos()
	{
		return this.yPos;
	}

	// Unit Attributes for receiving variable values
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

	// Movement 
	public bool getisSelected()
	{ 
		return this.isSelected;
	}

	public bool gethasAttacked()
	{
		return this.hasAttacked;
	}

	// Special Attributes 
	public bool getcaptureState()
	{
		return this.captureState;
	}

//	public void takeDamage(int damage) // pass in hp and attack
	public void takeDamage(int attackerDamage, int attackerHp)
	{
		// this.hp -= damage; 
		this.hp = (this.hp) - ((attackerDamage)*(attackerHp/10))/(this.defense);
		//result = floor((((health./10).*attacker_attack)./defender_defense).*attacker_attack)+1;
		if (hp <= 0)
		{
			die();
		}
	}

	private void die()
	{
		Destroy(gameObject); // will need modification due to removal from the player dictionary 
	}

	public void increaseStats(int attackMod, int defenseMod)
	{
		attack += attackMod;
		defense += defenseMod;
	}

	public void resetStats(int attackMod, int defenseMod)
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
	
	public void startMove(Unit thisUnit) // ???
	{
		//Code coming soon!
	}

	public bool getHasMoved() //Returns boolean value of 'hasMoved' when called.
	{
		return hasMoved;
	}
}
