using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : Unit
{
	int x = 0, y = 0, lastX = 0, lastY = 0, movement = 2;

	bool isSelected = false;

	void Start ()
	{
		transform.position = GridManager.gridMan.tiles[5, 6].position;
		x = 5;
		y = 6;
		
	}
	
	void Update ()
	{
		
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

	void FindTraversableTiles()
	{
		for(int i = -1; i < 2; i++)
		{
			for(int j = -1; j < 2; j++)
			{
				if(i == 0 && j == 0)
					continue;

				if (x + i > GridManager.gridMan.tiles.GetLength(0) - 1 || x + i < 0 || y + j > GridManager.gridMan.tiles.GetLength(1) - 1 || y + j < 0)
					continue;

				Tile currentTile = GridManager.gridMan.tiles[x + i, y + j];

				float totalCost = 0;

				while(totalCost <= movement)
				{
					//totalcost = (totalCost + distance between currentGrid and previousGrid) / cellSize + currentTile terrain Modifier
					totalCost += (Vector2.Distance(currentTile.gridPosition, GridManager.gridMan.tiles[currentTile.x - i, currentTile.y - j].gridPosition) / GridManager.gridMan.grid.cellSize.x) + currentTile.movementModifier;

					if (totalCost <= movement && currentTile.unit == null)
						currentTile.SetTraversable();
					else if(totalCost > movement)
						break;

					// bounds checking
					if (currentTile.x + i > GridManager.gridMan.tiles.GetLength(0) - 1 || currentTile.x + i < 0 || currentTile.y + j > GridManager.gridMan.tiles.GetLength(1) - 1 || currentTile.y + j < 0)
						break;

					currentTile = GridManager.gridMan.tiles[currentTile.x + i, currentTile.y + j]; // assign next tile
				}
			}

		}
	}

	private void OnMouseDown()
	{
		if (!isSelected)
		{
			isSelected = true;
			FindTraversableTiles();
			//Move();
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
