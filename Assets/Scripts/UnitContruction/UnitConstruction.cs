using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
    public int unitChoice;

    public Transform dropdownMenu;
    //public int value;



	public void buildUnit(string newName)
	{
        //Dropdown uiDropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        int menuIndex = dropdownMenu.GetComponent<Dropdown>().value;
        List<Dropdown.OptionData> menuOptions = dropdownMenu.GetComponent<Dropdown>().options;
        string value = menuOptions[menuIndex].text;
        buildingPos = GridManager.gridMan.selectedBuilding.gridPosition;
		playerMoney = PlayerManager.playerManager.GetActivePlayer().getMoney();
        
        if (value == "Tank") {//(newName == "Infantry") {
			canPurchase = playerMoney > productioncostUnitInfantry;
			if (canPurchase == true) {
				playerMoney = playerMoney - productioncostUnitInfantry;
                Instantiate(unitTank, buildingPos, Quaternion.identity);
			} else {
				// Return nothing due to insufficient funds 
				return;
			}
		} else if (newName == "Tank") {
			canPurchase = playerMoney > productioncostUnitTank;
			if (canPurchase == true) {
				playerMoney = playerMoney - productioncostUnitTank;
                Instantiate(unitTank, buildingPos, Quaternion.identity);
			} else {
				// Return nothing due to insufficient funds 
				return;
			}
		} else if (newName == "Chopper") {
			canPurchase = playerMoney > productioncostUnitChopper;
			if (canPurchase == true) {
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