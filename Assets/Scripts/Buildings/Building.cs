using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour {

    //global vars
    protected Player capturedBy;
    protected int goldPerTurn;
    protected Color color;
    protected int captureFlag;
    protected Unit currentUnit;
    protected Unit previousUnit;
    //public Vector3 buildingPos;

    // Constructor
    public Building()
    {
        this.capturedBy = null;
        this.goldPerTurn = 10;
        this.captureFlag = 1;
        this.color = Color.gray;
        this.currentUnit = null;
        this.previousUnit = null;
    }

	private void Start()
	{

	}

	private void Update()
	{

	}

	public void SetOwner(Player newOwner)
	{
		/// <summary>
		/// This will be called at start of game. It will be called on by a Player script.
		/// It will assign the owner of this building and update the color of the building.
		/// 
		/// 1. set this owner to newOwner
		/// 2. Change color of the building's Image component to the owner's color (red or blue depending on owner).
		/// </summary>
		///
		capturedBy = newOwner;
		GetComponent<Image>().color = newOwner.color;
	}

	//This function will be called on by the Building's CaptureCheck() function if the 
	//current unit has stayed on the building for 3 turns.
	public void Capture()
    {
        if (this.capturedBy != null)
            this.capturedBy.concedeBuilding(this);
        this.capturedBy = currentUnit.getUnitOwner();//update this 
        this.capturedBy.captureBuilding(this);
        this.captureFlag = 1;
        GetComponent<Image>().color = capturedBy.color;
    }

    //public Vector3 getbuildingPos()
    //{
    //    return this.buildingPos;
    //}

    public Player GetOwner()
    {
        return this.capturedBy;
    }

    //returns the goldPerTurn stat of the building.
    public int GetGoldPerTurn()
    {
        return this.goldPerTurn;
    }

    public void SetCurrentUnit(Unit newUnit)
    {
        this.currentUnit = newUnit;
    }

    //This function will be called by Unit.Reset() function at the start of every
    //unit's turn.
    public void CaptureCheck()
    {
        if (this.currentUnit == this.previousUnit)
            captureFlag += 1;
        else
        {
            captureFlag = 1;
            this.previousUnit = this.currentUnit;
        }

        if (captureFlag >= 3)
            this.Capture();
    }

}

