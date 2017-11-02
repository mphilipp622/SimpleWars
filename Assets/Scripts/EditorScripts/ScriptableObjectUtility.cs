#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;
using UnityEngine.UI;

public static class ScriptableObjectUtility
{
	/// <summary>
	/// This will allow us to create tile prefabs in the editor, using a menu item in the Unity editor
	/// </summary>

	static GameObject newObj;
	static string pathAndName, name;


	public static void CreateTile(string tileName, string terrainTag, UnityEngine.Object tileSprite)
	{
		///<summary>
		/// This function will create a new game object, assign it the components our Tile needs then save the object as a prefab
		/// so we can use it later.
		/// </summary>

		// Force our prefab name to a specific syntax. No spaces, no hyphens
		name = tileName.Replace(" ", "");
		name = name.Replace("-", "");

		// Create a filepath for the prefab using it's name.
		string prefabPath = "Assets/Prefabs/Tiles/" + name + ".prefab";

		// create a new game object. We need to do this so we can add components to it then store it as a prefab.
		newObj = new GameObject();
		newObj.transform.parent = GameObject.FindGameObjectWithTag("Grid").transform; // force gameobject's parent to be the grid.

		// Start adding components to the prefab
		// First, we need a RectTransform since the Tile will be a UI object attached to a canvas.
		RectTransform newRect = newObj.AddComponent<RectTransform>();
		newRect.anchorMin = new Vector2(0, 1); // this will set our anchoring. By default, we want the anchor to be top-left.
		newRect.anchorMax = new Vector2(0, 1);
		newRect.sizeDelta = new Vector2(120, 120); // default the width and height of the tile to 120 x 120
		newRect.localPosition = new Vector3(0, 0, 90); // make sure the local position is 0, 0, 90 by default.

		// add an Image component. This is necessary for the sprite to show up on the UI Canvas
		Image newImage = newObj.AddComponent<Image>();
		newImage.sprite = (Sprite)tileSprite; // set the Image sprite to the sprite the user specified from the popup window
		
		// Tile needs a collider on it to detect unit movements.
		BoxCollider2D newCollider = newObj.gameObject.AddComponent<BoxCollider2D>();
		newCollider.isTrigger = true; // set the collider to be a trigger.
		newCollider.size = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridLayoutGroup>().cellSize / 2; // set collider size to the cell size of our tiles

		// the LayoutElement allows our tiles to ignore mandatory positioning from the GridLayoutGroup component. We need this so
		// Isaac can place tiles wherever he wants without being forced into an order. ParentTile script allows him to place tiles
		// wherever he wants within the grid layout area.
		LayoutElement newLayoutElement = newObj.gameObject.AddComponent<LayoutElement>();
		newLayoutElement.ignoreLayout = true; // make sure the LayoutElement ignores layout by default.

		// The only reason we have a sprite renderer on the tile is so that the sprite shows up in the editor for the prefab. Without this, we would not be able to see the sprite attached to this game object in the inspector.
		SpriteRenderer newRend = newObj.gameObject.AddComponent<SpriteRenderer>();
		newRend.sprite = (Sprite)tileSprite;

		// Add ParentTile script. This is used for placing tiles in the editor and making sure their positions
		// snap to the grid properly.
		ParentTile newParentTile = newObj.gameObject.AddComponent<ParentTile>();
		newParentTile.rows = 8; // set default rows and columns to 9 and 16 since that is what our 120x120 tile size gets us at 16:9 aspect ratio
		newParentTile.columns = 16;

		newObj.gameObject.AddComponent<Tile>(); // Add our Tile script to the object

		newObj.tag = terrainTag; // set the object's tag appropraitely. Tagging is important for defense, attack, and movement mods.

		newObj.name = tileName; // set prefab name

		if (newObj.tag == "Building")
		{
			prefabPath = "Assets/Prefabs/Building/" + name + ".prefab";
			newObj.AddComponent<Building>();
		}

		PrefabUtility.CreatePrefab(prefabPath, newObj.gameObject); // create prefab at the path we made earlier using our gameobject.
		GameObject.DestroyImmediate(newObj.gameObject); // Once prefab is saved, destroy the game object from our scene.

		EditorUtility.FocusProjectWindow(); // force the Editor to focus on project window
		Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(prefabPath); // force the Editor to navigate to the new file that was just created.
	}

