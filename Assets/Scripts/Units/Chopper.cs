using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopper : Unit
{
	public Chopper() 
	{
		this.hp = 10;
		this.attack = 3;
		this.defense = 3;
		this.movement = 5;
		this.range = 1;
		this.productionCost = 5;
	}
}