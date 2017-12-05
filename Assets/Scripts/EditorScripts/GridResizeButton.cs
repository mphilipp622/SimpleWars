#if (UNITY_EDITOR)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;


[CustomEditor(typeof(ResizeGrid))]
public class GridResizeButton : Editor
{
	/// <summary>
	/// This script will create a button on the grid game object in the editor inspector.
	/// It will allow us to resize the grid in the editor without screwing up all the tile positioning.
	/// </summary>
	
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		GUILayout.TextArea("Set the x and y values you want for the tile sizes. Once they're set, press 'Change Cell Size'");

		ResizeGrid myScript = (ResizeGrid)target;
		if (GUILayout.Button("Change Cell Size"))
		{
			// if the button gets pressed in the inspector, call ResizeGrid.Resize() function.
			myScript.Resize();
		}
	}
}
#endif