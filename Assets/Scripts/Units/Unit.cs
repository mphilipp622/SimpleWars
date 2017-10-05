using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	// Unit Attributes
	[SerializeField] 
	protected int hp, attack, def,range,movement,productionCost;
	// Movement 
	protected bool isSelected; 
	protected bool hasAttacked;
	// Special Attributes 
	protected bool captureState; // remove
	// Map Attributes 
	private int xPos;
	private int yPos;
	Vector3 worldPosition;// grid posisition


	
	// Map Attributes 
	public int getxPos()
	{
		return this.xPos;
	}
	public int getyPos()
	{
		return this.yPos;
	}

	// Unit Attributes 
	public int gethp()
	{
		return this.hp;
	}
	public int getattack()
	{
		return this.attack;
	}
	public int getdef()
	{
		return this.def;
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
		this.hp = (this.hp) - ((attackerDamage)*(attackerHp/10))/(this.def);
		//result = floor((((health./10).*attacker_attack)./defender_defense).*attacker_attack)+1;
		if (hp<=0)
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
}
