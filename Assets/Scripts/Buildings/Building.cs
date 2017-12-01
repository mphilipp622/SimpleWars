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

	// Slider variables
	[SerializeField]
	Image sliderBackground, sliderFill;

	[SerializeField]
	Slider captureSlider;

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

	private void Start()
	{
		captureSlider.value = 0;
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
		SetSliderColors();
	}

	void SetSliderColors()
	{
		// The slider on the building indicates capture progress of an enemy troop against the building.
		// The slider background color will be set to the color of this building's owner.
		// the slider fill will be set to the color of the other player.
		// As the other player takes over this building, the slider will move to the right, making the other player's color start taking over
		sliderBackground.color = capturedBy.color; // background color of the slider is set to the owner's color.

		foreach(Player player in PlayerManager.playerManager.players)
		{
			// iterate over our players and grab the player that is NOT the owner of this building and assign the fill color for the slider to the other player's color.
			if (player != capturedBy)
				sliderFill.color = player.color;
		}
	}

	//This function will be called on by the Building's CaptureCheck() function if the 
	//current unit has stayed on the building for 3 turns.
	public void Capture()
    {
        if (this.capturedBy != null)
            this.capturedBy.concedeBuilding(this);
        this.capturedBy = currentUnit.getUnitOwner();//update this 
        this.capturedBy.captureBuilding(this);
        this.captureFlag = 0;
		SetSliderColors();
		UpdateCaptureIndicators();
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
		captureFlag = 0;
		UpdateCaptureIndicators();
    }

	void UpdateCaptureIndicators()
	{
		captureSlider.value = captureFlag;
		GetComponent<Image>().color = capturedBy.color;
	}

	//This function will be called by Unit.Reset() function at the start of every
	//unit's turn.
	public void CaptureCheck()
    {
		if (currentUnit.getUnitOwner() == capturedBy)
			// if the unit on the building is owned by the building owner, then exit
			return;

		if(currentUnit == null)
		{
			captureFlag = 0;
			UpdateCaptureIndicators();
			return;
		}

		if(this.previousUnit == null && this.currentUnit != null)
		{
			// if a unit has moved onto this building and nobody was previously on the building, increment the capture by one
			captureFlag += 1;
			GetComponent<Image>().color = Color.Lerp(capturedBy.color, currentUnit.getUnitOwner().color, captureFlag / 3f);
		}
		else if (this.currentUnit == this.previousUnit)
		{
			// if the unit on this building is the same unit from last turn, add 1 to the turn counter.
			captureFlag += 1;
			GetComponent<Image>().color = Color.Lerp(capturedBy.color, currentUnit.getUnitOwner().color, captureFlag / 3f);
		}
		else
		{
			// if a different unit has stepped on the building set flag back to 0.
			captureFlag = 0;
			this.previousUnit = this.currentUnit;
			UpdateCaptureIndicators();
		}

		captureSlider.value = captureFlag;

		if (captureFlag >= 3)
            this.Capture();
    }

}

