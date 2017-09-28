using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[ExecuteInEditMode]
public class ParentTile : MonoBehaviour {

	GridLayoutGroup grid;
	RectTransform thisRect;
	float newX, newY;

	public int rows, columns;

	private void Awake()
	{
		grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridLayoutGroup>();
		thisRect = GetComponent<RectTransform>();
		transform.parent = grid.transform;
		transform.localScale = new Vector3(1, 1, 1);
		//thisRect.anchoredPosition = Vector3.zero;
		//transform.SetParent(GameObject.FindGameObjectWithTag("Grid").transform);
		//this.enabled = false;
	}
	void Start ()
	{

	}
	
	void Update ()
	{
		if (Application.isPlaying)
		{
			Destroy(GetComponent<SpriteRenderer>());
			this.enabled = false;
		}

		newX = Mathf.Round(thisRect.anchoredPosition.x - ((thisRect.anchoredPosition.x % grid.cellSize.x) - grid.cellSize.x / 2));
		newY = Mathf.Round(thisRect.anchoredPosition.y - ((thisRect.anchoredPosition.y % grid.cellSize.y) + grid.cellSize.y / 2));
		//Debug.Log(newY);
		
		newX = Mathf.Clamp(newX, grid.cellSize.x / 2, (grid.cellSize.x * columns) - grid.cellSize.x / 2);
		newY = Mathf.Clamp(newY, -((grid.cellSize.x * rows) - grid.cellSize.y / 2), -(grid.cellSize.y / 2));
		
		thisRect.anchoredPosition = new Vector3(newX, newY, thisRect.localPosition.z);
	}

}
