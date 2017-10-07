using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
	public static UnitManager unitManager;
	// Will be using arrays instead of dictionary:
	//Dictionary<GameObject,Unit> Units;
	public GameObject prefab;

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
		// Will be using arrays instead of dictionary:
    	//Units = new Dictionary<GameObject,Unit>();
        //GameObject newObj=(GameObject)Instantiate(prefab,Vector3.zero,Quaternion.identity);// spawns in the middle of the screen 
    	//Units.Add(newObj,newObj.GetComponent<Unit>());
        //Units[newObj].gethp();
	}

	void Start()
	{

	}

	void Update()
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
