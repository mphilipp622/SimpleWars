using UnityEngine;
using System.Collections;

public class TurnToWall : MonoBehaviour {

	public GameManager Game;
	// Use this for initialization
	void Start () {
	

	}

	 bool isWall;
	void OnMouseDown()
	{
		string [] splitter = this.gameObject.name.Split (',');
		if(!isWall)
		{
			Game.addWall(int.Parse(splitter[0]),int.Parse(splitter[1]));
			isWall = true;
			this.GetComponent<Renderer>().material.color = Color.red;



		}
		else
		{
			Game.removeWall(int.Parse(splitter[0]),int.Parse(splitter[1]));
			isWall = false;
			this.GetComponent<Renderer>().material.color = Color.white;
		}
		

	}


	// Update is called once per frame
	void Update () {
	
	}

}
