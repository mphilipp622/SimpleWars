using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitConstruction : MonoBehaviour
{
	private int currentMoney; 
	private int productioncostUnitInfantry = 10;
	private int productioncostUnitTank = 20;
	private int productioncostUnitChopper = 15;
	private bool canPurchase; 
	public Vector3 buildingPos;
	public int playerMoney;
    public GameObject unitTank;
    public GameObject unitInfantry;
    public GameObject unitChopper;

	public void buildUnit(string newName)
	{
//		buildingPos = GridManager.gridMan.selectedBuilding.gridPosition;
		playerMoney = PlayerManager.playerManager.GetActivePlayer().getMoney();
		if (newName == "Infantry") {
			canPurchase = playerMoney > productioncostUnitInfantry;
			if (canPurchase = true) {
				playerMoney = playerMoney - productioncostUnitInfantry;
                Instantiate(unitInfantry, buildingPos, Quaternion.identity);
			} else {
				// Return nothing due to insufficient funds 
				return;
			}
		} else if (newName == "Tank") {
			canPurchase = playerMoney > productioncostUnitTank;
			if (canPurchase = true) {
				playerMoney = playerMoney - productioncostUnitTank;
                Instantiate(unitTank, buildingPos, Quaternion.identity);
			} else {
				// Return nothing due to insufficient funds 
				return;
			}
		} else if (newName == "Chopper") {
			canPurchase = playerMoney > productioncostUnitChopper;
			if (canPurchase = true) {
				playerMoney = playerMoney - productioncostUnitChopper;
//				Instantiate(prefabnameInfantry, buildingPos, Quaternion.identity);
			} else {
				// Return nothing due to insufficient funds 
				return;
			}
		}
	}
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}