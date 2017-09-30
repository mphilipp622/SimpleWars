using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;

public class ResizeGrid : MonoBehaviour
{
	public int newTileSizeX, newTileSizeY;

	public void Resize()
	{
		GameObject.FindGameObjectWithTag("Grid").GetComponent<GridLayoutGroup>().cellSize = new Vector2(newTileSizeX, newTileSizeY);
		foreach (ParentTile tile in FindObjectsOfType<ParentTile>())
			tile.UpdateDimensions();
	}
}