using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

// Controls player behavior
public class Player : MonoBehaviour
{
	public List<Unit> units;
	public List<Building> buildings;

    private int moneyPerTurn;
	[SerializeField]
    private int currentMoney;
	private bool isActive;
	public bool isLocked;
    protected Building removeBuilding = null;
    public Color color;

	[SerializeField]
	string _playerName;

	public string playerName
	{
		get
		{
			return _playerName;
		}
	}

	void Awake()
    {
		
    }

	void InitData()
	{
		foreach (Unit unit in units)
			unit.SetOwner(this);
		foreach (Building building in buildings)
			building.SetOwner(this);
	}

	public int GetWinSum()
	{
		int total = 0;
		total += units.Count;
		total += buildings.Count;
		total += currentMoney;
		Debug.Log(name + " Total Score = " + total);
		return total;
	}

	private void Start()
	{
		InitData();
		moneyPerTurn = 10 * buildings.Count;
	}

	public void Lock()
	{
        foreach (Unit unit in units)
            unit.reset();
		this.isLocked = true;
	}
	public bool getActive()
	{
		return this.isActive;
	}
    public void setMoney(int newMoney)
    {
        currentMoney = newMoney;
    }
	public int getMoney()
	{
		return this.currentMoney;
	}
	public int GetMoneyPerTurn()
	{
		return this.moneyPerTurn;
	}
	public Color getColor()
	{
        return this.color;
	}
    public void removeUnit(Unit removedUnit)            // Removes unit from list 
    {
        units.Remove(removedUnit);

	}
	public void addUnit(Unit addedUnit)                 // Adds unit to list 
    {
        units.Add(addedUnit);
    }
	public void captureBuilding(Building addedBuilding) // Adds building to list 
	{
		buildings.Add(addedBuilding);
	}
    public void concedeBuilding(Building removedBuilding) // Removes building from list
	{
        buildings.Remove(removedBuilding);

	}

	public void disableUnitBuilding()
     {
         foreach (Building building in buildings)
         {
             building.GetComponentInChildren<Dropdown>().enabled = false;
         }
     }
     public void enableUnitBuilding()
     {
         foreach (Building building in buildings)
         {
             building.GetComponentInChildren<Dropdown>().enabled = true;
        }
     }

	// public void SetActive()
	// public void SetInactive()

	// Update 
	public void startTurn() 
	{
		// Updates the income from buildings 
		this.moneyPerTurn = 10 * buildings.Count; // Assuming the income per building is 10 
		this.currentMoney = this.currentMoney + this.moneyPerTurn; // Will update income per building 
		enableUnitBuilding();
		foreach (Unit thisunit in units) 
		{
			thisunit.reset(); // Resets Boolean variables for the units 
		}
		this.isLocked = false; // unlocks the current player
	}

	private void Update()
	{
		if (units.Count == 0 && buildings.Count == 0)
			TurnManager.turnManager.Lose(this);
	}

}
