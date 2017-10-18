using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public static PlayerManager playerManager;

	Player activePlayer, inactivePlayer;

	public Player GetActivePlayer()
	{
		return activePlayer;
	}

	public Player GetInactivePlayer()
	{
		return inactivePlayer;
	}

	public void SetActivePlayer(Player newActivePlayer)
	{
		inactivePlayer = activePlayer; // set inactiveplayer to currently active player
		activePlayer = newActivePlayer; // set active player to new active player
	}

	void InitSingleton()
	{
		// singleton pattern. Assigns this instance of the class to gridMan if gridMan is null.
		// destroys this instance if a gridMan already exists. We only want one of these in our game.
		if (playerManager == null)
			playerManager = this;
		else if (playerManager != this)
			Destroy(gameObject);
	}
}
