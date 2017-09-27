using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
	public static UnitManager unitManager;

	Unit _selectedUnit;

	public Unit selectedUnit
	{
		get
		{
			return _selectedUnit;
		}
		set
		{
			_selectedUnit = value;
		}
	}

	private void Awake()
	{
		InitSingleton();
	}

	void Start ()
	{
		
	}
	
	void Update ()
	{ 
		
	}

	void InitSingleton()
	{
		// singleton pattern. Assigns this instance of the class to gridMan if gridMan is null.
		// destroys this instance if a gridMan already exists. We only want one of these in our game.
		if (unitManager == null)
			unitManager = this;
		else if (unitManager != this)
			Destroy(gameObject);
	}
}
