using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

/// <summary>
/// Popup Window for Isaac Tools Editor Extension
/// </summary>


public class TilePopupWindow : EditorWindow
{
	string tileName;
	string terrainTag;
	Object tileSprite;
	string terrainObj;

	public static void Init()
	{
		TilePopupWindow window = ScriptableObject.CreateInstance<TilePopupWindow>();
		window.position = new Rect(Screen.width / 2, Screen.height / 2, 256, 256);
		window.ShowPopup();
	}

	void OnGUI()
	{

		this.Repaint();

		tileName = EditorGUILayout.TextField("Tile Name", tileName);
		GUILayout.Space(10);

		terrainTag = EditorGUILayout.TagField("Terrain Tag", terrainTag);

		GUILayout.Space(10);
		tileSprite = EditorGUILayout.ObjectField("Sprite", tileSprite, typeof(Sprite), true);

		if (GUILayout.Button("Accept"))
		{
			ScriptableObjectUtility.CreateTile(tileName, terrainTag, tileSprite);
			this.Close();
		}
		else if (GUILayout.Button("Cancel"))
			this.Close();
	}
}
#endif