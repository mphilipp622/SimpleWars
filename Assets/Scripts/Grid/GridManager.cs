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

	private void Awake()
	{
		InitSingleton();

		_tiles = new Tile[columns, rows];

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
		foreach(Tile tile in grid.GetComponentsInChildren<Tile>())
		{
			RectTransform thisRect = tile.GetComponent<RectTransform>();
			int newX = (int) Mathf.Abs((thisRect.anchoredPosition.x / grid.cellSize.x));
			int newY = (int) Mathf.Abs((thisRect.anchoredPosition.y / grid.cellSize.y));
			_tiles[newX, newY] = tile;
			_tiles[newX, newY].InitializeData(newX, newY, thisRect);
		}
	}
}
