using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : Unit
{
	int x = 0, y = 0, lastX = 0, lastY = 0;

	void Start ()
	{
		transform.position = GridManager.gridMan.tiles[0, 0].position;
	}
	
	void Update ()
	{
		Move();
		//if(Input.GetButtonDown("Jump"))
		//	transform.position = new Vector3(GridManager.gridMan.gridPosition[4, 5].position.x, GridManager.gridMan.gridPosition[4, 5].position.y, 0);
	}

	void Move()
	{
		if (Input.GetKeyDown(KeyCode.W) && y < GridManager.gridMan.columns - 1)
		{
			lastY = y;
			y++;
			transform.position = GridManager.gridMan.tiles[x, y].position; // move character position
		}
		if (Input.GetKeyDown(KeyCode.S) && y > 0 )
		{
			lastY = y;
			y--;
			transform.position = GridManager.gridMan.tiles[x, y].position;
		}
		if (Input.GetKeyDown(KeyCode.A) && x > 0)
		{
			lastX = x;
			x--;
			transform.position = GridManager.gridMan.tiles[x, y].position;
		}
		if (Input.GetKeyDown(KeyCode.D) && x < GridManager.gridMan.rows - 1)
		{
			lastX = x;
			x++;
			transform.position = GridManager.gridMan.tiles[x, y].position;
		}
	}

	bool isSelected = false;
	private void OnMouseDown()
	{
		if (!isSelected)
		{
			isSelected = true;
			Move();
		}
		else if(isSelected)
		{
			isSelected = false;
		}
	}



	private void OnTriggerEnter2D(Collider2D collision)
	{
		GridManager.gridMan.tiles[x, y].unit = this;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		GridManager.gridMan.tiles[lastX, lastY].unit = null;
	}
}
