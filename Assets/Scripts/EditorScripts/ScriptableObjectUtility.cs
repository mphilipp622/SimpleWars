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
	//	This makes it easy to create, name and place unique new ScriptableObject asset files.
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

		RectTransform newRect = newObj.AddComponent<RectTransform>();
		newRect.anchorMin = new Vector2(0, 1);
		newRect.anchorMax = new Vector2(0, 1);
		newRect.sizeDelta = new Vector2(120, 120);
		newRect.localPosition = Vector3.zero;

		newObj.AddComponent<Image>();
		newObj.GetComponent<Image>().sprite = (Sprite)tileSprite;

		newObj.gameObject.AddComponent<Tile>();

		newObj.gameObject.AddComponent<ParentTile>();
		newObj.gameObject.GetComponent<ParentTile>().rows = 9;
		newObj.gameObject.GetComponent<ParentTile>().columns = 16;

		newObj.gameObject.AddComponent<LayoutElement>();
		newObj.gameObject.GetComponent<LayoutElement>().ignoreLayout = true;

		newObj.gameObject.AddComponent<BoxCollider2D>();
		newObj.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

		newObj.gameObject.AddComponent<SpriteRenderer>();
		newObj.gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)tileSprite;

		newObj.tag = terrainTag;

		newObj.name = tileName;

		PrefabUtility.CreatePrefab(prefabPath, newObj.gameObject);
		GameObject.DestroyImmediate(newObj.gameObject);

		EditorUtility.FocusProjectWindow();
		Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(prefabPath);
		//Selection.activeObject = newObj;
		

	}
	/*public static void PublishAsset(string newName, string thisBuildingType)
	{
		name = newName.Replace(" ", "");
		name = name.Replace("-", "");

		string prefabPath = "Assets/Prefabs/Buildings/" + thisBuildingType + "/" + name + ".prefab";

		//newObj = new GameObject();
		//newObj.AddComponent<SpriteRenderer>();

		/*if (thisBuildingType == "Research")
			newObj.AddComponent<InventoryResearch>();
		else
			newObj.AddComponent<Inventory>();

		newObj.AddComponent(Type.GetType(name));
		newObj.GetComponent<BuildingData>().name = name;
		newObj.tag = "Building";
		
		//GameObject.DestroyImmediate(gameObject.GetComponent<CreateClass>());
		PrefabUtility.CreatePrefab(prefabPath, newObj);
		//AssetDatabase.CreateAsset(scriptAsset, scriptPathAndName);
		GameObject.DestroyImmediate(newObj);

		//AssetDatabase.SaveAssets();
		//AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow();
		//Selection.activeObject = finalAsset;

		Debug.Log("Finished Compiling " + name);
	}*/
}
#endif