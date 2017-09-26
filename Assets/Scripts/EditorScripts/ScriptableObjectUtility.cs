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
	/// This will allow us to create tile prefabs in the editor, using a menu item.
	/// </summary>

	static GameObject newObj;
	static string pathAndName, name;

	public static void CreateTile(string tileName, string terrainTag, UnityEngine.Object tileSprite)
	{
		name = tileName.Replace(" ", "");
		name = name.Replace("-", "");

		string prefabPath = "Assets/Prefabs/Tiles/" + name + ".prefab";

		newObj = new GameObject();
		newObj.transform.parent = GameObject.FindGameObjectWithTag("Grid").transform;


		// Start adding components to the prefab
		RectTransform newRect = newObj.AddComponent<RectTransform>();
		newRect.anchorMin = new Vector2(0, 1);
		newRect.anchorMax = new Vector2(0, 1);
		newRect.sizeDelta = new Vector2(120, 120);
		newRect.localPosition = new Vector3(0, 0, 90);

		Image newImage = newObj.AddComponent<Image>();
		newImage.sprite = (Sprite)tileSprite;

		newObj.gameObject.AddComponent<Tile>();

		ParentTile newParentTile = newObj.gameObject.AddComponent<ParentTile>();
		newParentTile.rows = 9;
		newParentTile.columns = 16;

		LayoutElement newLayoutElement = newObj.gameObject.AddComponent<LayoutElement>();
		newLayoutElement.ignoreLayout = true;

		BoxCollider2D newCollider = newObj.gameObject.AddComponent<BoxCollider2D>();
		newCollider.isTrigger = true;
		newCollider.size = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridLayoutGroup>().cellSize;

		SpriteRenderer newRend = newObj.gameObject.AddComponent<SpriteRenderer>();
		newRend.sprite = (Sprite)tileSprite;

		newObj.tag = terrainTag;

		newObj.name = tileName;

		PrefabUtility.CreatePrefab(prefabPath, newObj.gameObject);
		GameObject.DestroyImmediate(newObj.gameObject);

		EditorUtility.FocusProjectWindow();
		Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(prefabPath);

	}
}
#endif