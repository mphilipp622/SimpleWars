using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	protected int hp;
	protected int attack; 
	protected int def; 
	protected int range;
	protected int movement; 
	protected int productionCost;
	protected int captureBuilding;
	private int xPos;
	private int yPos;
	Vector3 worldPosition;

	// Constructor 
	public Unit()
	{

	}	

	// Return values 
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
}

public class Infantry : Unit
{
	public Infantry()
	{
		this.hp = 10;
		this.attack = 2;
		this.def = 2;
		this.movement = 3;
		this.range = 2;
		this.productionCost = 2;
		this.captureBuilding = 50;
	}
}

public class Chopper : Unit
{
	public Chopper() 
	{
		this.hp = 10;
		this.attack = 3;
		this.def = 3;
		this.movement = 5;
		this.range = 1;
		this.productionCost = 5;
	}
}

public class Tank : Unit
{
	public Tank()
	{
		this.hp = 10;
		this.attack = 4;
		this.def = 4;
		this.movement = 4;
		this.range = 1;
		this.productionCost = 5;
	}
}