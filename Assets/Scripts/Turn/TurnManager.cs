using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    /*
        TurnManager :: endTurn();
            1. currentPlayer.Lock();
            2. set currentPlayer to next Player (Players[index++ % = 2])
            3. currentPlayers.startTurn()   
    */

public class TurnManager : MonoBehaviour
{
    int turnCounter = 0; //Initializes turn counter
    Player [] player = new Player [2]; //Creates array for the two store two Player class objects
    Player currentPlayer;  //Variable to represent the current player
    currentPlayer = player[0]; //Assigns the current Player to the first Player in player[]
    int index = 0;
    
    public static TurnManager turnManager;

    /*
        EndTurn() allows the current player to end their turn, locking them in from further input during
        the next Player's turn, assigns the currentPlayer to the next Player in player[], and increments the turn counter by 1.
    */
    
    public void EndTurn()
    {
        currentPlayer.Lock(); //Locks the current Player object
        index++;
        currentPlayer = player[index %= 2]; //Assigns currentPlayer to the next Player object in player[]
        currentPlayer.startTurn(); //Calls on the Player's startTurn function

        turnCounter++; //Increments turn counter by 1
    }

    void initSingleton()
    {
        if(turnManager == null)
            turnManager = this;
        else if (turnManager != this)
            Destroy(gameObject);
    }

    void start()
    {
        
    }   

    void update()
    {

    }
}