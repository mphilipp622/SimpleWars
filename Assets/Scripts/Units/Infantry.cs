using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		//this.captureBuilding = false;
	}
}
