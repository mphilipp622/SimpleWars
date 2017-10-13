using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParentUnit : MonoBehaviour {

	GridLayoutGroup grid;
	GridManager gridMan;
	RectTransform thisRect;
	float newX, newY;

	public int rows, columns;

	private void Awake()
	{
		InitData();
	}

	void Start ()
	{
		
	}
	
	void Update ()
	{
		newX = Mathf.Round(thisRect.anchoredPosition.x - ((thisRect.anchoredPosition.x % grid.cellSize.x) - grid.cellSize.x / 2));
		newY = Mathf.Round(thisRect.anchoredPosition.y - ((thisRect.anchoredPosition.y % grid.cellSize.y) + grid.cellSize.y / 2));
		
		newX = Mathf.Clamp(newX, grid.cellSize.x / 2, (grid.cellSize.x * columns) - grid.cellSize.x / 2);
		newY = Mathf.Clamp(newY, -((grid.cellSize.x * rows) - grid.cellSize.y / 2), -(grid.cellSize.y / 2));
		
		thisRect.anchoredPosition = new Vector3(newX, newY, thisRect.localPosition.z);
	}

	void InitData()
	{
		gridMan = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();
		grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridLayoutGroup>();
		//thisRect = GetComponent<RectTransform>();
		//thisRect.sizeDelta = grid.cellSize; // force width and height to current grid cell size.

		// force rows and columns to adapt to the current cell size and the 16:9 aspect ratio.
		// This calculation will allow us to calculate the needed rows and columns based on cell size.
		//rows = Mathf.RoundToInt(Screen.currentResolution.height / grid.cellSize.y);
		//columns = Mathf.RoundToInt(Screen.currentResolution.width / grid.cellSize.x);
		rows = gridMan.rows;
		columns = gridMan.columns;

		//GetComponent<BoxCollider2D>().size = thisRect.sizeDelta; // force the collider on the tile to match the current grid cell size
	}
}
