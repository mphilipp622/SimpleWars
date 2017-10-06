using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//End Turn Button -> Lock out inactive player -> refresh active player unit movements -> increment active played gold -> increment turn
//
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
    
    /*
        Player array, circular, size 2, current player is a variable, turn ends currentplayer.lock prevents player to do stuff
        Updates the player class to let know its active
        int current player = 0;
        current index in cir array 


        Example:
            currentPlayer.Lock();
            currentIndex++;
            currentIndex %= 2 
            curentPlayer = activePlayers. [current.index]
            currentPlayer.setActive ();
    */
    void start()
    {
        
    }   

    void update()
    {

    }


}