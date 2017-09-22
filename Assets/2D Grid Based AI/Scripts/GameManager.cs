using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class GameManager : MonoBehaviour {



	 
/* 		Author : Saad Khawaja
	 *  http://www.saadkhawaja.com
	 * 	http://www.twitter.com/saadskhawaja

	 *     This file is part of Grid Based A* - Tower Defense.

		    Grid Based A* - Tower Defense is free software: you can redistribute it and/or modify
		    it under the terms of the GNU General Public License as published by
		    the Free Software Foundation, either version 3 of the License, or
		    (at your option) any later version.

		    Grid Based A* - Tower Defense is distributed in the hope that it will be useful,
		    but WITHOUT ANY WARRANTY; without even the implied warranty of
		    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
		    GNU General Public License for more details.


	 * 
*/ 

	public MyPathNode[,] grid;
	public GameObject enemy;
	public GameObject gridBox;
	public int gridWidth;
	public int gridHeight;
	public Sprite carUp;
	public Sprite carDown;
	public Sprite carFront;
	public Sprite carBack;
	public float gridSize;
	public GUIStyle lblStyle;

	public static string distanceType;
	

	//This is what you need to show in the inspector.
	public static int distance = 2;


	void Start () {
	
	
		//Generate a grid - nodes according to the specified size
		grid = new MyPathNode[gridWidth, gridHeight];

		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				//Boolean isWall = ((y % 2) != 0) && (rnd.Next (0, 10) != 8);
				Boolean isWall = false;
				grid [x, y] = new MyPathNode ()
				{
					IsWall = isWall,
					X = x,
					Y = y,
				};
			}
		}

		//instantiate grid gameobjects to display on the scene
		createGrid ();

		//instantiate enemy object
		createEnemy ();

	
	}


	void OnGUI()
	{
		if(GUI.Button(new Rect(0f,0f,200f,50f),"Create Enemy"))
		{
			createEnemy();
		}
		if(GUI.Button(new Rect(0f,60f,200f,50f),"Reload"))
		{
			Application.LoadLevel(Application.loadedLevel);
		}

		GUI.Label(new Rect(5f,120f,200f,200f),"Click on the grid to place a wall/tower.\nYou can change the distance formula of the path to Euclidean, " +
			"Manhattan etc\nYou can also change the Grid size in the GameManager variables from the inspector",lblStyle);
	}


	void createGrid()
	{
	//Generate Gameobjects of GridBox to show on the Screen
		for (int i =0; i<gridHeight; i++) {
			for (int j =0; j<gridWidth; j++) {
				GameObject nobj = (GameObject)GameObject.Instantiate(gridBox);
				nobj.transform.position = new Vector2(gridBox.transform.position.x + (gridSize*j), gridBox.transform.position.y + (0.87f*i));
				nobj.name = j+","+i;

				nobj.gameObject.transform.parent = gridBox.transform.parent;
				nobj.SetActive(true);

			}
		}
	}

	void createEnemy()
	{
		GameObject nb = (GameObject)GameObject.Instantiate (enemy);
		nb.SetActive (true);
	}


	// Update is called once per frame
	void Update () {
	
	}
	
	public void addWall (int x, int y)
	{
		grid [x, y].IsWall = true;
	}
	
	public void removeWall (int x, int y)
	{
		grid [x, y].IsWall = false;
	}

}
