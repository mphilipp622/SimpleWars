using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//End Turn Button -> Lock out inactive player -> refresh active player unit movements -> increment active played gold -> increment turn

/*
    This will manage the in-game turn system between players, needs the GameManager for other features.

    -Game elements affected
        -Manage the start and end of the turns.
        -Inactive player is locked out for Active Player turn.
        -Unit Action and Movements are refreshed.
        -Gold Count is incremented depending on resources.
        -Capture System increments and determines if anything is captured.
*/


public class TurnManager : MonoBehaviour
{
    void start()
    {
        
    }   

    void update()
    {

    }
}