using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
#if (UNITY_EDITOR)
using UnityEditor;


[ExecuteInEditMode]
public class ParentTile : MonoBehaviour {
	/// <summary>
	/// This script will execute in the editor, not in play mode.
	/// It is used for snapping tiles to the grid in the scene view. This allows Isaac to put tiles wherever he wants and have them
	/// adhere to the correct cell sizing and grid boundaries.
	/// </summary>
	
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
		DestroyImmediate(GetComponent<SpriteRenderer>());
	}

	void Update ()
	{
		if (Application.isPlaying)
		{
			this.enabled = false;
		}

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
		thisRect = GetComponent<RectTransform>();
		thisRect.sizeDelta = grid.cellSize; // force width and height to current grid cell size.

		// force rows and columns to adapt to the current cell size and the 16:9 aspect ratio.
		// This calculation will allow us to calculate the needed rows and columns based on cell size.
		rows = gridMan.rows;
		columns = gridMan.columns;

		GetComponent<BoxCollider2D>().size = thisRect.sizeDelta /1.05f ; // force the collider on the tile to match the current grid cell size		transform.parent = grid.transform; // assign object to be child of grid.
		if (transform.tag == "Building")
			transform.SetParent(GameObject.FindGameObjectWithTag("Grid").transform.Find("Buildings").transform);
		else if (transform.tag == "Unit")
			transform.SetParent(GameObject.FindGameObjectWithTag("Grid").transform.Find("Units").transform);
		else
			transform.SetParent(GameObject.FindGameObjectWithTag("Grid").transform.Find("Tiles").transform);
		transform.localScale = new Vector3(1, 1, 1);
	}

	public void UpdateDimensions()
	{
		thisRect.sizeDelta = grid.cellSize;
		rows = Mathf.RoundToInt(Screen.currentResolution.height / grid.cellSize.y);
		columns = Mathf.RoundToInt(Screen.currentResolution.width / grid.cellSize.x);
		GetComponent<BoxCollider2D>().size = thisRect.sizeDelta;
	}
}

#endif