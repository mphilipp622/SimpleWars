using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UnitConstruction : MonoBehaviour
{
	private int currentMoney; 
	private int productioncostUnitInfantry = 10;
	private int productioncostUnitTank = 10;
	private int productioncostUnitChopper = 10;
	private bool canPurchase; 
	public Vector3 buildingPos;
	public int playerMoney;
    public GameObject unitTank;
    public GameObject unitInfantry;
    public GameObject unitChopper;
    public int unitChoice;
    public Dropdown selectedropDown;
    //public Button yourButton;
    //public Transform dropdownMenu;
    //public int value;

    void Start()
    {
        Debug.Log(gameObject.GetComponent<Tile>().gridPosition);
        selectedropDown.value = 4;
    }

    public void buildUnit()
    {
        buildingPos = gameObject.GetComponent<Tile>().gridPosition;
        playerMoney = PlayerManager.playerManager.GetActivePlayer().getMoney();
        //int chosenValue = GameObject.Find("UnitConstruction").GetComponent<Dropdown>().value;//selectedropDown.value;
        //Debug.Log(selectedropDown.value);

        if (selectedropDown.value == 0)
        {
            canPurchase = playerMoney >= productioncostUnitInfantry;
            if (canPurchase == true)
            {
                playerMoney = playerMoney - productioncostUnitInfantry;
                Debug.Log(playerMoney);
                PlayerManager.playerManager.GetActivePlayer().setMoney(playerMoney);
                UIManager.uiManager.updateMoney(playerMoney);
                GameObject temp = Instantiate(unitTank, buildingPos, Quaternion.identity, GridManager.gridMan.grid.transform.Find("Units").transform);
                temp.GetComponent<RectTransform>().anchoredPosition = buildingPos;
                temp.GetComponent<Unit>().SetOwner(gameObject.GetComponent<Building>().GetOwner());

                selectedropDown.value = 4;
            }
            else
            {
                // Return nothing due to insufficient funds 
                return;

            }
        }
        else if (selectedropDown.value == 1)
        {
            //Debug.Log("Reached Section");
            canPurchase = playerMoney > productioncostUnitTank;
            if (canPurchase == true)
            {
                playerMoney = playerMoney - productioncostUnitTank;
                Debug.Log(playerMoney);
                PlayerManager.playerManager.GetActivePlayer().setMoney(playerMoney);
                //Instantiate(unitTank, buildingPos, Quaternion.identity, GridManager.gridMan.grid.transform.Find("Units").transform);
                GameObject temp = Instantiate(unitTank, buildingPos, Quaternion.identity, GridManager.gridMan.grid.transform.Find("Units").transform);
                temp.GetComponent<RectTransform>().anchoredPosition = buildingPos;
                temp.GetComponent<Unit>().SetOwner(gameObject.GetComponent<Building>().GetOwner());
                
                selectedropDown.value = 4;
            }
            else
            {
                // Return nothing due to insufficient funds
                return;
            }
        }
        else if (selectedropDown.value == 2)
        {
            //Debug.Log("Reached Section");
            canPurchase = playerMoney > productioncostUnitChopper;
            if (canPurchase == true)
            {
                playerMoney = playerMoney - productioncostUnitChopper;
                Debug.Log(playerMoney);
                PlayerManager.playerManager.GetActivePlayer().setMoney(playerMoney);
                UIManager.uiManager.UpdateUI(gameObject.GetComponent<Building>().GetOwner(),0);
                GameObject temp = Instantiate(unitTank, buildingPos, Quaternion.identity, GridManager.gridMan.grid.transform.Find("Units").transform);
                temp.GetComponent<RectTransform>().anchoredPosition = buildingPos;
                temp.GetComponent<Unit>().SetOwner(gameObject.GetComponent<Building>().GetOwner());
                
                selectedropDown.value = 4;
            }
            else
            {
                // Return nothing due to insufficient funds 
                return;
            }
        }
    }

/*
	public void buildUnit(string newName)
	{
        //Dropdown uiDropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        //int menuIndex = dropdownMenu.GetComponent<Dropdown>().value;
        //List<Dropdown.OptionData> menuOptions = dropdownMenu.GetComponent<Dropdown>().options;
        //string value = menuOptions[menuIndex].text;

        buildingPos = GridManager.gridMan.selectedBuilding.gridPosition;
		playerMoney = PlayerManager.playerManager.GetActivePlayer().getMoney();
        
        if (newName == "Infantry") {
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
		} else if (newName == "Jet") {
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
*/
}