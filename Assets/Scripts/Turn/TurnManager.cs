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
    int turnCounter = 1; //Initializes turn counter
    Player [] player; //Creates array for the two store two Player class objects
    Player currentPlayer;  //Variable to represent the current player
   // currentPlayer = player[0]; //Assigns the current Player to the first Player in player[]
    int index = 0;
    
    public static TurnManager turnManager;
	private void Awake()
	{
		player = new Player[2];
		
	}
	private void Start()
	{
		player[0] = PlayerManager.playerManager.players[0];
		player[1] = PlayerManager.playerManager.players[1];
		currentPlayer = player[0]; //Assigns the current Player to the first Player in player[]
        player[1].Lock();
		UIManager.uiManager.UpdateUI(currentPlayer, turnCounter);
	}
	/*
        EndTurn() allows the current player to end t
        heir turn, locking them in from further input during
        the next Player's turn, assigns the currentPlayer to the next Player in player[], and increments the turn counter by 1.
    */

	public void EndTurn()
    {
        UnitManager.unitManager.selectedUnit = null;
        currentPlayer.Lock(); //Locks the current Player object
        index++;
		index %= 2;
        currentPlayer = player[index]; //Assigns currentPlayer to the next Player object in player[]
        currentPlayer.startTurn(); //Calls on the Player's startTurn function

        turnCounter++; //Increments turn counter by 1
		UIManager.uiManager.UpdateUI(currentPlayer, turnCounter);
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