using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	private List<Unit> units;
	private List<Building> buildings;

	private bool isActive;
    private int moneyPerTurn;
    private int currentMoney;
	private Color playerColor;
   
	public bool startTurn;
    public bool captureBuilding;
    public bool concedeBuilding;

	void Awake()
    {
        units = new List<Unit>();
        buildings = new List<Building>();
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
		return this.playerColor;
	}

}
