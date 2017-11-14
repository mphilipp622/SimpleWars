using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildingDropDown : MonoBehaviour
{
    [Tooltip("The drop down box to populate with Unit Types")]
    public Dropdown dropDown;

    [Tooltip("Createable Units")]
    public Sprite[] units;

    void Start()
    {
        
        dropDown.ClearOptions();


        List<Dropdown.OptionData> unitTypes = new List<Dropdown.OptionData>();
        foreach (var unitType in units)
        {
            string unitName = unitType.name;
            int dot = unitType.name.IndexOf('.');
            if (dot >= 0)
            {
                unitName = unitName.Substring(dot + 1);
            }

            // Add the option to the list
            var unitOption = new Dropdown.OptionData(unitName, unitType);
            unitTypes.Add(unitOption);
        }

        // Add the options to the drop down box
        dropDown.AddOptions(unitTypes);
    }
}