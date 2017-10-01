using UnityEngine;
using UnityEditor;

public class YourClassAsset
{
	/// <summary>
	/// Creates a custom menu item with a single option in it.
	/// </summary>
	[MenuItem("Isaac Tools/Create New Tile")]
	public static void CreateTile()
	{
		TilePopupWindow.Init(); // call on our popup window.
	}
}