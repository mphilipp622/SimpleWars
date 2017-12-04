using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager uiManager;

	[SerializeField]
	Text moneyText, moneyPerTurnText, turnText, currentPlayerText;

	private void Awake()
	{
		initSingleton();
	}
	void Start ()
	{

	}
	
	void Update ()
	{
		
	}

	public void UpdateUI(Player currentPlayer, int turnCounter)
	{
		turnText.text = "Turn: " + turnCounter;
		moneyText.text = "Money: $" + currentPlayer.getMoney();
		moneyPerTurnText.text = "Money Per Turn: $" + currentPlayer.GetMoneyPerTurn();
		currentPlayerText.text = PlayerManager.playerManager.GetActivePlayer().playerName;
		currentPlayerText.color = PlayerManager.playerManager.GetActivePlayer().color;
	}
    public void updateMoney(int newMoney)
    {
        moneyText.text = "Money: $" + newMoney.ToString();
    }
	void initSingleton()
	{
		if (uiManager == null)
			uiManager = this;
		else if (uiManager != this)
			Destroy(gameObject);
	}
}