	public static void CreateUnit(string unitName, string unitType, UnityEngine.Object unitSprite)
	{
		///<summary>
		/// This function will create a new game object, assign it the components our Tile needs then save the object as a prefab
		/// so we can use it later.
		/// </summary>

		// Force our prefab name to a specific syntax. No spaces, no hyphens
		name = unitName.Replace(" ", "");
		name = name.Replace("-", "");

		// Create a filepath for the prefab using it's name.
		string prefabPath = "Assets/Prefabs/Units/" + name + ".prefab";

		// create a new game object. We need to do this so we can add components to it then store it as a prefab.
		newObj = new GameObject();
		newObj.transform.parent = GameObject.FindGameObjectWithTag("Grid").transform; // force gameobject's parent to be the grid.

		// Start adding components to the prefab
		// First, we need a RectTransform since the Tile will be a UI object attached to a canvas.
		RectTransform newRect = newObj.AddComponent<RectTransform>();
		newRect.anchorMin = new Vector2(0, 1); // this will set our anchoring. By default, we want the anchor to be top-left.
		newRect.anchorMax = new Vector2(0, 1);
		newRect.sizeDelta = new Vector2(120, 120); // default the width and height of the tile to 120 x 120
		newRect.localPosition = new Vector3(0, 0, 90); // make sure the local position is 0, 0, 90 by default.

		Image newImage = newObj.AddComponent<Image>();
		newImage.sprite = (Sprite)unitSprite; // set the Image sprite to the sprite the user specified from the popup window
		// The only reason we have a sprite renderer on the tile is so that the sprite shows up in the editor for the prefab. Without this, we would not be able to see the sprite attached to this game object in the inspector.
		SpriteRenderer newRend = newObj.gameObject.AddComponent<SpriteRenderer>();
		newRend.sprite = (Sprite)unitSprite;

		LayoutElement newLayoutElement = newObj.gameObject.AddComponent<LayoutElement>();
		newLayoutElement.ignoreLayout = true; // make sure the LayoutElement ignores layout by default.

		Rigidbody2D newRigidbody = newObj.AddComponent<Rigidbody2D>();
		newRigidbody.gravityScale = 0f;

		// Tile needs a collider on it to detect unit movements.
		BoxCollider2D newCollider = newObj.gameObject.AddComponent<BoxCollider2D>();
		newCollider.isTrigger = true; // set the collider to be a trigger.
		newCollider.size = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridLayoutGroup>().cellSize; // set collider size to the cell size of our tiles

		// Add ParentTile script. This is used for placing tiles in the editor and making sure their positions
		// snap to the grid properly.
		ParentTile newParentTile = newObj.gameObject.AddComponent<ParentTile>();
		newParentTile.rows = 8; // set default rows and columns to 9 and 16 since that is what our 120x120 tile size gets us at 16:9 aspect ratio
		newParentTile.columns = 16;

		newObj.AddComponent<Unit>();
		/*if (unitType == "infantry")
			newObj.AddComponent<Infantry>();
		else if (unitType == "tank")
			newObj.AddComponent<Tank>();
		else if (unitType == "helicopter")
			newObj.AddComponent<Helicopter>();*/

		newObj.tag = "Unit"; // set the object's tag appropraitely. Tagging is important for defense, attack, and movement mods.

		newObj.name = unitName; // set prefab name

		PrefabUtility.CreatePrefab(prefabPath, newObj.gameObject); // create prefab at the path we made earlier using our gameobject.
		GameObject.DestroyImmediate(newObj.gameObject); // Once prefab is saved, destroy the game object from our scene.

		EditorUtility.FocusProjectWindow(); // force the Editor to focus on project window
		Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(prefabPath); // force the Editor to navigate to the new file that was just created.

	}
}
#endif