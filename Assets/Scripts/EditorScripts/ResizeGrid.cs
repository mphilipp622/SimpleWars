#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;


public class ResizeGrid : MonoBehaviour
{
	/// <summary>
	/// This class will actually resize the grid in the editor. GridResizeButton script will call on it.
	/// </summary>

	public int newTileSizeX, newTileSizeY; // new x and y dimensions for tile size

	public void Resize()
	{
		GameObject.FindGameObjectWithTag("Grid").GetComponent<GridLayoutGroup>().cellSize = new Vector2(newTileSizeX, newTileSizeY);
		foreach (ParentTile tile in FindObjectsOfType<ParentTile>())
			tile.UpdateDimensions(); // grab every tile in the scene and update their dimensions
	}
}
#endif