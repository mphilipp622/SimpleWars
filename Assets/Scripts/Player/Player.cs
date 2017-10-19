using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	private List<Unit> units;
	private List<Building> buildings;

    private int moneyPerTurn;
    private int currentMoney;
	public Color color;
	private bool isActive;
	//private bool captureBuilding;
	//private bool ConcedeBuilding;
	public bool isLocked;
    protected Building removeBuilding = null;

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
	public void captureBuilding(Building addedBuilding)
	{
		buildings.Add(addedBuilding);
	}
    public void concedeBuilding(Building removedBuilding)
	{
        buildings.Remove(removedBuilding); 
	}
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
