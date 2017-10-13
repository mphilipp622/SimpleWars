using UnityEngine;

#if UNITY_EDITOR // this preprocessor directive is required to avoid some errors and warnings in the console
using UnityEditor;

/// <summary>
/// Popup Window for Isaac Tools Editor Extension
/// Notice that it inherits from EditorWindow. We do this so we can access a bunch of Unity Editor functionality
/// This allows us to customize the Unity editor to create custom tools that we want.
/// </summary>

public class UnitPopupWindow: EditorWindow
{
	string unitName;
	string unitType;
	Object unitSprite;
	string[] unitTypes = { "infantry", "tank", "helicopter" };
	int index = 0;

	public static void Init()
	{
		// this function will be called by the isaac menu tool. It will initialize and show this popup window.

		UnitPopupWindow window = ScriptableObject.CreateInstance<UnitPopupWindow>(); // create a new instance of this class
		window.position = new Rect(Screen.width / 2, Screen.height / 2, 256, 256); // set the position of the window when it opens
		window.ShowPopup(); // called EditorWindow.ShowPopup() to open the window in the editor.
	}

	void OnGUI()
	{
		// OnGUI is called even when the game is not playing. This allows us to create functionality that updates in editor mode

		this.Repaint(); // refresh the popup window

		// Create a text field for the name of the prefab we want to make. Assign tileName to the string that's in the name text field.
		unitName = EditorGUILayout.TextField("Tile Name", unitName);
		GUILayout.Space(10); // create some space on the window. Allows us to space out our ui elements

		index = EditorGUILayout.Popup("Unit Type", index, unitTypes);
		unitType = unitTypes[index];

		GUILayout.Space(10);

		// create a sprite field. Allows user to select a sprite to use for the tile. Assign tileSprite to this object
		unitSprite = EditorGUILayout.ObjectField("Sprite", unitSprite, typeof(Sprite), true);

		GUILayout.Space(10);

		if (GUILayout.Button("Accept"))
		{
			// Create Accept button. When user presses this button, we want to call our ScriptableObjectUtility to create a prefab.
			ScriptableObjectUtility.CreateUnit(unitName, unitType, unitSprite);
			this.Close(); // close window
		}
		else if (GUILayout.Button("Cancel"))
			this.Close();
	}
}
#endif