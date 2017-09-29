using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    protected bool isCaptured;
    protected string capturedBy;
    protected int goldPerTurn;

    // Constructor
    public Building()
    {
        this.isCaptured = false;
        this.capturedBy = null;
        this.goldPerTurn = 10;
    }

    public void Capture(string capturer)
    {
        this.isCaptured = true;
        this.capturedBy = capturer;
    }

    public string getOwner()
    {
        return this.capturedBy;
    }


    public int GetGoldPerTurn()
    {
        return this.goldPerTurn;
    }

}

