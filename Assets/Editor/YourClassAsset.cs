using UnityEngine;
using UnityEditor;

public class YourClassAsset
{
	[MenuItem("Isaac Tools/Create New Tile")]
	public static void CreateTile()
	{
		TilePopupWindow.Init();
	}
}