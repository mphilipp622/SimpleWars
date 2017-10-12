using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    //global vars
    protected Player capturedBy;
    protected int goldPerTurn;
    protected Color color;
    protected int captureFlag;
    protected Unit currentUnit;
    protected Unit previousUnit;

    // Constructor
    public Building()
    {
        this.capturedBy = null;
        this.goldPerTurn = 10;
        this.captureFlag = 0;
        this.color = Color.gray;
        this.currentUnit = null;
        this.previousUnit = null;
    }

    //This function will be called on by the Building's CaptureCheck() function if the 
    //current unit has stayed on the building for 3 turns.
    public void Capture()
    {
        if (this.capturedBy != null)
			this.capturedBy.concedeBuilding();		// Checks list and removes building from player list
        this.capturedBy = currentUnit;				//update this 
        this.capturedBy.captureBuilding(this);		// Checks list and updates it with capture owner
        this.captureFlag = 0;
        this.color = capturedBy.color;

    }

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
            captureFlag = 0;
            this.previousUnit = this.currentUnit;
        }

        if (captureFlag >= 3)
            this.Capture();
    }

}

