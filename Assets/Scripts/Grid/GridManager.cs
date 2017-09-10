using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
	
	public GameObject gridButton; // assign in inspector
	public GridLayoutGroup grid; // assign in inspector

	public int rows, columns;

	public static GridManager gridMan; // used for global access to this class

	Tile[,] _tiles;
	Button[,] _gridButtons;

	public Tile[,] tiles
	{ 
		// C# property. get allows us to keep from modifying the variable _gridPosition directly. Instead, we can only 
		// get data from it. We can also check whether the 2D array exists or not whenever we call gridPosition
		get
		{
			return _tiles;
		}
	}

	public Button[,] gridButtons
	{
		get
		{
			if (_gridButtons == null)
				_gridButtons = new Button[rows, columns];

			return _gridButtons;
		}
	}

	private void Awake()
	{
		InitSingleton();

		_tiles = new Tile[rows, columns];
		_gridButtons = new Button[rows, columns]; // initialize our 2D array

		if (grid == null)
			grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridLayoutGroup>(); // automatically assign grid if we forgot to do it in inspector

		InitGrid();
	}

	void Start()
	{

	}
	
	void Update()
	{
		
	}

	void InitSingleton()
	{
		// singleton pattern. Assigns this instance of the class to gridMan if gridMan is null.
		// destroys this instance if a gridMan already exists. We only want one of these in our game.
		if (gridMan == null)
			gridMan = this;
		else if (gridMan != this)
			Destroy(gameObject);
	}

	void InitGrid()
	{
		GameObject newObj = null;

		for (int i = columns - 1; i >= 0; i--)
		{
			for (int j = 0; j < rows; j++)
			{
				// create a new game object in our scene. Will spawn a copy of our prefab and make it a child of the grid
				newObj = (GameObject)Instantiate(gridButton, grid.transform);
				_tiles[j, i] = new Tile(j, i, newObj.GetComponent<RectTransform>());
				_gridButtons[j, i] = newObj.GetComponent<Button>(); // assign x,y coordinates to each button.
				_gridButtons[j, i].GetComponentInChildren<Text>().text = j.ToString() + ", " + i.ToString(); // change button text.
				//newObj.GetComponent<Tile>().x = j;
				//newObj.GetComponent<Tile>().y = i;
			}
		}
	}
}
