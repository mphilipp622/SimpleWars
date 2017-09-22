using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class TilePopupWindow : EditorWindow
{
	//static string terrainType;
	string tileName;
	string terrainTag;
	Object tileSprite;

	public static void Init()
	{
		//terrainType = newTerrainType;
		TilePopupWindow window = ScriptableObject.CreateInstance<TilePopupWindow>();
		window.position = new Rect(Screen.width / 2, Screen.height / 2, 256, 256);
		window.ShowPopup();
	}

	void OnGUI()
	{

		this.Repaint();

		tileName = EditorGUILayout.TextField("Tile Name", tileName);
		GUILayout.Space(20);

		terrainTag = EditorGUILayout.TagField("Terrain Tag", terrainTag);
		tileSprite = EditorGUILayout.ObjectField("Sprite", tileSprite, typeof(Sprite), true);

		//Debug.Log(buildingType);

		if (GUILayout.Button("Accept"))
		{
			//compiling = true;
			//targetTime = EditorApplication.timeSinceStartup + waitTime;
			ScriptableObjectUtility.CreateTile(tileName, terrainTag, tileSprite);
			this.Close();
		}
		else if (GUILayout.Button("Cancel"))
			this.Close();
		/*else if (compiling)
		{
			
			if (EditorApplication.timeSinceStartup >= targetTime)
			{
				//this.Close();
				ScriptableObjectUtility.PublishAsset(buildingName, nonStaticBuildingType);
				compiling = false;
				this.Close();
			}
		}*/
	}


	/*void CreateScript()
	{
		nonStaticBuildingType = buildingType;
		//ScriptableObjectUtility.CreateBuildingAsset(buildingType, buildingName);
		/*else if(buildingType == "Manufacturing")
			ScriptableObjectUtility.CreateBuildingAsset(buildingType, buildingName);
		else if (buildingType == "Research")
			ScriptableObjectUtility.CreateBuildingAsset(buildingType, buildingName);
		else if (buildingType == "NaturalResources")
			ScriptableObjectUtility.CreateBuildingAsset(buildingType, buildingName);
			
	}*/
}
#endif