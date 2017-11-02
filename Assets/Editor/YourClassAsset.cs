using UnityEngine;
using UnityEditor;

public class YourClassAsset
{
	/// <summary>
	/// Creates a custom menu item with a single option in it.
	/// </summary>
	[MenuItem("CSCI 150/Create New Tile OR Building")]
	public static void CreateTile()
	{
		TilePopupWindow.Init(); // call on our popup window.
	}
	[MenuItem("CSCI 150/Create New Unit")]
	public static void CreateUnit()
	{
		UnitPopupWindow.Init();
	}
}