<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Unit {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//update comment
public class Tank : Unit
{
	public Tank()
	{
		this.hp = 10;
		this.attack = 4;
		this.defense = 4;
		this.movement = 4;
		this.range = 1;
		this.productionCost = 5;
	}
}
>>>>>>> origin/JosephUnit
