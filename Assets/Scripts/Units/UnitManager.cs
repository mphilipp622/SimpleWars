using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
	/// <summary>
	/// This class will be static and global so every other script in the game can access it and its public members and functions.
	/// This class keeps track of the currently selected unit in the scene. Because so many scripts depend on this information,
	/// this class needs to be static and global for access.
	/// </summary>
	
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
		// singleton pattern. Assigns this instance of the class to unitManager if unitManager is null.
		// destroys this instance if a unitManager already exists. We only want one of these in our game.
		if (unitManager == null)
			unitManager = this;
		else if (unitManager != this)
			Destroy(gameObject);
	}
}
