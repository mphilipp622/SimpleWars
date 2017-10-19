using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	private List<Unit> units;
	private List<Building> buildings;

    private int moneyPerTurn;
    private int currentMoney;
	private bool isActive;
	public bool isLocked;
    protected Building removeBuilding = null;
    public Color color;

	void Awake()
    {
        units = new List<Unit>();
        buildings = new List<Building>();
    }
	public bool Lock()
	{
		return this.isLocked = true;
	}
	public bool getActive()
	{
		return this.isActive;
	}
	public int getMoney()
	{
		return this.currentMoney;
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
	// Update 
	public void startTurn() 
	{
		// Updates the income from buildings 
		this.moneyPerTurn = 10 * buildings.Count; // Assuming the income per building is 10 
		this.currentMoney = this.currentMoney + this.moneyPerTurn; // Will update income per building 
		foreach (Unit thisunit in units) 
		{
			thisunit.reset(); // Resets Boolean variables for the units 
		}
		this.isLocked = false; // unlocks the current player
	}

}
