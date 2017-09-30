using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;

[CustomEditor(typeof(ResizeGrid))]
public class GridResizeButton : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		GUILayout.TextArea("Set the x and y values you want for the tile sizes. Once they're set, press 'Change Cell Size'");

		ResizeGrid myScript = (ResizeGrid)target;
		if (GUILayout.Button("Change Cell Size"))
		{
			myScript.Resize();
		}
	}

	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}